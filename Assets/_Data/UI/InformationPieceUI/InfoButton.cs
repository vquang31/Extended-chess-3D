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
        if (BoardManager.Instance.TargetPiece != null)
        {
            if (InformationPieceUIManager.Instance._showUI == true)
            {
                InformationPieceUIManager.Instance.HideUI();
            }
            else
            {
                InformationPieceUIManager.Instance.ShowUI();
                InformationPieceUIManager.Instance.SetInformationOfPiece(BoardManager.Instance.TargetPiece.GetComponent<Piece>());
            }
        }
        else 
        {
            Square square = SearchingMethod.FindSquareByPosition(BoardManager.Instance.TargetPosition);
            if (square._buffItem != null)
            {
                if(InfoIBAndMagicManagerUI.Instance.gameObject.activeSelf == true)
                {
                    InfoIBAndMagicManagerUI.Instance.HideUI();
                }
                else
                {
                    InfoIBAndMagicManagerUI.Instance.ShowUI(square._buffItem);
                }
            }
        }


    }

    public void SetInformationOfGround(int x)
    {
        _infoGround.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = x.ToString();
    }

    

}
