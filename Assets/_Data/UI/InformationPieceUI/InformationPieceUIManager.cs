using TMPro;
using UnityEngine;

public class InformationPieceUIManager : Singleton<InformationPieceUIManager>
{
    private GameObject _informationPieceUI;

    private GameObject _namePiece;

    private GameObject _hpText;

    private GameObject _sliderHp;

    private GameObject _BaseInformation;


    public GameObject InformationPieceUI
    {
        get => _informationPieceUI;
    }


    protected override void Start()
    {
        base.Start();
        HideUI();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();


        _informationPieceUI = GameObject.Find("InformationPieceUI");

        _namePiece = transform.Find("NamePiece_IP").gameObject;
        _hpText = transform.Find("HpText_IP").gameObject;
        _sliderHp = transform.Find("SliderHp_IP").gameObject;
        _BaseInformation = transform.Find("BaseInformation_IP").gameObject;
    }


    public void ShowUI()
    {
        _informationPieceUI.SetActive(true);
    }

    public void HideUI()
    {
        _informationPieceUI.SetActive(false);
    }

    public void SetInformationOfPiece(Piece piece)
    {
        _namePiece.GetComponent<TextMeshProUGUI>().text = piece.GetType().Name;
        _hpText.GetComponent<TextMeshProUGUI>().text = piece.Hp.ToString() + " / " + piece.MaxHp.ToString();
        _sliderHp.GetComponent<HpBar>().SetValue(piece);
        _BaseInformation.GetComponent<TextMeshProUGUI>().text = "Attack: " + piece.AttackPoint.ToString() + "\n" +
            "Jump point: " + piece.JumpPoint.ToString() + "\n" +
            "Range Attack: " + piece.HeightRangeAttack.ToString() + "\n" +
            "MovePoint: " + piece.MovePoint.ToString() + "\n " +
            "Cost: " + piece.Cost.ToString() ;
    }


}
