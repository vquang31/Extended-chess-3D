using UnityEngine;

public class Method2
{
    static public string NameSquare(Vector2Int pos)
    {
        return "Square_" + (char)('a' - 1 + pos.x) + pos.y.ToString();
    }

    static public string NameSquare(int x, int y)
    {
        return NameSquare(new Vector2Int(x, y));
    }

    static public string NameItemBuff(Vector2Int pos)
    {
        return "BuffItem_" + (char)('a' - 1 + pos.x) + pos.y.ToString();
    }

    static public string NameItemBuff(Vector3Int pos)
    {
        return "BuffItem_" + (char)('a' - 1 + pos.x) + pos.y.ToString();
    }




    static public string NamePiece(int side, string name, int index)
    {
        string s = (side == Const.SIDE_WHITE) ? "White" : "Black";
        s = s + name + "_" + ((index != 0) ? index : "");
        return s;
    }

    /// <summary>
    /// This Vector3Int is used for 3D position in Unity
    /// </summary>
    /// <param name="pos"> pos.x = x , pos.y = z</param>
    /// <param name="height"></param>
    /// <returns></returns>
    static public Vector3Int Pos2dToPos3d(Vector2Int pos, int height)
    {
        return new Vector3Int(pos.x, height, pos.y);
    }

    /// <summary>
    ///  This method return Vector3Int follow to Vector2Int
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    static public Vector3Int Pos2dToPos3d(Vector2Int pos)
    {
        return new Vector3Int(pos.x, SearchingMethod.HeightOfSquare(pos), pos.y);
    }

    static public Vector2Int Pos3dToPos2d(Vector3Int pos)
    {
        return new Vector2Int(pos.x, pos.z);
    }



}
