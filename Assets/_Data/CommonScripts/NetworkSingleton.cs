using Mirror;
using UnityEngine;

public abstract class NetworkSingleton<T> : NewNetworkBehaviour where T : NewNetworkBehaviour
{
    public static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null) Debug.LogError("Singleton instance has not been created yet!");
            return _instance;
        }
    }

    protected override void Awake()
    {
        LoadInstance();
        LoadComponents();
    }

    protected virtual void LoadInstance()
    {
        if (_instance == null)
        {
            _instance = this as T;
            if (transform.parent == null) DontDestroyOnLoad(gameObject);
            return;
        }

        if (_instance != this) Debug.LogError("Another instance of SingletonExample already exists!");
    }

}
