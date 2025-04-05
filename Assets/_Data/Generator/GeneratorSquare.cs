using Mono.Cecil;
using UnityEngine;

public class GeneratorSquare : Singleton<GeneratorSquare>
{
    [SerializeField] public bool log = false;
    // White
    private GameObject _squarePrefab1;
    // Black
    private GameObject _squarePrefab2;

    public GameObject SquarePrefab1 => _squarePrefab1;

    public GameObject SquarePrefab2 => _squarePrefab2;


    protected override void LoadComponents()
    {
        this._squarePrefab1 = GameObject.Find("Prefab_WhiteSquare");
        this._squarePrefab2 = GameObject.Find("Prefab_BlackSquare");
    }

    public void Generate()
    {
        if(log) Debug.Log("Generate Square"); 
        // Generate square
        bool isWhite = false;
        for (int z = 1; z <= Const.MAX_BOARD_SIZE; z++)
        {
            for (int x = 1; x <= Const.MAX_BOARD_SIZE; x++)
            {
                GameObject newSquareGameObject;
                newSquareGameObject = GameObject.Instantiate((isWhite) ? _squarePrefab1 : _squarePrefab2);
                newSquareGameObject.name = Method2.NameSquare(x, z);
                newSquareGameObject.transform.parent = GameObject.Find("BoardSquare").transform;
                
                Square square = newSquareGameObject.GetComponent<Square>();   
                square.InitHeight(new Vector2Int(x,z));

                isWhite = !isWhite;
            }
            isWhite = !isWhite;
        }
        GenerateMap();

    }
    protected void GenerateMap()
    {
        //GenerateMountain(new Vector3Int(8, 8, 8));
        //GenerateMountain(new Vector3Int(6, 8, 8));
        //GenerateMountain(new Vector3Int(10, 8, 8));

        //GenerateMountains(24, 4, 10);
        //UpHeightSquares(10, 1, 4);
    }

    protected void GenerateMountains(int n,int minHeight, int maxHeight)
    {
        if (log) Debug.Log("Generate Mountain");
        for (int i = 1; i <= n; i++)
        {
            int x = Random.Range(2, Const.MAX_BOARD_SIZE -1 + 1);
            int z = Random.Range(2, Const.MAX_BOARD_SIZE -1 + 1);
            int y = Random.Range(minHeight, maxHeight + 1);
            if(log)Debug.Log(Method2.NameSquare(new Vector2Int(x, z)));
            GenerateMountain(new Vector3Int(x, y, z));
        }
    }



    /// <summary>
    ///  pos.x: x,
    ///  pos.y: height of Mountain,
    ///  pos.z: y 
    /// </summary>
    /// <param name="pos"></param>
    protected void GenerateMountain(Vector3Int pos)
    {
        int maxHeight = pos.y;
        int height = 0;
        for (int x = -maxHeight + 1; x < maxHeight; x++)
        {
            if (x <= 0) height++;
            else height--;

            int currentHeight = 0;
            bool plus = true;

            for (int z = -height + 1; z < height; z++)
            {
                if (currentHeight == height)
                {
                    plus = false;
                }
                if(plus) currentHeight++;
                else currentHeight--;

                if (!SearchingMethod.IsSquareValid(new Vector2Int(pos.x + x, pos.z + z)))
                    continue;
                Square square = GameObject.Find(Method2.NameSquare(new Vector2Int(pos.x + x, pos.z + z))).GetComponent<Square>();
                // Do not change this agolrithm
                if (square.Position.y < currentHeight)
                {
                    if (square.Position.y + currentHeight <= maxHeight && square.Position.y != 1)
                    {
                        square.SetPosition(new Vector3Int(pos.x + x, (square.Position.y + currentHeight) / 2, pos.z + z));
                    }
                    else
                    {
                        square.SetPosition(new Vector3Int(pos.x + x, currentHeight, pos.z + z));
                    }
                }

                //if (square.Position.y < currentHeight)
                //{
                //    square.SetPosition(new Vector3Int(pos.x + x, currentHeight, pos.z + z));
                //}

                //if (square.Position.y < currentHeight)
                //{
                //    if (square.Position.y != 1)
                //    {
                //        square.SetPosition(new Vector3Int(pos.x + x, (square.Position.y + currentHeight) / 2, pos.z + z));
                //    }
                //    else
                //    {
                //        square.SetPosition(new Vector3Int(pos.x + x, currentHeight, pos.z + z));
                //    }
                //}


            }
        }
    }

    protected void UpHeightSquares(int n,int minHeight, int maxHeight)
    {
        if(log) Debug.Log("Up Height Square");
        for (int i = 1; i <= n; i++)
        {
            int x = Random.Range(1, Const.MAX_BOARD_SIZE + 1);
            int z = Random.Range(1, Const.MAX_BOARD_SIZE + 1);
            int y = Random.Range(minHeight, maxHeight + 1);
            if(log) Debug.Log(Method2.NameSquare(new Vector2Int(x, z)));
            UpHeightSquare(new Vector2Int(x, z), y);
        }
    }


    protected void UpHeightSquare(Vector2Int pos, int upHeight)
    {
        Square square = GameObject.Find(Method2.NameSquare(pos)).GetComponent<Square>();
        square.SetPosition(new Vector3Int(pos.x, square.Position.y + upHeight, pos.y));
    }

}