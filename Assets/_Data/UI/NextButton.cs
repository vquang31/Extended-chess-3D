using UnityEngine;

public class NextButton : MenuButton
{
    [SerializeField]
    [Tooltip("The GameObject of the next menu to show when this button is clicked.")]
    private GameObject nextMenuGameObject; // dùng kéo thả
                                           //use this to assign the next menu GameObject in the inspector

    protected override void OnClick()
    {
        base.OnClick();
        if (nextMenuGameObject != null)
        {
            nextMenuGameObject?.SetActive(true);
        }
    }
}
