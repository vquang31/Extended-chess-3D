using System.Security.Cryptography;
using UnityEngine;

public class SelectPieceUIManager : Singleton<SelectPieceUIManager>
{
    private GameObject _selectPieceUI;

    protected override void Start()
    {
        base.Start();
        HideUI();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        _selectPieceUI = GameObject.Find("SelectPieceUI");
    }


    public void ShowUI()
    {
        _selectPieceUI.SetActive(true);
    }

    public void HideUI()
    {
        _selectPieceUI.SetActive(false);
    }
}
