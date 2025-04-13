using UnityEngine;

public class KillButton : BaseButton
{
    protected override void OnClick()
    {
        BoardManager.Instance.selectedPiece.GetComponent<Piece>().KillChess();
        base.OnClick();
    }
}
