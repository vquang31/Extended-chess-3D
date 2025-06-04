using UnityEngine;

public class SelectMagicBtn : BaseButton 
{
    [SerializeField] private Magic _magic;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        _magic = GetComponent<Magic>();
    }


    protected override void OnClick()
    {
        MagicCastManager.Instance.SetSelectedMagic(_magic);
        CastMagicUIManager.Instance.ShowCastingUI();
        InfoIBAndMagicManagerUI.Instance.ShowUI(_magic);
    }
}