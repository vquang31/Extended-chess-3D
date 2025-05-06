using UnityEngine;

public class EndTurnButton : BaseButton
{
    protected override void OnClick()
    {
        base.OnClick();
        TurnManager.Instance.ChangeTurn();
    }
}
