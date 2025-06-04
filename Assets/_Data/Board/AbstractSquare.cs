using UnityEngine;

public abstract class AbstractSquare : NewMonoBehaviour
{
    protected bool canClick = true;

    public bool CanClick
    {
        get { return canClick; }
        set { canClick = value; }
    }

    [SerializeField] public  Vector3Int _position;
    public Vector3Int Position
    {
        get => _position;
        set => _position = value;
    }
    protected virtual void OnMouseDown()
    {
        //if (InputBlocker.IsPointerOverUI()) { return; }
        ClickSquare.Instance.SelectSquare(this);
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
    
    }
    public virtual void SetPosition(Vector3Int pos)
    {
        _position = pos;
        // transform.position.y = Square._position.y/2
        transform.position = new Vector3(pos.x, (float)pos.y / 2, pos.z);
       
    }
}
