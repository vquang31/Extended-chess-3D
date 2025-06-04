using System;
using UnityEngine;

public class CastMagicUIManager : Singleton<CastMagicUIManager> {
    private GameObject _openListMagicButton; // Button to open the magic selection UI
    private GameObject _cancelButton;
    private GameObject _castButton;
    private GameObject _magicList;


    protected override void Start()
    {
        base.Start();
        ResetUI();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();

        _openListMagicButton = GameObject.Find("OpenListMagicBtn");
        _cancelButton = GameObject.Find("CancelMagicBtn");
        _castButton = GameObject.Find("CastBtn");
        _magicList = GameObject.Find("MagicList");

    }

    public void ResetUI()
    {
        _castButton.SetActive(false);
        _cancelButton.SetActive(false);
        _magicList.SetActive(false);
        _openListMagicButton.SetActive(true);
    }

    public void ShowListMagic()
    {
        _magicList.SetActive(true);
        _cancelButton.SetActive(true);
    }

    public void HideListMagic()
    {
        _magicList.SetActive(false);
    }

    public void ShowCastButton()
    {
        _castButton.SetActive(true);
    }
    public void HideCastButton()
    {
        _castButton.SetActive(false);
    }

    public void ShowCastingUI()
    {
        HideListMagic();
    }

    public void HideCastingUI()
    {
        ShowListMagic();
        HideCastButton();
    }
}