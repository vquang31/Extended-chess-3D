using UnityEngine;

public class OpenListMagicBtn : SelectButton
{
    protected override void OnClick()
    {
        MagicCastManager.Instance.IsCasting = 1;
        BoardManager.Instance.ReturnSelectedPosition();
        CastMagicUIManager.Instance.ShowListMagic();
        Hide();
        base.OnClick();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

}