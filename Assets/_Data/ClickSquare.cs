using UnityEngine;
using UnityEngine.EventSystems;

public class ClickSquare : Singleton<ClickSquare>
{
    private float lastClickTime = 0f;
    private float doubleClickThreshold = 0.3f; // Giới hạn thời gian giữa 2 lần click (300ms)
    private SelectCell selectCell;


    [SerializeField]
    public AbstractSquare square;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        selectCell = GameObject.Find("SelectCell").GetComponent<SelectCell>();
    }

    /// <summary>
    ///  Hàm này sẽ được gọi đầu tiên khi click vào ô nào đó hoặc quân cờ nào đó
    /// </summary>
    /// <param name="square"></param>
    public void SelectSquare(AbstractSquare square)
    {
        if (square == null)
        {
            this.square = square;
            lastClickTime = Time.time;
        }
        else
        {
            if(square == this.square)
            {
                if(Time.time - lastClickTime < doubleClickThreshold)
                {
                    changeTargetCamera();
                }
            }
            this.square = square;
            lastClickTime = Time.time;
        }
        selectCell.UpdatePosition(square.transform.position);
        Vector3Int position = square.Position;


        BoardManager.Instance.UpdateTargetPieceAndPosition(position);
        ///
        // set Information 
        InfoButton.Instance.SetInformationOfGround(position.y);
        // display table when table Information is active 
        // hide tabel Information if target piece is null or buffItem is null

        if (BoardManager.Instance.TargetPiece == null)
        {
            InformationPieceUIManager.Instance.HideUI();
            Square square2 = SearchingMethod.FindSquareByPosition(BoardManager.Instance.TargetPosition);
            if (square2._buffItem != null)
            {
                InfoIBAndMagicManagerUI.Instance.ShowUI(square2._buffItem);
            }
            else
            {
                InfoIBAndMagicManagerUI.Instance.HideUI();
            }
        }
        else
        {
            InfoIBAndMagicManagerUI.Instance.HideUI();
            if (InformationPieceUIManager.Instance.InformationPieceUI.activeSelf == true)
            {
                //InformationPieceUIManager.Instance.ShowUI();
                // change Information of Piece for target piece
                InformationPieceUIManager.Instance.SetInformationOfPiece(BoardManager.Instance.TargetPiece.GetComponent<Piece>());
            }
        }
        SelectPieceUIManager.Instance.attackButton.SetActive(false);
        SelectPieceUIManager.Instance.killButton.SetActive(false);

        return;
    }

    public void changeTargetCamera()
    {
        CameraManager.Instance.SetTarget(square);
    }
}
