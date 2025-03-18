using UnityEngine;

public class Player : NewMonoBehaviour
{
    private bool _turn;

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
}
