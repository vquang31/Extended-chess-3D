using UnityEngine;

public class AttackButton : SelectButton
{
    protected override void OnClick()
    {
        BoardManager.Instance.selectedPiece.GetComponent<Piece>().AttackChess();
        base.OnClick();
    }
}
