using Mirror;
using UnityEngine;

public class PlayerManager : NetworkBehaviour
{
    [SyncVar] public int _side; // 1: trắng, -1: đen

    public bool playable = false;

    public Player Player => GetComponent<Player>();

    protected void Awake()
    {
        LoadComponents();
    }

    protected void LoadComponents()
    {
    }


    public override void OnStartClient()
    {
        base.OnStartClient();
        if (isLocalPlayer)
        {
            Debug.Log("You are team: " + (_side == Const.SIDE_WHITE ? "White" : "Black"));
            playable = true;
        }
        Player.InitSide(_side);
    }

    public bool IsMyTurn()
    {
        return TurnManager.Instance.GetCurrentTurn() == _side;
    }

}
