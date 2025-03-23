using System.Collections;
using System.ComponentModel;
using UnityEngine;

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
        //if (pos.y == 1 || pos.y == Const.MAX_BOARD_SIZE || pos.x == 1 || pos.x == Const.MAX_BOARD_SIZE)
        //{
        //    _position.y = 3;
        //}
        //else if (pos.y == 2 || pos.y == Const.MAX_BOARD_SIZE - 1 || pos.x == 2 || pos.x == Const.MAX_BOARD_SIZE - 1)
        //{
        //    _position.y = 2;
        //}
        //else if (pos.y == 3 || pos.y == Const.MAX_BOARD_SIZE - 2 || pos.x == 3 || pos.x == Const.MAX_BOARD_SIZE - 2)
        //{
        //    _position.y = 1;
        //} 
        //else
        //{
        //    _position.y = 1;
        //}
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
        if (BoardManager.Instance.selectedPiece == null) // chua select 
        {
            if (pieceGameObject != null &&
                pieceGameObject.GetComponent<Piece>().Side == TurnManager.Instance.Turn())
            {
                pieceGameObject.GetComponent<Piece>().MouseSelected();
            }
            else
            {
                BoardManager.Instance.CancelHighlightAndSelectedChess();
            }
        }
        else
        {
            BoardManager.Instance.CancelHighlightAndSelectedChess();
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
