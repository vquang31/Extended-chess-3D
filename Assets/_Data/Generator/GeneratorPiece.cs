using Unity.Burst.CompilerServices;
using UnityEngine;

public class GeneratorPiece: Singleton<GeneratorPiece>
{

    protected GameObject WhitePawnGameObject;
    protected GameObject WhiteRookGameObject;
    protected GameObject WhiteKnightGameObject;
    protected GameObject WhiteBishopGameObject;
    protected GameObject WhiteQueenGameObject;
    protected GameObject WhiteKingGameObject;

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
        this.WhiteBishopGameObject = GameObject.Find("Prefab_WhiteBishop");
        this.WhiteQueenGameObject = GameObject.Find("Prefab_WhiteQueen");
        this.WhiteKingGameObject = GameObject.Find("Prefab_WhiteKing");

        this.BlackPawnGameObject = GameObject.Find("Prefab_BlackPawn");
        this.BlackRookGameObject = GameObject.Find("Prefab_BlackRook");
        this.BlackKnightGameObject = GameObject.Find("Prefab_BlackKnight");
        this.BlackBishopGameObject = GameObject.Find("Prefab_BlackBishop");
        this.BlackQueenGameObject = GameObject.Find("Prefab_BlackQueen");
        this.BlackKingGameObject = GameObject.Find("Prefab_BlackKing");

    }

    public void Generate()
    {
        // Generate piece
        for(int i = 1; i >= -1; i-=2)
        {
            Vector2Int newPos2D;
            Vector3Int newPos3D;
            for (int j = 1; j <= 8 ; j++)
            {
                GameObject newPieceGameObject;
                newPieceGameObject = GameObject.Instantiate((i == Const.SIDE_WHITE) ? WhitePawnGameObject : BlackPawnGameObject);
                newPieceGameObject.name = Method2.NamePiece(i,"Pawn", j);
                newPieceGameObject.transform.parent = GameObject.Find("Pieces").transform;
                Pawn piece = newPieceGameObject.GetComponent<Pawn>();
                newPos2D = new Vector2Int((Const.MAX_BOARD_SIZE - 8 ) / 2 + j
                                                    , (i == Const.SIDE_WHITE) ? 2 : Const.MAX_BOARD_SIZE - 1);
                newPos3D = Method2.Pos2dToPos3d(newPos2D, SearchingMethod.HeightOfSquare(newPos2D));
                piece.SetPosition(newPos3D);
            }

            for (int j = 1; j <= 2; j++)
            {
                GameObject newRook = GameObject.Instantiate((i == Const.SIDE_WHITE) ? WhiteRookGameObject : BlackRookGameObject);
                GameObject newKnight = GameObject.Instantiate((i == Const.SIDE_WHITE) ? WhiteKnightGameObject : BlackKnightGameObject);
                GameObject newBishop = GameObject.Instantiate((i == Const.SIDE_WHITE) ? WhiteBishopGameObject : BlackBishopGameObject);
                
                newRook.name = Method2.NamePiece(i, "Rook", j);
                newKnight.name = Method2.NamePiece(i, "Knight", j);
                newBishop.name = Method2.NamePiece(i, "Bishop", j);
                
                newRook.transform.parent = GameObject.Find("Pieces").transform;
                newKnight.transform.parent = GameObject.Find("Pieces").transform;
                newBishop.transform.parent = GameObject.Find("Pieces").transform;

                Rook rook = newRook.GetComponent<Rook>();
                Knight knight = newKnight.GetComponent<Knight>();
                Bishop bishop = newBishop.GetComponent<Bishop>();

                newPos2D = new Vector2Int((Const.MAX_BOARD_SIZE - 8) / 2 + ((j == 1) ? 1 : 8)
                                                    , (i == Const.SIDE_WHITE) ? 1 : Const.MAX_BOARD_SIZE);
                newPos3D = Method2.Pos2dToPos3d(newPos2D, SearchingMethod.HeightOfSquare(newPos2D));
                
                rook.SetPosition(newPos3D);

                newPos2D = new Vector2Int((Const.MAX_BOARD_SIZE - 8) / 2 + ((j == 1) ? 2 : 7)
                                    , (i == Const.SIDE_WHITE) ? 1 : Const.MAX_BOARD_SIZE);
                newPos3D = Method2.Pos2dToPos3d(newPos2D, SearchingMethod.HeightOfSquare(newPos2D));
                knight.SetPosition(newPos3D);

                newPos2D = new Vector2Int((Const.MAX_BOARD_SIZE - 8) / 2 + ((j == 1) ? 3 : 6)
                                    , (i == Const.SIDE_WHITE) ? 1 : Const.MAX_BOARD_SIZE);
                newPos3D = Method2.Pos2dToPos3d(newPos2D, SearchingMethod.HeightOfSquare(newPos2D));
                bishop.SetPosition(newPos3D);
            }
            GameObject newQueen = GameObject.Instantiate((i == Const.SIDE_WHITE) ? WhiteQueenGameObject : BlackQueenGameObject);
            GameObject newKing  = GameObject.Instantiate((i == Const.SIDE_WHITE) ? WhiteKingGameObject : BlackKingGameObject);
            
            newQueen.name = Method2.NamePiece(i, "Queen", 1);
            newKing.name = Method2.NamePiece(i, "King", 0);

            newQueen.transform.parent = GameObject.Find("Pieces").transform;
            newKing.transform.parent = GameObject.Find("Pieces").transform;

            Queen queen = newQueen.GetComponent<Queen>();
            King king = newKing.GetComponent<King>();

            newPos2D = new Vector2Int((Const.MAX_BOARD_SIZE - 8) / 2 + 4
                                , (i == Const.SIDE_WHITE) ? 1 : Const.MAX_BOARD_SIZE);
            newPos3D = Method2.Pos2dToPos3d(newPos2D, SearchingMethod.HeightOfSquare(newPos2D));
            queen.SetPosition(newPos3D);

            newPos2D = new Vector2Int((Const.MAX_BOARD_SIZE - 8) / 2 + 5
                                , (i == Const.SIDE_WHITE) ? 1 : Const.MAX_BOARD_SIZE);
            newPos3D = Method2.Pos2dToPos3d(newPos2D, SearchingMethod.HeightOfSquare(newPos2D));
            king.SetPosition(newPos3D);
        }
    }
}