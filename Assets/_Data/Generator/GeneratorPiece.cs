using Mirror;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class GeneratorPiece: NetworkSingleton<GeneratorPiece>
{

    public GameObject WhitePawnPrefab;
    protected GameObject WhiteRookPrefab;
    protected GameObject WhiteKnightPrefab;
    protected GameObject WhiteBishopPrefab;
    protected GameObject WhiteQueenPrefab;
    protected GameObject WhiteKingPrefab;

    protected GameObject BlackPawnPrefab;
    protected GameObject BlackRookPrefab;
    protected GameObject BlackKnightPrefab;
    protected GameObject BlackBishopPrefab;
    protected GameObject BlackQueenPrefab;
    protected GameObject BlackKingPrefab;

    [SerializeField] protected GameObject WhitepieceVFX_Prefab;
    [SerializeField] private Animator WhitePieceVFXAnimator;
    [SerializeField] protected GameObject BlackPieceVFX_Prefab;
    [SerializeField] private Animator BlackPieceVFXAnimator;



    protected override void LoadComponents()
    {
        base.LoadComponents();

        
        this.WhitePawnPrefab =  SearchingMethod.FindRegisteredPrefab("Prefab_WhitePawn");
        this.WhiteRookPrefab =  SearchingMethod.FindRegisteredPrefab("Prefab_WhiteRook");
        this.WhiteKnightPrefab = SearchingMethod.FindRegisteredPrefab("Prefab_WhiteKnight");
        this.WhiteBishopPrefab = SearchingMethod.FindRegisteredPrefab("Prefab_WhiteBishop");
        this.WhiteQueenPrefab = SearchingMethod.FindRegisteredPrefab("Prefab_WhiteQueen");
        this.WhiteKingPrefab = SearchingMethod.FindRegisteredPrefab("Prefab_WhiteKing");

        this.BlackPawnPrefab = SearchingMethod.FindRegisteredPrefab("Prefab_BlackPawn");
        this.BlackRookPrefab = SearchingMethod.FindRegisteredPrefab("Prefab_BlackRook");
        this.BlackKnightPrefab = SearchingMethod.FindRegisteredPrefab("Prefab_BlackKnight");
        this.BlackBishopPrefab = SearchingMethod.FindRegisteredPrefab("Prefab_BlackBishop");
        this.BlackQueenPrefab = SearchingMethod.FindRegisteredPrefab("Prefab_BlackQueen");
        this.BlackKingPrefab = SearchingMethod.FindRegisteredPrefab("Prefab_BlackKing");

        this.WhitepieceVFX_Prefab = GameObject.Find("Prefab_WhitePieceVFX");
        WhitePieceVFXAnimator = WhitepieceVFX_Prefab.GetComponent<Animator>();


        this.BlackPieceVFX_Prefab = GameObject.Find("Prefab_BlackPieceVFX");
        BlackPieceVFXAnimator = BlackPieceVFX_Prefab.GetComponent<Animator>();


    }


    public void Generate()
    {
        // Generate piece
        for(int i = 1; i >= -1; i-=2)
        {
            Vector2Int newPos2D;
            for (int j = 1; j <= 8 ; j++)
            {
                GameObject newPieceGameObject;
                newPieceGameObject = GameObject.Instantiate((i == Const.SIDE_WHITE) ? WhitePawnPrefab : BlackPawnPrefab);
                newPieceGameObject.name = Method2.NamePiece(i,"Pawn", j);
                Pawn pawn = newPieceGameObject.GetComponent<Pawn>();
                newPos2D = new Vector2Int((Const.MAX_BOARD_SIZE - 8 ) / 2 + j
                                        , (i == Const.SIDE_WHITE) ? 1 + Random.Range(1,3 + 1) : Const.MAX_BOARD_SIZE - Random.Range(1,3 + 1));
                SaveData(pawn,newPos2D, newPieceGameObject, i);
            }

            for (int j = 1; j <= 2; j++)
            {
                GameObject newRook = GameObject.Instantiate((i == Const.SIDE_WHITE) ? WhiteRookPrefab : BlackRookPrefab);
                GameObject newKnight = GameObject.Instantiate((i == Const.SIDE_WHITE) ? WhiteKnightPrefab : BlackKnightPrefab);
                GameObject newBishop = GameObject.Instantiate((i == Const.SIDE_WHITE) ? WhiteBishopPrefab : BlackBishopPrefab);
                
                newRook.name = Method2.NamePiece(i, "Rook", j);
                newKnight.name = Method2.NamePiece(i, "Knight", j);
                newBishop.name = Method2.NamePiece(i, "Bishop", j);

                Rook rook = newRook.GetComponent<Rook>();
                Knight knight = newKnight.GetComponent<Knight>();
                Bishop bishop = newBishop.GetComponent<Bishop>();

                newPos2D = new Vector2Int((Const.MAX_BOARD_SIZE - 8) / 2 + ((j == 1) ? 1 : 8)
                                                    , (i == Const.SIDE_WHITE) ? 1 : Const.MAX_BOARD_SIZE);
                SaveData(rook, newPos2D, newRook, i );

                newPos2D = new Vector2Int((Const.MAX_BOARD_SIZE - 8) / 2 + ((j == 1) ? 2 : 7)
                                    , (i == Const.SIDE_WHITE) ? 1 : Const.MAX_BOARD_SIZE);
                SaveData(knight, newPos2D, newKnight, i);

                newPos2D = new Vector2Int((Const.MAX_BOARD_SIZE - 8) / 2 + ((j == 1) ? 3 : 6)
                                    , (i == Const.SIDE_WHITE) ? 1 : Const.MAX_BOARD_SIZE);
                SaveData(bishop, newPos2D, newBishop, i);
            }
            GameObject newQueen = GameObject.Instantiate((i == Const.SIDE_WHITE) ? WhiteQueenPrefab : BlackQueenPrefab);
            GameObject newKing  = GameObject.Instantiate((i == Const.SIDE_WHITE) ? WhiteKingPrefab : BlackKingPrefab);
            
            newQueen.name = Method2.NamePiece(i, "Queen", 1);
            newKing.name = Method2.NamePiece(i, "King", 0);

            Queen queen = newQueen.GetComponent<Queen>();
            King king = newKing.GetComponent<King>();

            newPos2D = new Vector2Int((Const.MAX_BOARD_SIZE - 8) / 2 + 4
                                , (i == Const.SIDE_WHITE) ? 1 : Const.MAX_BOARD_SIZE);
            SaveData(queen, newPos2D, newQueen, i );

            newPos2D = new Vector2Int((Const.MAX_BOARD_SIZE - 8) / 2 + 5
                                , (i == Const.SIDE_WHITE) ? 1 : Const.MAX_BOARD_SIZE);
            SaveData(king, newPos2D, newKing , i);

        }
    }

    /// <summary>
    ///  set position for piece
    ///  , set parent for piece
    /// </summary>
    /// <param name="piece"></param>
    /// <param name="newPos2D"></param>
    /// <param name="newPieceGameObject"></param>
    private void SaveData(Piece piece,Vector2Int newPos2D,GameObject newPieceGameObject, int side)
    {
        //newPieceGameObject.transform.parent = GameObject.Find("Pieces").transform;
        newPieceGameObject.AddComponent<Animator>().runtimeAnimatorController
            = (side == Const.SIDE_WHITE) ? WhitePieceVFXAnimator.runtimeAnimatorController  : BlackPieceVFXAnimator.runtimeAnimatorController;

        Vector3Int newPos3D = ConvertMethod.Pos2dToPos3d(newPos2D);
        piece.SetPosition(newPos3D);

        NetworkServer.Spawn(newPieceGameObject);
        GameManager.Instance.RpcSetPieceParent(newPieceGameObject.GetComponent<NetworkIdentity>());
        GameManager.Instance.pieces.Add(piece);   
    }
}