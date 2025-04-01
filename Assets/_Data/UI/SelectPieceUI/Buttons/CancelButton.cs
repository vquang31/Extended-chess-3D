using UnityEngine;

public class CancelButton : BaseButton
{
    protected override void OnClick()
    {
        BoardManager.Instance.CancelHighlightAndSelectedChess();
        SelectPieceUIManager.Instance.HideUI(); 
    }
}
