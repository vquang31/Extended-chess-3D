using UnityEngine;

public class InformationPieceUIManager : Singleton<InformationPieceUIManager>
{
    private GameObject _informationPieceUI;

    protected override void Start()
    {
        base.Start();
        HideUI();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        _informationPieceUI = GameObject.Find("InformationPieceUI");
    }


    public void ShowUI()
    {
        _informationPieceUI.SetActive(true);
    }

    public void HideUI()
    {
        _informationPieceUI.SetActive(false);
    }
}
