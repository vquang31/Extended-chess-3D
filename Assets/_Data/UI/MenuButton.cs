using UnityEngine;

public abstract class MenuButton : BaseButton
{
    // have 2 classses that inherit from this class
    // is BackButton and NextButton to manage the menu navigation
    [SerializeField]
    protected GameObject currentMenuGameObject;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        currentMenuGameObject = transform.parent.gameObject;
    }

    protected override void OnClick()
    {
        base.OnClick();
        if (currentMenuGameObject != null)
        {
            currentMenuGameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Current menu GameObject is not assigned or is null.");
        }
    }
}
