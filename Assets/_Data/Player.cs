using System;
using Mirror;
using UnityEngine;


public class Player : NetworkBehaviour
{

    public bool playable = false;
    //[SyncVar(hook = nameof(OnHealthChanged))]
    [SyncVar] public  int _side; // 1: trắng, -1: đen

    [SyncVar] public int _actionPoint = Const.MAX_POINT_PER_TURN;
    [SyncVar] public int _mana = Const.MAX_MANA / 2;

    [SyncVar(hook = nameof(OnOccupyingPointChaned))] private int occupyingPoint = 0;


    private int _maxActionPoint = Const.MAX_POINT_PER_TURN;
    // === Properties ===
    public int ActionPoint => _actionPoint;
    public int MaxActionPoint => _maxActionPoint;
    public int Mana => _mana;

    public int Side => _side;

    public int OccupyingPoint => occupyingPoint;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (isLocalPlayer)
        {
            Debug.Log("You are team: " + (_side == Const.SIDE_WHITE ? "White" : "Black"));
            playable = true;
        }

        //TurnManager turnManager = FindObjectOfType<TurnManager>();
        //if (_side == Const.SIDE_WHITE)
        //{
        //    turnManager.player1 = this;
        //}
        //else if (_side == Const.SIDE_BLACK)
        //{
        //    turnManager.player2 = this;
        //}

    }

    public bool IsMyTurn()
    {
        return TurnManager.Instance.GetCurrentTurn() == _side;
    }

    public void InitSide(int side)
    {
        _side = side;

    }


    [Command (requiresAuthority = false)]
    public void StartTurn()
    {
        _actionPoint = _maxActionPoint;
        TurnManager.Instance.RpcUpdateActionPointBar();
        IncreaseMana(Const.INCREASE_MANA_PER_TURN);
    }

    [Command(requiresAuthority = false)]
    public void EndTurn()
    {
        IncreaseMana(_actionPoint / 2);
        _actionPoint = 0;
    }
    [Command(requiresAuthority = false)]
    public void IncreaseMana(int mana)
    {
        _mana += mana;
        if (_mana > Const.MAX_MANA)
        {
            _mana = Const.MAX_MANA;
        }
        TurnManager.Instance.RpcUpdateManaPointBar();
    }

    public void IncreaseActionPoint(int value)
    {
        _actionPoint += value;
        if (_actionPoint > _maxActionPoint) _actionPoint = _maxActionPoint;
        TurnManager.Instance.RpcUpdateActionPointBar(); 
    }


    // === Hooks for Client ===
    private void OnTurnChanged(bool oldVal, bool newVal)
    {
        //TurnManager.Instance?.UpdateTurnUI(newVal); // Hiển thị icon lượt chơi
    }

    //private void OnActionPointChanged(int oldVal, int newVal)
    //{
    //    if (isOwned) TurnManager.Instance?.RpcUpdateActionPointBar();
    //}

    //private void OnManaChanged(int oldVal, int newVal)
    //{
    //    if (isOwned) TurnManager.Instance?.UpdateManaPointBar();
    //}

    public bool IsPlayable()
    {
        return playable;
    }
    public void IncreaseOccupyingPoint(int value)
    {
        occupyingPoint += value;
        //TurnManager.Instance.RpcUpdateOccupyingPointBar();
    }

    private void OnOccupyingPointChaned(int oldValue, int newValue)
    {

        TurnManager.Instance.CmdUpdateOccupyingPointBar();
    }

}
