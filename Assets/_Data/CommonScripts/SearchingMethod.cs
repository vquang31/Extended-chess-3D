using Unity.VisualScripting;
using UnityEngine;
using Mirror;
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
            return false;
        return true;
    }


    static public bool CanStandOnSquare(Vector2Int pos)
    {
        if(IsSquareValid(pos) == false) return false;
        if (IsSquareEmpty(pos)) return true;
        Square square = FindSquareByPosition(pos);
        if (square.ObjectGameObject.TryGetComponent<BuffItem>(out BuffItem x) == true)
            return true;
        return false;
    }


    // phải kiểm tra xem square có phải là square hợp lệ không
    static public bool IsSquareEmpty(Vector2Int pos)
    {
        Square square = FindSquareByPosition(pos);
        if(square == null) return false;
        if (square.ObjectGameObject == null) return true;
        return false;
    }
    static public Square FindSquareByPosition(Vector2Int pos)
    {
        if(IsSquareValid(pos) == false) {  
            return null; 
        }
        return GameObject.Find(Method2.NameSquare(pos)).GetComponent<Square>();
    }
    static public Piece FindPieceByPosition(Vector2Int pos)
    {
        if (IsSquareValid(pos) == false) return null;
        GameObject GO = FindSquareByPosition(pos).ObjectGameObject;
        if(GO == null) return null; 
        if (GO.TryGetComponent<Piece>(out var piece) == false)
        {
            return null;
        }
        if (piece == null)
        {
            return null;
        }
        return piece.GetComponent<Piece>();
    }


    /// 3D -> 2D
    static public bool IsSquareValid(Vector3Int pos)
    {
        return IsSquareValid(ConvertMethod.Pos3dToPos2d(pos));
    }
    static public bool IsSquareEmpty(Vector3Int pos)
    {
        return IsSquareEmpty(ConvertMethod.Pos3dToPos2d(pos));
    }
    static public Square FindSquareByPosition(Vector3Int pos)
    {
        return FindSquareByPosition(ConvertMethod.Pos3dToPos2d(pos));
    }
    static public Piece FindPieceByPosition(Vector3Int pos)
    {
        return FindPieceByPosition(ConvertMethod.Pos3dToPos2d(pos));
    }
    /// NetworkManager
    /// 
    static public GameObject FindRegisteredPrefab(string prefabName)
    {
        foreach (GameObject prefab in NetworkManager.singleton.spawnPrefabs)
        {
            if (prefab.name == prefabName)
                return prefab;
        }
        return null;
    }


}
