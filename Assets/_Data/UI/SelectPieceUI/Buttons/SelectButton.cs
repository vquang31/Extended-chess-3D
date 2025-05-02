using UnityEngine;

public class SelectButton : BaseButton
{
    protected override void OnClick()
    {
        BoardManager.Instance.CancelHighlightAndSelectedChess();
        SelectPieceUIManager.Instance.HideUI();
        InformationPieceUIManager.Instance.HideUI();
    
    }
}
