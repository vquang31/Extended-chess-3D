using Mirror;
using UnityEngine;

public class ChessNetwork : NetworkBehaviour
{
    [SyncVar] private Vector3 syncedPosition;

    public void RequestMove(Vector3 newPos)
    {
        //    if (hasAuthority)
        //    {
        //        CmdMove(newPos);
        //    }
    }

    [Command]
    private void CmdMove(Vector3 newPos)
    {
        syncedPosition = newPos;
        transform.position = newPos;
    }

    private void Update()
    {
        //if (!hasAuthority)
        //    transform.position = syncedPosition;
    }
}