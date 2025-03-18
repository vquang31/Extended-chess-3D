using Unity.VisualScripting;
using UnityEngine;

public class SearchingMethod
{

    static public int HeightOfSquare(Vector2Int pos)
    {
        Square square = GameObject.Find(Method2.NameSquare(pos)).GetComponent<Square>();
        return square.Position.y;
    }
    static public bool IsSquareValid(Vector2Int pos)
    {
        if (pos.x < 1 || pos.x > Const.MAX_BOARD_SIZE || pos.y < 1 || pos.y > Const.MAX_BOARD_SIZE)
        {
            return false;
        }
        return true;
    }
    static public bool IsSquareEmpty(Vector2Int pos)
    {
        if(IsSquareValid(pos) == false)
        {
            return false;
        }   
        return FindSquareByPosition(pos).PieceGameObject == null;
    }
    static public Square FindSquareByPosition(Vector2Int pos)
    {
        return GameObject.Find(Method2.NameSquare(pos)).GetComponent<Square>();
    }
    static public Piece FindPieceByPosition(Vector2Int pos)
    {
        GameObject piece = FindSquareByPosition(pos).PieceGameObject;
        if (piece == null)
        {
            return null;
        }
        return piece.GetComponent<Piece>();
    }


    /// 3D -> 2D
    static public bool IsSquareValid(Vector3Int pos)
    {
        return IsSquareValid(Method2.Pos3dToPos2d(pos));
    }
    static public bool IsSquareEmpty(Vector3Int pos)
    {
        return IsSquareEmpty(Method2.Pos3dToPos2d(pos));
    }
    static public Square FindSquareByPosition(Vector3Int pos)
    {
        return FindSquareByPosition(Method2.Pos3dToPos2d(pos)).GetComponent<Square>();
    }
    static public Piece FindPieceByPosition(Vector3Int pos)
    {
        return FindPieceByPosition(Method2.Pos3dToPos2d(pos));
    }

}
