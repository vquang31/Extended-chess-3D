using UnityEngine;

public class TurnManager : Singleton<TurnManager>   
{
    private Player player1;
    private Player player2;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        player1 = GameObject.Find("Player1").GetComponent<Player>();
        player2 = GameObject.Find("Player2").GetComponent<Player>();
    }

    private void Start()
    {
        player1.Turn = true;
        player2.Turn = false;
    }


    public void ChangeTurn()
    {
        player1.Turn = !player1.Turn;
        player2.Turn = !player2.Turn;
    }

    public int Turn()
    {
        if (player1.Turn) return 1;
        return -1;
    }
}
