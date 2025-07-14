using Mirror;
using NUnit.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GeneratorSquare : NetworkSingleton<GeneratorSquare>
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
        _squarePrefab1 = SearchingMethod.FindRegisteredPrefab("Prefab_WhiteSquare");
        _squarePrefab2 = SearchingMethod.FindRegisteredPrefab("Prefab_BlackSquare");
    }

    public void Generate()
    {
        // Generate square
        bool isWhite = false;
        for (int z = 1; z <= Const.MAX_BOARD_SIZE; z++)
        {
            for (int x = 1; x <= Const.MAX_BOARD_SIZE; x++)
            {
                GameObject newSquareGameObject;
                newSquareGameObject = Instantiate((isWhite) ? _squarePrefab1 : _squarePrefab2);
                NetworkServer.Spawn(newSquareGameObject);
                GameManager.Instance.RpcSpawnSquare
                    (newSquareGameObject.GetComponent<NetworkIdentity>(), new Vector2Int(x, z));

                isWhite = !isWhite;
            }
            isWhite = !isWhite;
        }

    }
    public void GenerateMap()
    {
        //GenerateMountain(new Vector3Int(8, 8, 8));
        //GenerateMountain(new Vector3Int(6, 8, 8));
        //GenerateMountain(new Vector3Int(10, 8, 8));

        List<Vector3Int> listPosition1 = GenerateMountains(4, 4, 10);

        List<Vector3Int> listPosition2 = GenerateUpHeightSquares(10, 1, 4);

        listPosition1.AddRange(listPosition2);

        listPosition1 = Filter(listPosition1);

        UpHeightSquares(listPosition1);

    }

    private List<Vector3Int> GenerateMountains(int n,int minHeight, int maxHeight)
    {
        List<Vector3Int> listPosition = new List<Vector3Int>();
        if (log) UnityEngine.Debug.Log("Generate Mountain");
        for (int i = 1; i <= n; i++)
        {
            int x = Random.Range(2, Const.MAX_BOARD_SIZE -1 + 1);
            int z = Random.Range(2, Const.MAX_BOARD_SIZE -1 + 1);
            int y = Random.Range(minHeight, maxHeight + 1);
            if(log)UnityEngine.Debug.Log(Method2.NameSquare(new Vector2Int(x, z)));
            List<Vector3Int> listPosition1 = GenerateMountain(new Vector3Int(x, y, z));
            listPosition.AddRange(listPosition1);
        }
        return listPosition;
    }



    /// <summary>
    ///  pos.x: x,
    ///  pos.y: height of Mountain,
    ///  pos.z: y 
    /// </summary>
    /// <param name="pos"></param>
    private List<Vector3Int> GenerateMountain(Vector3Int pos)
    {
        List<Vector3Int> listPosition = new List<Vector3Int>();
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
                        
                Square square = SearchingMethod.FindSquareByPosition(new Vector2Int(pos.x + x, pos.z + z));
                // Do not change this agolrithm
                if (square.Position.y < currentHeight)
                {
                    //Debug.Log(square.Position.x + " " + square.Position.z);
                    if (square.Position.y + currentHeight <= maxHeight && square.Position.y != 1)
                    {
                        listPosition.Add(new Vector3Int(pos.x + x, (square.Position.y + currentHeight) / 2, pos.z + z));
                        
                        // cách này đúng nhưng không có animation
                        //square.SetPosition(new Vector3Int(pos.x + x, (square.Position.y + currentHeight) / 2, pos.z + z)); 

                        // cách này sai do hàm ChangeHeightRoutine bị ghi đè 
                        // tăng (square.Position.y + currentHeight) / 2 - square.Position.y  đơn vị
                        // -> giá trị mới = (square.Position.y + currentHeight) / 2
                        //square.ChangeHeight((square.Position.y + currentHeight) / 2 - square.Position.y, Const.CHANGE_HEIGHT_INIT_DURATION);  
                    }
                    else
                    {
                        listPosition.Add(new Vector3Int(pos.x + x, currentHeight, pos.z + z));
                        // cách này sai do hàm ChangeHeightRoutine bị ghi đè
                        //square.ChangeHeight(currentHeight - square.Position.y, Const.CHANGE_HEIGHT_INIT_DURATION); // giá trị mới = currentHeight đơn vị
                        
                       
                        // cách này đúng nhưng không có animation
                        //square.SetPosition(new Vector3Int(pos.x + x, currentHeight, pos.z + z)); 


                    }
                }
            }
        }
        return listPosition;
    }

    private List<Vector3Int> GenerateUpHeightSquares(int n,int minHeight, int maxHeight)
    {
        List<Vector3Int> listPosition = new List<Vector3Int>();
        if (log) UnityEngine.Debug.Log("Up Height Square");
        for (int i = 1; i <= n; i++)
        {
            int x = Random.Range(1, Const.MAX_BOARD_SIZE + 1);
            int z = Random.Range(1, Const.MAX_BOARD_SIZE + 1);
            int y = Random.Range(minHeight, maxHeight + 1);
            if(log) UnityEngine.Debug.Log(Method2.NameSquare(new Vector2Int(x, z)));
            listPosition.Add(new Vector3Int(x, y,z));
        }
        return listPosition;
    }

    private List<Vector3Int> Filter (List<Vector3Int> listPosition)
    {        
        static Vector3Int Find(Vector3Int pos ,List<Vector3Int> listPosition1)
        {
            Vector3Int result = new(-1, -1, -1); 
            foreach (Vector3Int pos1 in listPosition1)
            {
                if (pos.x == pos1.x && pos.z == pos1.z)
                {
                     return pos1;
                }
            }
            return result;
        }

        List<Vector3Int> listPosition1 = new List<Vector3Int>();
        foreach (Vector3Int pos in listPosition)
        {
            Vector3Int pos1 = Find(pos, listPosition1);
            if (pos1 == new Vector3Int(-1, -1, -1))
            {
                listPosition1.Add(pos);
            }
            else
            {
                if(pos.y > pos1.y)
                {
                    listPosition1.Remove(pos1);
                    listPosition1.Add(pos);
                }
            }
        }
        return listPosition1;
    }

    private void UpHeightSquares(List<Vector3Int> listPosition1)
    {
        foreach (Vector3Int pos in listPosition1)
        {
            if (log) UnityEngine.Debug.Log(Method2.NameSquare(new Vector2Int(pos.x, pos.z)) + " " + pos.y);
            UpHeightSquare(pos);
        }
    }

    private void UpHeightSquare(Vector3Int pos)
    {
        Square square = SearchingMethod.FindSquareByPosition(pos);
        //square.SetPosition(new Vector3Int(pos.x, pos.y - 1, pos.z));
        square.CmdChangeHeight(pos.y - 1, Const.CHANGE_HEIGHT_INIT_DURATION);

    }

}