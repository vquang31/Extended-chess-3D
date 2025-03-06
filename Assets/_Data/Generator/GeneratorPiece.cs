using Unity.Burst.CompilerServices;
using UnityEngine;

public class GeneratorPiece: Singleton<Generator>
{

    protected GameObject WhitePawnGameObject;
    protected GameObject WhiteRookGameObject;
    protected GameObject WhiteKnightGameObject;
    protected GameObject BishopGameObject;
    protected GameObject QueenGameObject;
    protected GameObject KingGameObject;

    protected GameObject BlackPawnGameObject;
    protected GameObject BlackRookGameObject;
    protected GameObject BlackKnightGameObject;
    protected GameObject BlackBishopGameObject;
    protected GameObject BlackQueenGameObject;
    protected GameObject BlackKingGameObject;



    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.WhitePawnGameObject = GameObject.Find("Prefab_WhitePawn");
        this.WhiteRookGameObject = GameObject.Find("Prefab_WhiteRook");
        this.WhiteKnightGameObject = GameObject.Find("Prefab_WhiteKnight");
        this.BishopGameObject = GameObject.Find("Prefab_WhiteBishop");
        this.QueenGameObject = GameObject.Find("Prefab_WhiteQueen");
        this.KingGameObject = GameObject.Find("Prefab_WhiteKing");

        this.BlackPawnGameObject = GameObject.Find("Prefab_BlackPawn");
        this.BlackRookGameObject = GameObject.Find("Prefab_BlackRook");
        this.BlackKnightGameObject = GameObject.Find("Prefab_BlackKnight");
        this.BlackBishopGameObject = GameObject.Find("Prefab_BlackBishop");
        this.BlackQueenGameObject = GameObject.Find("Prefab_BlackQueen");
        this.BlackKingGameObject = GameObject.Find("Prefab_BlackKing");

        Generate();
    }

    protected void Generate()
    {
        Debug.Log("Generate Piece");
        // Generate piece
        for(int i = 1; i >= -1; i-=2)
        {
            for(int j = 1; j <= 8 ; j++)
            {
                GameObject newPieceGameObject;
                newPieceGameObject = GameObject.Instantiate((i == Const.SIDE_WHITE) ? WhitePawnGameObject : BlackPawnGameObject);
                newPieceGameObject.name = Method2.NamePiece(i,"Pawn", j);
                newPieceGameObject.transform.parent = GameObject.Find("Pieces").transform;
                Piece piece = newPieceGameObject.GetComponent<Piece>();
                Vector2Int newPos2D = new Vector2Int((Const.MAX_BOARD_SIZE - 8 ) / 2 + j
                                                    , (i == Const.SIDE_WHITE) ? 2 : Const.MAX_BOARD_SIZE - 1);
                Vector3Int newPos3D = Method2.Pos2dToPos3d(newPos2D, SearchingMethod.HeightOfSquare(newPos2D));
                piece.SetPosition(newPos3D);
                //piece.InitRandomHeight(new Vector2Int(1, i));
            }
            //for(int j = 1  ; j <= 2 ; j++)
            //{
            //    GameObject newRook = GameObject.Instantiate((i == Const.SIDE_WHITE) ? WhiteRookGameObject : BlackRookGameObject);
            //    GameObject newKnight = GameObject.Instantiate((i == Const.SIDE_WHITE) ? WhiteKnightGameObject : BlackKnightGameObject);
            //    GameObject newBishop = GameObject.Instantiate((i == Const.SIDE_WHITE) ? BishopGameObject : BlackBishopGameObject);
            //    newRook.name = Method2.NamePiece(i, "Rook", j);
            //    newKnight.name = Method2.NamePiece(i, "Knight", j);
            //    newBishop.name = Method2.NamePiece(i, "Bishop", j);
            //    newRook.transform.parent = GameObject.Find("Pieces").transform;
            //    newKnight.transform.parent = GameObject.Find("Pieces").transform;
            //    newBishop.transform.parent = GameObject.Find("Pieces").transform;

            //}
        }
    }
}