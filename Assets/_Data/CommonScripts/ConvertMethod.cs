using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class ConvertMethod
{
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
