using UnityEngine;

public class TurnManager : Singleton<TurnManager>   
{
    private Player player1;
    private Player player2;

    protected PointTurnBar pointTurnBar;



    protected override void LoadComponents()
    {
        base.LoadComponents();
        player1 = GameObject.Find("Player1").GetComponent<Player>();
        player2 = GameObject.Find("Player2").GetComponent<Player>();
        pointTurnBar = GameObject.Find("SliderTurnPoint").GetComponent<PointTurnBar>();

    }

    protected override void Start()
    {
        base.Start();
        player1.Turn = true;
        player2.Turn = false;
        
    }

    public void ChangeTurn()
    {
        BoardManager.Instance.CancelHighlightAndSelectedChess();
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
        Player currentPlayer = GetPlayer(GetCurrentTurn());
        pointTurnBar.SetMaxPoint(currentPlayer.MaxTurnPoint);
        pointTurnBar.SetPoint(currentPlayer.TurnPoint); 


        // generate item buff
        //if(Random.Range(0, 2) == 1)
        GeneratorItemBuff.Instance.Generate(1);
    }

    public void EndPieceTurn(int cost)
    {
        Player currentPlayer = GetPlayer(GetCurrentTurn()); 
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


}
