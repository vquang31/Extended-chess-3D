using UnityEngine;
using UnityEngine.UIElements;

public class MoveButton : BaseButton
{
    protected override void OnClick()
    {
        BoardManager.Instance.selectedPiece.GetComponent<Piece>().Move();
        base.OnClick();
    }
}
