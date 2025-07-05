using System;
using UnityEngine;

public class Player : NewMonoBehaviour
{
    private bool _turn;

    private int _actionPoint = Const.MAX_POINT_PER_TURN;
    private int _maxActionPoint = Const.MAX_POINT_PER_TURN;   

    [SerializeField]private int _mana = Const.MAX_MANA / 2;
    public int ActionPoint
    {
        get => _actionPoint;
        set => _actionPoint = value;
    }
    public int MaxActionPoint
    {
        get => _maxActionPoint;
        set => _maxActionPoint = value;
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
        _actionPoint = _maxActionPoint;
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

    public void IncreaseActionPoint(int cost)
    {
        ActionPoint += cost;
        TurnManager.Instance.UpdateActionPointBar(); 
    }
}
