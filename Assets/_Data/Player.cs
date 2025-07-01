using System;
using UnityEngine;

public class Player : NewMonoBehaviour
{
    private bool _turn;

    private int _turnPoint = Const.MAX_POINT_PER_TURN;
    private int _maxTurnPoint = Const.MAX_POINT_PER_TURN;   

    [SerializeField]private int _mana = Const.MAX_MANA / 2;
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

    public int Mana
    {
        get => _mana;
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
        IncreaseMana(Const.INCREASE_MANA_PER_TURN);
        _turn = true;
    }

    public void IncreaseMana(int mana)
    {
        _mana += mana;
        if (_mana > Const.MAX_MANA)
        {
            _mana = Const.MAX_MANA;
        }
        TurnManager.Instance.UpdateManaPointBar();
    }

    public void IncreaseTurnPoint(int cost)
    {
        TurnPoint += cost;
        TurnManager.Instance.UpdateTurnPointBar(); 
    }
}
