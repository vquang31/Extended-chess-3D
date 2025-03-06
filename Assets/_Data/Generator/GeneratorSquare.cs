using UnityEngine;

public class GeneratorSquare : Singleton<Generator>
{
    // White
    private GameObject _squarePrefab1;
    // Black
    private GameObject _squarePrefab2;

    protected override void LoadComponents()
    {
        this._squarePrefab1 = GameObject.Find("Prefab_WhiteSquare");
        this._squarePrefab2 = GameObject.Find("Prefab_BlackSquare");
        Generate();
    }

    protected void Generate()
    {
        Debug.Log("Generate Square"); 
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
                square.InitRandomHeight(new Vector2Int(x,z));

                isWhite = !isWhite;
            }
            isWhite = !isWhite;
        }
    }
}