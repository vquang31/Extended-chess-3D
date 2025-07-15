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
     *      Không thể dùng được StartCoroutine
     *   [ClientRpc] Server -> All Clients
     *   [Client]  anyone -> Client only
     *   [Server]  anyone -> Server only (call 1 time )
     * 
     */

    /*
        Cách xử lý khi muốn đồng bộ dữ liệu  -- UI
                [ClientRpc]
                public void RpcUpdateActionPointBar()
                {
                    StartCoroutine(UpdateActionPointBarRoutine());
                }

                IEnumerator UpdateActionPointBarRoutine()
                {
                    yield return new WaitForSeconds(0.1f); // wait for a short time to ensure UI is ready
                    UpdateActionPointBar();
                }

                public void UpdateActionPointBar()
                {
                    pointTurnBar.SetPoint(GetPlayablePlayer().ActionPoint);
                }
     */


    /*
        Cách xử lý khi muốn gửi request từ client đến server
        C1:                                và Server xử lý
        

                [Command(requiresAuthority = false)]
                public void CmdChangeHeight(int n, float duration)
                {
                    ChangeHeight(n, duration);
                }
                [Server]
                public void ChangeHeight(int n , float duration)
                {
                    StartCoroutine(ChangeHeightRoutine(n,duration));

                    if(pieceGameObject != null)
                    {
                        pieceGameObject?.GetComponent<Piece>().ChangeHeight(n, duration);
                    }
                    if(_buffItem != null)
                        _buffItem?.ChangeHeight(n, duration);
                }
                IEnumerator ChangeHeightRoutine(int n , float duration) // animation - position
                {
                    Vector3 start = transform.position;
                    Vector3 target = start + Vector3.up * (float) n / 2;

                    float elapsed = 0f;
                    while (elapsed < duration)
                    {
                        transform.position = Vector3.Lerp(start, target, elapsed / duration);
                        elapsed += Time.deltaTime;
                        yield return null; // Chờ 1 frame trước khi tiếp tục
                    }
                    transform.position = target; // Đảm bảo đạt đúng vị trí../ m,7
                    SetPosition(new Vector3Int(Position.x, Position.y + n, Position.z));
                }


        C2:     và Server xử lý và Các Client cùng thực hiện một hành động nào đó


                [Command(requiresAuthority = false)]
                private void CmdAnimatorSetTrigger(string nameTrigger)
                {
                    RpcAnimatorSetTrigger(nameTrigger);
                }
                [ClientRpc]
                private void RpcAnimatorSetTrigger(string nameTrigger)
                {
                    animator.SetTrigger(nameTrigger);
                }
     
     
     
     */


}