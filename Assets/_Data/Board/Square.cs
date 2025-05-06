using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;

public class Square : AbstractSquare, IAnimation 
{

    [SerializeField] private GameObject pieceGameObject;
    public BuffItem _buffItem;

    public GameObject PieceGameObject
    {
        get => pieceGameObject;
        set => pieceGameObject = value;
    }

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
    }



    protected override void OnMouseDown()
    {
        if (!canClick) return;
        if (InputBlocker.IsPointerOverUI())  return; 
        base.OnMouseDown();
        if (pieceGameObject != null)
        {
            pieceGameObject.GetComponent<Piece>().MouseSelected();
        }
    }

    public void ChangeHeight(int n)
    {
        _position.y += n;
        StartCoroutine(ChangeHeightRoutine(n));

        if(pieceGameObject != null)
            pieceGameObject?.GetComponent<Piece>().ChangeHeight(n);
        if(_buffItem != null)
            _buffItem?.ChangeHeight(n);

    }
    IEnumerator ChangeHeightRoutine(int n)
    {
        Vector3 start = transform.position;
        Vector3 target = start + Vector3.up * (float) n / 2;
        float duration = 1f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(start, target, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null; // Chờ 1 frame trước khi tiếp tục
        }
        transform.position = target; // Đảm bảo đạt đúng vị trí../ m,7
    }


}
