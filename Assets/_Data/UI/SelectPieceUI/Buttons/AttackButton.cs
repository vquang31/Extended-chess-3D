using UnityEngine;

public class AttackButton : BaseButton
{
    protected override void OnClick()
    {
        BoardManager.Instance.selectedPiece.GetComponent<Piece>().AttackChess();
        base.OnClick();
    }
}
