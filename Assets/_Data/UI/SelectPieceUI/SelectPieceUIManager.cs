using System.Security.Cryptography;
using UnityEngine;

public class SelectPieceUIManager : Singleton<SelectPieceUIManager>
{
    private GameObject _selectPieceUI;
    public GameObject moveButton;
    public GameObject attackButton;
    public GameObject cancelButton;
    public GameObject killButton;
    protected override void Start()
    {
        base.Start();
        HideUI();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        _selectPieceUI = GameObject.Find("SelectPieceUI");
        moveButton = GameObject.Find("MoveBtn");
        attackButton = GameObject.Find("AttackBtn");
        cancelButton = GameObject.Find("CancelBtn");
        killButton = GameObject.Find("KillBtn");
    }


    public void ShowUI()
    {
        _selectPieceUI.SetActive(true);
        moveButton.SetActive(false);
        attackButton.SetActive(false);
        killButton.SetActive(false);
    }

    public void HideUI()
    {
        _selectPieceUI.SetActive(false);
        moveButton.SetActive(false);
        attackButton.SetActive(false);
        killButton.SetActive(false);
    }
}
