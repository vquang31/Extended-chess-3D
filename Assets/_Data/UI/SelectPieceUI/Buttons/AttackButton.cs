using UnityEngine;

public class AttackButton : SelectButton
{
    protected override void OnClick()
    {
        SoundFXMananger.Instance.PlaySoundFX(Const.FX_ATTACK_PIECE, BoardManager.Instance.selectedPiece.transform, 1f);
        BoardManager.Instance.selectedPiece.GetComponent<Piece>().AttackChess();
        base.OnClick();
    }
}
