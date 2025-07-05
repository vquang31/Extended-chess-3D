using UnityEngine;
using UnityEngine.UI;

public class PauseGameButton : NextButton
{
    protected override void OnClick()
    {
        //bool isPaused = Time.timeScale == 0;
        //if (!isPaused)
        //{
        //Time.timeScale = 0;
        //pauseMenuUI?.SetActive(true);
        //}
        //else
        //{
        //    Time.timeScale = 1;
        //    pauseMenuUI?.SetActive(false);
        //}


        //base.OnClick();

        UIManager.Instance.ShowPauseMenuAndPauseGame();
    }


}
