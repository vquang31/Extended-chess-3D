using Mirror;
using System;
using System.Collections;
using UnityEngine;

public class TurnManager  : NetworkSingleton<TurnManager>
{


    [SyncVar] private int _currentTurn = Const.SIDE_WHITE;  // bắt đầu turn là trắng

    [SerializeField] private double minTimePerTurn = 2100;
    private double _lastTurnTime = 0f;

    [SyncVar] public  Player player1;
    [SyncVar] public  Player player2;

    protected ActionPointBar pointTurnBar;
    protected ManaPointBar manaPointBar;
    protected GameObject announcementTurnUI;
    protected GameObject playable_UIGameObject;

    public override void OnStartServer()
    {
        base.OnStartServer();
        _lastTurnTime = NetworkTime.time * 1000;
        LoadComponents();
    }
    protected override void LoadComponents()
    {
        pointTurnBar = GameObject.Find("SliderActionPoint").GetComponent<ActionPointBar>();
        manaPointBar = GameObject.Find("SliderManaPoint").GetComponent<ManaPointBar>();
        announcementTurnUI = GameObject.Find("AnnouncementTurnUI");
        playable_UIGameObject = GameObject.Find("Playable_UI");
    }

    protected override void Start()
    {
        _lastTurnTime = Time.time * 1000;
    }

    [Command (requiresAuthority = false)]
    public void EndPieceTurn(int cost)
    {
        Player currentPlayer = GetCurrentPlayer();
        currentPlayer.IncreaseActionPoint(-cost);
        foreach (var piece in GameManager.Instance.pieces)
        {
            if (piece.Side == GetCurrentTurn())
            {
                if (piece.Cost <= currentPlayer.ActionPoint)
                {
                    return;
                }
            }
        }
        ChangeTurn();
    }


    [Command(requiresAuthority = false)]
    public void ChangeTurn()
    {
        //if (!isServer) return;
        RpcChangeTurn(); // gọi hàm RpcChangeTurn trên tất cả client
    }


    [ClientRpc] // cả 2 server và client đều chạy hàm này
    public void RpcChangeTurn()
    {
        // get current time
        _lastTurnTime = Time.time * 1000; // convert to milliseconds
        BoardManager.Instance.CancelHighlightAndSelectedChess();   // cả 2 

        if(GetCurrentTurn() == Const.SIDE_WHITE)
        {
            _currentTurn = Const.SIDE_BLACK;
        }
        else
        {
            _currentTurn = Const.SIDE_WHITE;
        }

        // switch turn
        SwitchTurn();  // server
        foreach (var piece in GameManager.Instance.pieces)
        {
            piece.ResetMove();
        }

        //RpcUpdateClientUI();
        // Update and Reset UI
        UpdateAndResetUI(); // cả 2

        // generate item buff
        GeneratorItemBuff.Instance.Generate(1); //server
    }

    [ServerCallback]
    //[Server]
    private void SwitchTurn()
    {
        if (GetCurrentTurn() == Const.SIDE_WHITE)
        {
            player1.StartTurn();
            player2.EndTurn();
        }
        else
        {
            player1.EndTurn();
            player2.StartTurn();
        }
    }

    private void UpdateAndResetUI()
    {

        // reset point turn bar
         UpdateActionPointBar();

        // update mana point bar    
        if(isServer) RpcUpdateManaPointBar();
        //announcement
        announcementTurnUI.GetComponent<AnnouncementTurn>().ShowAnnouncementTurn(GetCurrentTurn());
        // update playable UI
        playable_UIGameObject.SetActive(GetPlayablePlayer().Side == GetCurrentTurn());

        MagicCastManager.Instance.EndCasting();
    }

    public Player GetPlayablePlayer()
    {
        if (player1.IsPlayable()) return player1;
        else return player2;
    }

    [ClientRpc]
    public void RpcUpdateManaPointBar()
    {
        StartCoroutine(UpdateManaPointBarRoutine());
    }

    IEnumerator UpdateManaPointBarRoutine()
    {
        yield return new WaitForSeconds(0.1f); // wait for a short time to ensure UI is ready
        UpdateManaPointBar();   
    }
    public void UpdateManaPointBar()
    {
        manaPointBar.SetPoint(GetCurrentPlayer().Mana);
    }


    [ClientRpc]
    public void RpcUpdateActionPointBar()
    {
        StartCoroutine(UpdateActionPointBarRoutine());
    }

    IEnumerator UpdateActionPointBarRoutine()
    {
        yield return new WaitForSeconds(0.1f); // wait for a short time to ensure UI is ready
        UpdateActionPointBar();
    }

    public void UpdateActionPointBar()
    {

        pointTurnBar.SetPoint(GetPlayablePlayer().ActionPoint);
    }

    public int GetCurrentTurn()
    {
        return _currentTurn;
    }

     public Player GetPlayer(int side)
    {
        if(player1._side == side)
        {
            return player1;
        }
        return player2;
    }

    public bool CanSwitchTurn()
    {
        // check if enough time has passed
        double currentTime = Time.time * 1000; // convert to milliseconds
        return (currentTime - _lastTurnTime) >= minTimePerTurn;
    }


    public Player GetCurrentPlayer()
    {
        if (_currentTurn == Const.SIDE_WHITE) return player1;
        return player2;
    }

    [Server]
    public void SetReady()
    {
        GeneratorManager.Instance.Generate();
    }

    [ClientRpc]
    public void RpcSetActiveOfflineManager()
    {
        Debug.Log("Run3 - Called on client only");
        GameObject OfflineManager = GameObject.Find("Offline_MANAGER");
        GameObject firstChild = OfflineManager.transform.GetChild(0).gameObject;
        firstChild.SetActive(true);
    }

    [TargetRpc]
    public void TargetSetActiveOfflineManager(NetworkConnection target)
    {
        Debug.Log("Run2 - Called on client only");
        GameObject OfflineManager = GameObject.Find("Offline_MANAGER");
        GameObject firstChild = OfflineManager.transform.GetChild(0).gameObject;
        firstChild.SetActive(true);
    }

}


