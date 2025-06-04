using UnityEngine;

public class TurnManager : Singleton<TurnManager>   
{

    [SerializeField] private double minTimePerTurn = 2100;
    private double _lastTurnTime = 0f;

    private Player player1;
    private Player player2;

    protected PointTurnBar pointTurnBar;
    protected GameObject announcementTurnUI;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        player1 = GameObject.Find("Player1").GetComponent<Player>();
        player2 = GameObject.Find("Player2").GetComponent<Player>();
        pointTurnBar = GameObject.Find("SliderTurnPoint").GetComponent<PointTurnBar>();
        announcementTurnUI = GameObject.Find("AnnouncementTurnUI");
    }

    protected override void Start()
    {
        base.Start();
        player1.Turn = true;
        player2.Turn = false;
        _lastTurnTime = Time.time * 1000;
    }

public void ChangeTurn()
    {

        // 
        // get current time
        _lastTurnTime = Time.time * 1000; // convert to milliseconds


        BoardManager.Instance.CancelHighlightAndSelectedChess();
        // switch turn
        if (GetCurrentTurn() == Const.SIDE_WHITE)
        {
            player1.EndTurn();
            player2.StartTurn();
        }
        else
        {
            player1.StartTurn();
            player2.EndTurn();
        }

        foreach (var piece in GameManager.Instance.pieces)
        {
            piece.ResetMove();
        }
        // reset point turn bar
        Player currentPlayer = GetCurrentPlayer();
        pointTurnBar.SetMaxPoint(currentPlayer.MaxTurnPoint);
        pointTurnBar.SetPoint(currentPlayer.TurnPoint);
        
        //announcement
        announcementTurnUI.GetComponent<AnnouncementTurn>().ShowAnnouncementTurn(GetCurrentTurn());

        // generate item buff
        //if(Random.Range(0, 2) == 1)
        GeneratorItemBuff.Instance.Generate(1);
    }

    public void EndPieceTurn(int cost)
    {
        Player currentPlayer = GetCurrentPlayer();
        currentPlayer.TurnPoint -= cost;
        pointTurnBar.SetPoint(currentPlayer.TurnPoint);
        foreach (var piece in GameManager.Instance.pieces)
        {
            if (piece.Side == GetCurrentTurn())
            {
                if (piece.Cost <= currentPlayer.TurnPoint)
                {
                    return;
                }
            }
        }
        
        ChangeTurn();
    }

    public int GetCurrentTurn()
    {
        if (player1.Turn) return Const.SIDE_WHITE;
        return Const.SIDE_BLACK;
    }

     public Player GetPlayer(int side)
    {
        if (side == Const.SIDE_WHITE) return player1;
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
        if (player1.Turn) return player1;
        return player2;
    }

}
