using UnityEngine;

public class CancelMagicBtn : BaseButton
{
    protected override void OnClick()
    {
        if (MagicCastManager.Instance.IsCasting == 2)
        {
            CastMagicUIManager.Instance.HideCastingUI();
            MagicCastManager.Instance.ResetCasting();
        }
        else 
        {
            MagicCastManager.Instance.EndCasting();
        }
    }
}