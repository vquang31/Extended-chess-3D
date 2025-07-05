using UnityEngine;

public class UIManager : Singleton<UIManager>
{

    [SerializeField]
    private GameObject pauseMenuUI;

    [SerializeField]
    private GameObject settingMenuUI;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        pauseMenuUI = GameObject.Find("PauseMenu_UI");
        settingMenuUI = GameObject.Find("SettingMenu_UI");

    }

    protected override void Start()
    {
        base.Start();
        HidePauseMenuAndResumeGame();
        HideSettingMenu();
    }


    public void ShowPauseMenuAndPauseGame() 
    {
        Time.timeScale = 0;
        pauseMenuUI?.SetActive(true);
    }

    public void HidePauseMenuAndResumeGame()
    {
        Time.timeScale = 1;
        pauseMenuUI?.SetActive(false);
    }

    public void ShowSettingMenu()
    {
        settingMenuUI?.SetActive(true);
    }

    public void HideSettingMenu()
    {
        settingMenuUI?.SetActive(false);
    }




}
