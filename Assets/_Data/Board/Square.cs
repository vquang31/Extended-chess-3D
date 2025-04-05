using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;

public class Square : AbstractSquare, IAnimation 
{

    [SerializeField] private GameObject pieceGameObject;
    private ItemBuff itemBuff;

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
        base.OnMouseDown();
        if (BoardManager.Instance.selectedPiece == null) // chua select 
        {
            if (pieceGameObject != null)
            { 
                if(pieceGameObject.GetComponent<Piece>().Side == TurnManager.Instance.Turn())
                {
                    pieceGameObject.GetComponent<Piece>().MouseSelected();
                }
                else
                {
                    //ClickSquare.Instance.selectSquare(this);
                }
            } 
            else
            {
                //ClickSquare.Instance.selectSquare(this);
            }
        }
        else
        {
            if (BoardManager.Instance.selectedPiece == pieceGameObject)
            {
                // khong bao gio xay ra truong hop nay
                //Debug.Log("111");
                BoardManager.Instance.ReturnSelectedPosition();
                BoardManager.Instance.CancelHighlightAndSelectedChess();
            }
            else
            {
                if(pieceGameObject != null)
                {
                    if(pieceGameObject.GetComponent<Piece>().Side == TurnManager.Instance.Turn())
                    {
                        pieceGameObject.GetComponent<Piece>().MouseSelected();
                    }
                    else
                    {
                        //ClickSquare.Instance.selectSquare(this);
                    }
                }
                else
                {
                    //ClickSquare.Instance.selectSquare(this);
                }
            }
        }
    }

    public void MoveUp(int n)
    {
        _position.y += n;
        StartCoroutine(MoveUpRoutine(n));
        if(pieceGameObject != null)
            pieceGameObject.GetComponent<Piece>().MoveUp(n);
    }

    IEnumerator MoveUpRoutine(int n)
    {
        Vector3 start = transform.position;
        Vector3 target = start + Vector3.up * (float)n / 2;
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
