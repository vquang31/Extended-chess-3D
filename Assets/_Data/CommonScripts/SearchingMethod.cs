using Unity.VisualScripting;
using UnityEngine;

public class SearchingMethod
{

    static public int HeightOfSquare(Vector2Int pos)
    {
        Square square = GameObject.Find(Method2.NameSquare(pos)).GetComponent<Square>();
        return square.Position.y;
    }


    static public bool IsCellValid(Vector2Int pos)
    {
        if (pos.x < 1 || pos.x > Const.MAX_BOARD_SIZE || pos.y < 1 || pos.y > Const.MAX_BOARD_SIZE)
        {
            return false;
        }
        return true;
    }
}
