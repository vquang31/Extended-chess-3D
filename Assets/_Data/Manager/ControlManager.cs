using Mirror;
using UnityEngine;

public class ControlManager : NetworkSingleton<ControlManager>
{
    [Command(requiresAuthority = false)]
    public void CmdMoveCamera(Vector3 position ,Vector3Int pos)
    {
        RpcMoveCamera(position , pos);
    }
    [ClientRpc]
    public void RpcMoveCamera(Vector3 position ,Vector3Int pos)
    {
        MoveTargetWithMouse.Instance.MoveToPosition(position);

        // update SelectCell position

        Square square = SearchingMethod.FindSquareByPosition(pos);

        ClickSquare.Instance.SelectCell.UpdatePosition(square.transform.position);
    }
}
