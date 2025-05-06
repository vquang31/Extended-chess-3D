using UnityEngine;

public class Player : NewMonoBehaviour
{
    private bool _turn;

    private int _turnPoint = Const.MAX_POINT_PER_TURN;
    private int _maxTurnPoint = Const.MAX_POINT_PER_TURN;   
    public int TurnPoint
    {
        get => _turnPoint;
        set => _turnPoint = value;
    }
    public int MaxTurnPoint
    {
        get => _maxTurnPoint;
        set => _maxTurnPoint = value;
    }   


    public bool Turn
    {
        get => _turn;
        set => _turn = value;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        _turn = false;  
    }

    public void EndTurn()
    {
        _turn = false;
    }

    public void StartTurn()
    {
        _turnPoint = _maxTurnPoint;
        _turn = true;
    }


}
