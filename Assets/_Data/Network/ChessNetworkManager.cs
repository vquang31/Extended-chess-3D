using Mirror;
using UnityEngine;

public class ChessNetworkManager : NetworkManager
{
    private int nextTeam = Const.SIDE_WHITE;

    [Header("Custom Prefabs")]
    public GameObject onlineManagerPrefab;

    private GameObject spawnedOnlineManager;


    public override void OnStartServer()
    {
        base.OnStartServer();

        if (spawnedOnlineManager == null)
        {
            spawnedOnlineManager = Instantiate(onlineManagerPrefab);
            NetworkServer.Spawn(spawnedOnlineManager);
        }
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn) // chỉ chạy trên server
    {
        GameObject playerGO = Instantiate(playerPrefab);
        Player player = playerGO.GetComponent<Player>();
        player._side = nextTeam;

        NetworkServer.AddPlayerForConnection(conn, playerGO);
        // Gán player vào TurnManager
        TurnManager turnManager = Object.FindAnyObjectByType<TurnManager>();
        if (turnManager != null)
        {
            if (nextTeam == Const.SIDE_WHITE)
            {
                turnManager.player1 = player;
            }
            else
            {
                turnManager.player2 = player;
            }
            if(turnManager.player1 != null && turnManager.player2 != null) // Nếu đủ người 
            {
                turnManager.SetReady();
                TurnManager.Instance.RpcSetActiveOfflineManager();
            }
            //turnManager.TargetSetActiveOfflineManager(conn);
        }
        if(nextTeam == Const.SIDE_WHITE)
        {
            nextTeam = Const.SIDE_BLACK;
        }
        else
        {
            nextTeam = Const.SIDE_WHITE;
        }
    }


}