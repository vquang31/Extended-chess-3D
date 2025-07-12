using Mirror;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : NetworkSingleton<GameManager>
{
    [SyncVar]public List<Piece> pieces = new();


    [ClientRpc]
    public void RpcSetPieceParent(NetworkIdentity pieceNetId)
    {
        GameObject pieceGO = pieceNetId.gameObject;
        Transform parent = GameObject.Find("Pieces")?.transform;
        if (parent != null)
        {
            pieceGO.transform.SetParent(parent);
        }
    }
}
