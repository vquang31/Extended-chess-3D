using UnityEngine;

public class BlueHightlight : Highlight
{
    protected override void OnMouseDown()
    {
        if (!canClick) return;
        BoardManager.Instance.selectedPiece.GetComponent<Piece>().FakeMove(_position);
        SelectPieceUIManager.Instance.moveButton.SetActive(true);
        base.OnMouseDown();
        
    }
}
