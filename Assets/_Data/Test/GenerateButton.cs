using UnityEngine;

public class GenerateButton : BaseButton
{
    protected override void OnClick()
    {
        GeneratorManager.Instance.Generate();
        Debug.Log("Generate Button Clicked");
    }
}
