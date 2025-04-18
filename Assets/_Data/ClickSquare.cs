using UnityEngine;
using UnityEngine.EventSystems;

public class ClickSquare : Singleton<ClickSquare>
{
    private float lastClickTime = 0f;
    private float doubleClickThreshold = 0.3f; // Giới hạn thời gian giữa 2 lần click (300ms)
    private SelectCell selectCell;


    [SerializeField]
    AbstractSquare square;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        selectCell = GameObject.Find("SelectCell").GetComponent<SelectCell>();
    }

    /// <summary>
    ///  Hàm này sẽ được gọi đầu tiên khi click vào ô nào đó hoặc quân cờ nào đó
    /// </summary>
    /// <param name="square"></param>
    public void selectSquare(AbstractSquare square)
    {
        if (square == null)
        {
            selectCell.UpdatePosition(square.transform.position);
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
            selectCell.UpdatePosition(square.transform.position);
            this.square = square;

            lastClickTime = Time.time;
        }

        Vector3Int position = square.Position;

        BoardManager.Instance.TargetPiece = SearchingMethod.FindPieceByPosition(position)?.gameObject;

        ///
        // set Information 
        InfoButton.Instance.SetInformationOfGround(position.y);
        // display table when table Information is active 

        // hide tabel Information if target piece is null
        
        if(BoardManager.Instance.TargetPiece == null)
        {
            InformationPieceUIManager.Instance.HideUI();
        }
        else
        {
            if(InformationPieceUIManager.Instance.InformationPieceUI.activeSelf == true)
            {
                //InformationPieceUIManager.Instance.ShowUI();
                // change Information of Piece for target piece
                InformationPieceUIManager.Instance.SetInformationOfPiece(BoardManager.Instance.TargetPiece.GetComponent<Piece>());

            }
        }

        ///
        SelectPieceUIManager.Instance.attackButton.SetActive(false);
        ///
        SelectPieceUIManager.Instance.killButton.SetActive(false);

        return;
    }

    public void changeTargetCamera()
    {
        CameraManager.Instance.SetTarget(square);
    }
}
