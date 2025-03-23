using UnityEngine;

public class ItemBuff : NewMonoBehaviour
{
    private Vector2Int _position;

    public Vector2Int Position
    {
        get => _position;
        set => _position = value;
    }
}
