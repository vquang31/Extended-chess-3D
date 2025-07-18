using Mirror;
using System.Collections;
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

    protected GameObject TowerPrefab;

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

        this.TowerPrefab = SearchingMethod.FindRegisteredPrefab("Prefab_Tower");

        this.WhitepieceVFX_Prefab = GameObject.Find("Prefab_WhitePieceVFX");
        WhitePieceVFXAnimator = WhitepieceVFX_Prefab.GetComponent<Animator>();


        this.BlackPieceVFX_Prefab = GameObject.Find("Prefab_BlackPieceVFX");
        BlackPieceVFXAnimator = BlackPieceVFX_Prefab.GetComponent<Animator>();

    }

    private void GeneratePiece()
    {
        // Generate piece
        for (int i = 1; i >= -1; i -= 2)
        {
            Vector2Int newPos2D;
            for (int j = 1; j <= 8; j++)
            {
                GameObject newPieceGameObject;
                newPieceGameObject = GameObject.Instantiate((i == Const.SIDE_WHITE) ? WhitePawnPrefab : BlackPawnPrefab);

                while (true)
                {
                    newPos2D = new Vector2Int((Const.MAX_BOARD_SIZE - 8) / 2 + j
                                        , (i == Const.SIDE_WHITE) ? 1 + Random.Range(1, 3 + 1) : Const.MAX_BOARD_SIZE - Random.Range(1, 3 + 1));
                    if (SearchingMethod.CanStandOnSquare(newPos2D))
                    {
                        break;
                    }
                }
                SaveData(newPos2D, newPieceGameObject, i, Method2.NamePiece(i, "Pawn", j));
            }

            for (int j = 1; j <= 2; j++)
            {
                GameObject newRook = GameObject.Instantiate((i == Const.SIDE_WHITE) ? WhiteRookPrefab : BlackRookPrefab);
                GameObject newKnight = GameObject.Instantiate((i == Const.SIDE_WHITE) ? WhiteKnightPrefab : BlackKnightPrefab);
                GameObject newBishop = GameObject.Instantiate((i == Const.SIDE_WHITE) ? WhiteBishopPrefab : BlackBishopPrefab);

                newPos2D = new Vector2Int((Const.MAX_BOARD_SIZE - 8) / 2 + ((j == 1) ? 1 : 8)
                                                    , (i == Const.SIDE_WHITE) ? 1 : Const.MAX_BOARD_SIZE);
                SaveData(newPos2D, newRook, i, Method2.NamePiece(i, "Rook", j));

                newPos2D = new Vector2Int((Const.MAX_BOARD_SIZE - 8) / 2 + ((j == 1) ? 2 : 7)
                                    , (i == Const.SIDE_WHITE) ? 1 : Const.MAX_BOARD_SIZE);
                SaveData(newPos2D, newKnight, i, Method2.NamePiece(i, "Knight", j));

                newPos2D = new Vector2Int((Const.MAX_BOARD_SIZE - 8) / 2 + ((j == 1) ? 3 : 6)
                                    , (i == Const.SIDE_WHITE) ? 1 : Const.MAX_BOARD_SIZE);
                SaveData(newPos2D, newBishop, i, Method2.NamePiece(i, "Bishop", j));
            }
            GameObject newQueen = GameObject.Instantiate((i == Const.SIDE_WHITE) ? WhiteQueenPrefab : BlackQueenPrefab);
            GameObject newKing = GameObject.Instantiate((i == Const.SIDE_WHITE) ? WhiteKingPrefab : BlackKingPrefab);

            newPos2D = new Vector2Int((Const.MAX_BOARD_SIZE - 8) / 2 + 4
                                , (i == Const.SIDE_WHITE) ? 1 : Const.MAX_BOARD_SIZE);
            SaveData(newPos2D, newQueen, i, Method2.NamePiece(i, "Queen", 1));

            newPos2D = new Vector2Int((Const.MAX_BOARD_SIZE - 8) / 2 + 5
                                , (i == Const.SIDE_WHITE) ? 1 : Const.MAX_BOARD_SIZE);
            SaveData(newPos2D, newKing, i, Method2.NamePiece(i, "King", 0));

        }
    }

    private void GenerateTower()
    {
        // Generate tower
        GameObject newTowerGameObject = GameObject.Instantiate(TowerPrefab);
        Vector2Int newPos2D = new Vector2Int((Const.MAX_BOARD_SIZE + 1) / 2, (Const.MAX_BOARD_SIZE + 1) / 2);
        SaveData(newPos2D, newTowerGameObject, 0, Method2.NameTower() + "1");


        // 1 / 9 
        newTowerGameObject = GameObject.Instantiate(TowerPrefab);
        while (true)
        {
            newPos2D = new Vector2Int(Random.Range(2, Const.MAX_BOARD_SIZE / 3 + 2 ),
                                    Random.Range(2, Const.MAX_BOARD_SIZE / 3 + 2 ));
            if (SearchingMethod.IsSquareEmpty(newPos2D))
            {
                SaveData(newPos2D, newTowerGameObject, 0, Method2.NameTower() + "2");
                break;
            }

        }

        // 9 / 9
        newTowerGameObject = GameObject.Instantiate(TowerPrefab);
        while (true)
        {
            newPos2D = new Vector2Int(Random.Range(Const.MAX_BOARD_SIZE * 2 / 3 - 2, Const.MAX_BOARD_SIZE  + 1 - 1 ),
                                    Random.Range(Const.MAX_BOARD_SIZE * 2 / 3 - 2, Const.MAX_BOARD_SIZE + 1 - 1 ));
            if (SearchingMethod.IsSquareEmpty(newPos2D))
            {
                SaveData(newPos2D, newTowerGameObject, 0, Method2.NameTower() + "3");
                break;
            }
        }
    }

    public void Generate()
    {
        StartCoroutine(GenerateRoutine());
    }
    private IEnumerator GenerateRoutine()
    {
        GeneratePiece();
        yield return new WaitForSeconds(0.5f);
        GenerateTower();
    }
    /// <summary>
    ///  set position for piece
    ///  , set parent for piece
    /// </summary>
    /// <param name="piece"></param>
    /// <param name="newPos2D"></param>
    /// <param name="newPieceGameObject"></param>
    private void SaveData(Vector2Int newPos2D,GameObject newGameObject, int side, string nameGO)
    {
        Vector3Int newPos3D = ConvertMethod.Pos2dToPos3d(newPos2D);
        NetworkServer.Spawn(newGameObject);
        RpcSetPieceOrTower(newGameObject.GetComponent<NetworkIdentity>() , newPos3D, side, nameGO);
    }

    [ClientRpc]
    public void RpcSetPieceOrTower(NetworkIdentity netId, Vector3Int newPos3D, int side, string nameGO)
    {
        GameObject newGO = netId.gameObject;
        newGO.name = nameGO;

        if (side != 0)
        {
            Transform parent = GameObject.Find("Pieces")?.transform;
            if (parent != null)
            {
                newGO.transform.SetParent(parent);
            }

            newGO.AddComponent<Animator>().runtimeAnimatorController
        = (side == Const.SIDE_WHITE) ? WhitePieceVFXAnimator.runtimeAnimatorController : BlackPieceVFXAnimator.runtimeAnimatorController;

            Piece piece = newGO.GetComponent<Piece>();
            piece.SetPosition(newPos3D);
            GameManager.Instance.pieces.Add(piece);
        }
        else
        {
            Transform parent = GameObject.Find("Towers")?.transform;
            if (parent != null)
            {
                newGO.transform.SetParent(parent);
            }
            Tower tower = newGO.GetComponent<Tower>();
            tower.SetPosition(newPos3D);
            Square square = SearchingMethod.FindSquareByPosition(newPos3D);
            square.ObjectGameObject = newGO;
            GameManager.Instance.towers.Add(tower);
        }

 
    }



}