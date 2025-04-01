using UnityEngine;

public class TurnManager : Singleton<TurnManager>   
{
    [SerializeField] public bool avaliable = false;
    private Player player1;
    private Player player2;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        player1 = GameObject.Find("Player1").GetComponent<Player>();
        player2 = GameObject.Find("Player2").GetComponent<Player>();
    }

    protected override void Start()
    {
        base.Start();
        player1.Turn = true;
        player2.Turn = false;

        if (avaliable == false)
        {
            player1.Turn = true; 
            player2.Turn = true;
        }
    }


    public void ChangeTurn()
    {
        if (avaliable)
        {
            player1.Turn = !player1.Turn;
            player2.Turn = !player2.Turn;

        }
    }

    public int Turn()
    {
        if (player1.Turn) return 1;
        return -1;
    }
}
