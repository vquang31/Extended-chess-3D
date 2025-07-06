using UnityEngine;
using UnityEngine.UIElements;

public class MoveButton : SelectButton
{
    protected override void OnClick()
    {
        SoundFXMananger.Instance.PlaySoundFX(Const.FX_MOVE_PIECE, BoardManager.Instance.selectedPiece.transform);
        BoardManager.Instance.selectedPiece.GetComponent<Piece>().Move();
        base.OnClick();
    }
}
