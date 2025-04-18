using UnityEngine;

public class KillButton : SelectButton
{
    protected override void OnClick()
    {
        BoardManager.Instance.selectedPiece.GetComponent<Piece>().KillChess();
        base.OnClick();
    }
}
