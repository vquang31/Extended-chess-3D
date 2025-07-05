using UnityEngine;

public class BackButton : MenuButton
{

    [SerializeField]
    private GameObject previousMenuGameObject;


    protected override void OnClick()
    {
        base.OnClick();
        if (previousMenuGameObject != null)
        {
            previousMenuGameObject?.SetActive(true);
        }
    }

}
