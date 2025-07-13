using UnityEngine;
using Mirror;

public class NewNetworkBehaviour : NetworkBehaviour
{

    protected virtual void Awake()
    {
        this.LoadComponents();
    }
    // Dont use this method in child class
    // if use this method,  you must call base.Awake() in child class
    //                      and don't call LoadComponents() again

    public override void OnStartClient()
    {
        base.OnStartClient();
        this.LoadComponents();
    }
    protected virtual void Start()
    {
    }

    protected virtual void Reset()
    {
        this.LoadComponents();
        this.ResetValues();
    }

    // sử dụng khi awake hoặc reset
    // load các component cần thiết
    protected virtual void LoadComponents()
    {

    }

    protected virtual void ResetValues()
    {

    }

    
    public void Delete()
    {
        Destroy(gameObject);
    }


    ////
    ///
    /*
     *   [Command]  Client (has authority - isOnwer) -> Server (call n times)
     *   [ClientRpc] Server -> All Clients
     *   [Client]  anyone -> Client only
     *   [Server]  anyone -> Server only (call 1 time )
     * 
     */



}