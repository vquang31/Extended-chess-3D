using Mirror;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : NetworkSingleton<GameManager>
{
    [SyncVar]public List<Piece> pieces = new();
    [SyncVar]public List<Tower> towers = new();


}
