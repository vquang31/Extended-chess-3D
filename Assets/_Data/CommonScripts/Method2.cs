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
        return "BuffItem_" + (char)('a' - 1 + pos.x) + pos.z.ToString();
    }




    static public string NamePiece(int side, string name, int index)
    {
        string s = (side == Const.SIDE_WHITE) ? "White" : "Black";
        s = s + name + "_" + ((index != 0) ? index : "");
        return s;
    }

}
