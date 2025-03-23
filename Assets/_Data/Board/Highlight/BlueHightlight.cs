using UnityEngine;

public class BlueHightlight : AbstractSquare
{
    protected override void OnMouseDown()
    {
        base.OnMouseDown();
        if (!canClick) return;
        BoardManager.Instance.selectedPiece.GetComponent<Piece>().Move(_position);
    }
}
