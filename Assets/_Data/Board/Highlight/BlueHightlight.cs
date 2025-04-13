using UnityEngine;

public class BlueHightlight : Highlight
{
    protected override void OnMouseDown()
    {
        if (!canClick) return;
        if (InputBlocker.IsPointerOverUI()) { return; }

        /// nếu để base lên trước thì target piece sẽ là null
        ///  bởi vị trí của square chưa được set
        BoardManager.Instance.selectedPiece.GetComponent<Piece>().FakeMove(_position);
        SelectPieceUIManager.Instance.moveButton.SetActive(true);
        // nếu để base sau thì target piece sẽ NOT NULL 
        // do đã FakeMove , ô đó đã có piece
        base.OnMouseDown();
    }
}
