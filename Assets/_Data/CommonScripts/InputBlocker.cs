using UnityEngine;
using UnityEngine.EventSystems;

public static class InputBlocker
{
    public static bool IsPointerOverUI()
    {
        if (EventSystem.current == null) return false;
        return EventSystem.current.IsPointerOverGameObject();
    }
}