using Mirror;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : NetworkSingleton<GameManager>
{
    [SyncVar]public List<Piece> pieces = new();
    [SyncVar]public List<Tower> towers = new();

    [ClientRpc]
    public void RpcEndGame(int loseSide)
    {
        Debug.Log($"Game Over for side: {loseSide}");
        TurnManager.Instance.EndGame(loseSide);
    }
}
