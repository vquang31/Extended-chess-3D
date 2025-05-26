// Assets/Scripts/NetworkLauncher.cs

using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class NetworkLauncher : MonoBehaviour
{
    public GameObject playerPrefab;
    public Button hostButton;
    public Button clientButton;
    public Button serverButton;

    void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;

        hostButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
        });

        clientButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });

        serverButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartServer();
        });
    }

    void OnClientConnected(ulong clientId)
    {
        // Spawn player only if server or host
        if (NetworkManager.Singleton.IsServer)
        {
            var player = Instantiate(playerPrefab);
            player.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);
        }
    }
}
