using UnityEngine;

public class CastMagicBtn : BaseButton
{
    protected override void OnClick()
    {
        Debug.Log("Cast Magic!!!");
        ///
        MagicCastManager.Instance.CastMagic();
        MagicCastManager.Instance.EndCasting();
    }
}
