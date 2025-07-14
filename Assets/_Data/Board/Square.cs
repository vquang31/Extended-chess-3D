using Mirror;
using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;

public class Square : AbstractNetworkSquare, IAnimation 
{

    [SyncVar][SerializeField] private GameObject pieceGameObject;
    public BuffItem _buffItem;

    public GameObject PieceGameObject
    {
        get => pieceGameObject;
        set => pieceGameObject = value;
    }

    [ServerCallback]
    public void InitHeight(Vector2Int pos)
    {
        _position.y = 1;
        SetPosition(new Vector3Int(pos.x, Position.y, pos.y));
    }

    protected override void Reset()
    {
        this.LoadComponents();
        this.ResetValues();
    }

    public override void SetPosition(Vector3Int pos)
    {
        base.SetPosition(pos);
        //if (pieceGameObject != null)
        //    pieceGameObject?.GetComponent<Piece>().SetPosition(pos);
    }
    protected override void OnMouseDown()
    {
        MouseSelected();
    }
    
    public void MouseSelected()
    {

        if (!canClick) return;
        if (InputBlocker.IsPointerOverUI()) return;
        base.OnMouseDown();
        if (TurnManager.Instance.GetCurrentPlayer().IsPlayable() == false) return;
        if (TurnManager.Instance.GetPlayablePlayer().Side != TurnManager.Instance.GetCurrentTurn()) return;
        // 2 điều kiện giống nhau

        //////////// Cast Magic
        if (MagicCastManager.Instance.IsCasting > 0) {
            if(MagicCastManager.Instance.IsCasting == 2)
            {
                if(MagicCastManager.Instance.PositionCasts.Count < MagicCastManager.Instance.SelectedMagic.MaxQuantity &&
                    (MagicCastManager.Instance.PositionCasts.Count + 1) * MagicCastManager.Instance.SelectedMagic.Cost <= TurnManager.Instance.GetCurrentPlayer().Mana)
                {
                    MagicCastManager.Instance.PrepareCast(Position);
                }
            }
        
            return; // nếu đang cast phép thuật thì không cho click
        } 
        //////
        if (pieceGameObject != null)
        {
            pieceGameObject.GetComponent<Piece>().MouseSelected();
        }
    }

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
    IEnumerator ChangeHeightRoutine(int n , float duration)
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


}
