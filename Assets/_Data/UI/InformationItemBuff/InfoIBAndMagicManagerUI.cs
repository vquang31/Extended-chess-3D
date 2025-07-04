using TMPro;
using UnityEngine;

public class InfoIBAndMagicManagerUI : Singleton<InfoIBAndMagicManagerUI>
{
    private GameObject _descriptionGameObject;


    protected override void Start()
    {
        base.Start();
        HideUI();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        _descriptionGameObject = transform.Find("Description_IB_UI").gameObject;
    }

    public void ShowUI(BuffItem buffItem)
    {
        SetInformationOfItemBuff(buffItem);
        this.gameObject.SetActive(true);
    }

    public void ShowUI(Magic magic)
    {
        SetInformationOfMagic(magic);
        this.gameObject.SetActive(true);
    }
    public void HideUI()
    {
        this.gameObject.SetActive(false);
    }

    public void SetInformationOfItemBuff(BuffItem buffItem)
    {
        _descriptionGameObject.GetComponent<TextMeshProUGUI>().text = buffItem.Description();
    }

    public void SetInformationOfMagic(Magic magic)
    {
        _descriptionGameObject.GetComponent<TextMeshProUGUI>().text = magic.Description();
    }
}
