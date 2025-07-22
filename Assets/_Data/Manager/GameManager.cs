using Mirror;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : NetworkSingleton<GameManager>
{
    [SyncVar]public List<Piece> pieces = new();
    [SyncVar]public List<Tower> towers = new();

    [ClientRpc]
    public void RpcEndGame(int side)
    {
        Debug.Log($"Game Over for side: {side}");
        TurnManager.Instance.EndGame(side);
    }
}
