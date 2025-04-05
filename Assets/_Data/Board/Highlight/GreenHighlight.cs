using UnityEngine;

public class GreenHighlight : Highlight
{
    protected override void OnMouseDown()
    {
        if (!canClick) return;
        BoardManager.Instance.selectedPiece.GetComponent<Piece>().FakeMove(_position);
        SelectPieceUIManager.Instance.moveButton.SetActive(false);
        base.OnMouseDown();
    }
}
