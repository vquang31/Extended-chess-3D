using Mirror;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : NetworkSingleton<GameManager>
{
    [SyncVar]public List<Piece> pieces = new();




    [ClientRpc]
    public void RpcSpawnSquare(NetworkIdentity newItemBuffId , Vector2Int v)
    {
        GameObject newSquareGO = newItemBuffId.gameObject;
        newSquareGO.name = Method2.NameSquare(v);
        newSquareGO.transform.parent = GameObject.Find("BoardSquare").transform;

        Square square = newSquareGO.GetComponent<Square>();
        square.InitHeight(v);
    }



}
