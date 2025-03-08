using UnityEngine;
using UnityEngine.UIElements;

public class Knight : Piece
{

    public override void SetPosition(Vector3Int pos)
    {
        base.SetPosition(pos);
        transform.position = new Vector3(0, -0.1f, 0) + transform.position;
    }
}