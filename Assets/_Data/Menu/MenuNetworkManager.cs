using Unity.Netcode.Transports.UTP;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class MenuNetworkManager : MonoBehaviour
{

    [SerializeField] private int maxPlayers = 3;
    public TMP_InputField ipInput;
    public TMP_InputField portInput;
    public GameObject statusText;
    public GameObject informationIP;

    public Button hostButton;
    public Button clientButton;
    public Button serverButton;
    public Button loadMainGameButton;


    private void Awake()
    {
        ipInput = GameObject.Find("IP_TextField").GetComponent<TMP_InputField>();
        portInput = GameObject.Find("Port_TextField").GetComponent<TMP_InputField>();
        statusText = GameObject.Find("StatusText");
        informationIP = GameObject.Find("Information");
    }



    void Start()
    {
        // Lắng nghe trạng thái kết nối
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;

        statusText.GetComponent<TextMeshProUGUI>().text = "Chưa kết nối";


        hostButton.onClick.AddListener(() =>
        {
            StartHost();
        });

        clientButton.onClick.AddListener(() =>
        {
            StartClient();
        });

        serverButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.ConnectionApprovalCallback = ApprovalCheck;
            NetworkManager.Singleton.NetworkConfig.ConnectionApproval = true;
            NetworkManager.Singleton.StartServer();
        });


        loadMainGameButton.onClick.AddListener(() =>
        {
            LoadMainGameScene();
        });

    }

    public void StartHost()
    {
        SetConnectionData();

        // Gắn callback để phê duyệt kết nối
        //NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
        //NetworkManager.Singleton.ConnectionApprovalCallback = ApprovalCheck;
        //NetworkManager.Singleton.NetworkConfig.ConnectionApproval = true; // <<< BẮT BUỘC PHẢI BẬT
        bool started = NetworkManager.Singleton.StartHost();
        statusText.GetComponent<TextMeshProUGUI>().text = started ? "Hosting..." : "Host failed!";

        informationIP.GetComponent<TextMeshProUGUI>().text =
            $"IP: {NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address}\n" +
            $"Port: {NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Port}";

    }


    public void StartClient()
    {
        SetConnectionData();

        bool started = NetworkManager.Singleton.StartClient();
        statusText.GetComponent<TextMeshProUGUI>().text = started ? "Đang kết nối..." : "Client failed!";
    }

    void SetConnectionData()
    {
        var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        string ip = ipInput.text;
        ushort port = ushort.Parse(portInput.text);
        transport.SetConnectionData(ip, port);
    }

    private void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {
        int connectedClients = NetworkManager.Singleton.ConnectedClients.Count;

        Debug.Log($"[APPROVAL] Số client hiện tại: {connectedClients}");

        if (connectedClients >= maxPlayers - 1)
        {
            response.Approved = false;
            response.CreatePlayerObject = false;
            response.Reason = "Đã đầy người chơi!";
            response.Pending = false;
            Debug.LogWarning($"[APPROVAL] Từ chối client vì đã đủ số lượng: {response.Reason}");
        }
        else
        {
            response.Approved = true;
            response.CreatePlayerObject = true;
            response.Pending = false;
            Debug.Log("[APPROVAL] Chấp nhận client.");
        }

       
    }




    void OnClientConnected(ulong clientId)
    {
        if (clientId == NetworkManager.Singleton.LocalClientId)
        {
            statusText.GetComponent<TextMeshProUGUI>().text = "Đã kết nối!";
        }
    }

    void OnClientDisconnected(ulong clientId)
    {
        if (clientId == NetworkManager.Singleton.LocalClientId)
        {
            string reason = NetworkManager.Singleton.DisconnectReason;

            if (string.IsNullOrEmpty(reason))
            {
                reason = "Mất kết nối!";
            }

            statusText.GetComponent<TextMeshProUGUI>().text = reason;
        }
    }

    public void LoadMainGameScene()
    {
        if (NetworkManager.Singleton.IsServer || NetworkManager.Singleton.IsHost)
        {
            NetworkManager.Singleton.SceneManager.LoadScene("MainGameScene", LoadSceneMode.Single);
        }
        else
        {
            statusText.GetComponent<TextMeshProUGUI>().text = "Chỉ Host mới được chuyển scene!";
        }
    }
}