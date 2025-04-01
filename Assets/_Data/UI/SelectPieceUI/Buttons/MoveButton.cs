using UnityEngine;
using UnityEngine.UIElements;

public class MoveButton : BaseButton
{
    protected override void OnClick()
    {
        base.OnClick();
        //BoardManager.Instance.selectedPiece.GetComponent<Piece>().Move(_position);
    }
}
