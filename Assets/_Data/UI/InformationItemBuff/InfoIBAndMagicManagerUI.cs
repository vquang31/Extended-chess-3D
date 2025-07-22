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
        ShowUI(buffItem.Description());
    }

    public void ShowUI(Magic magic)
    {
        ShowUI(magic.Description());    
    }

    public void ShowUI(Tower tower)
    {
        ShowUI(tower.Description());
    }


    public void ShowUI(string s)
    {
        SetInformation(s);
        this.gameObject.SetActive(true);
    }


    public void ShowUI()
    {

    }

    public void HideUI()
    {
        this.gameObject.SetActive(false);
    }

    public void SetInformation(string s)
    {
        _descriptionGameObject.GetComponent<TextMeshProUGUI>().text = s;
    }

}
