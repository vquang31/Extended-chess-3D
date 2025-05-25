using UnityEngine;

public class EndTurnButton : BaseButton
{
    protected override void OnClick()
    {
        if (TurnManager.Instance.CanSwitchTurn() == false) return;
        base.OnClick();
        TurnManager.Instance.ChangeTurn();
    }
}
