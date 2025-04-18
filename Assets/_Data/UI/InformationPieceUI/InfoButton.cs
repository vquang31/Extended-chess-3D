using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoButton : Singleton<InfoButton>
{
    [SerializeField] protected Button _button;

    public GameObject _infoGround;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        _button = GetComponent<Button>();
        _infoGround = GameObject.Find("InfoGround");
    }

    protected override void Start()
    {
        base.Start();
        _button.onClick.AddListener(OnClick);
    }


    protected void OnClick()
    {
        if (BoardManager.Instance.targetPiece != null)
        {
            if(InformationPieceUIManager.Instance.InformationPieceUI.activeSelf == true)
            {
                InformationPieceUIManager.Instance.HideUI();
            }
            else
            {
                InformationPieceUIManager.Instance.ShowUI();
                InformationPieceUIManager.Instance.SetInformationOfPiece(BoardManager.Instance.TargetPiece.GetComponent<Piece>());
            }
        }
    }

    public void SetInformationOfGround(int x)
    {
        _infoGround.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = x.ToString();
    }

    

}
