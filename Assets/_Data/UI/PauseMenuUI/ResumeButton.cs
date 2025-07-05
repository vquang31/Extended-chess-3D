using UnityEngine;

public class ResumeBtn : BackButton
{
    protected override void OnClick()
    {
        //base.OnClick();
        // Resume the game and hide the pause menu
        UIManager.Instance.HidePauseMenuAndResumeGame();
    }

}
