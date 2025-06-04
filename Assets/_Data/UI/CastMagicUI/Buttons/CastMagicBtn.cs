using UnityEngine;

public class CastMagicBtn : BaseButton
{
    protected override void OnClick()
    {
        MagicCastManager.Instance.CastMagic();
        MagicCastManager.Instance.EndCasting();

    }
}
