using UnityEngine;

public class EndTurnButton : BaseButton
{
    protected override void OnClick()
    {
        if (TurnManager.Instance.CanSwitchTurn() == false) return;
        // trả lại trạng thái ban đầu cho bàn cờ và ẩn UI chọn quân cờ
        BoardManager.Instance.ReturnSelectedPosition();
        base.OnClick();
        TurnManager.Instance.ChangeTurn();
    }
}
