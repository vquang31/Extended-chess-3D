using Unity.VisualScripting;
using UnityEngine;

public class SearchingMethod
{

    static public int HeightOfSquare(Vector2Int pos)
    {
        Square square = GameObject.Find(Method2.NameSquare(pos)).GetComponent<Square>();
        return square.Position.y;
    }
}
