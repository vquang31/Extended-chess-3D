using UnityEngine;
using UnityEngine.UIElements;

public class MoveButton : SelectButton
{
    protected override void OnClick()
    {
        BoardManager.Instance.selectedPiece.GetComponent<Piece>().Move();
        base.OnClick();
    }
}
