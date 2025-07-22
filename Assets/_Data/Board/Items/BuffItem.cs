using Mirror;
using System.Collections;
using UnityEngine;

public class BuffItem : ObjectOnSquare
{
    protected override void Start()
    {
        InitValue();
    }

    protected virtual void InitValue(){}

    //[SerializeField]
    //private Camera _mainCamera;

    public virtual string Description()
    {
        return "";
    }


    protected override void LoadComponents()
    {
    }

    [ServerCallback]
    public override void SetPosition(Vector3Int pos)
    {
        Position = pos;
        float height = GeneratorSquare.Instance.SquarePrefab1.transform.localScale.y / 2;
        transform.position = Vector3.up * (height) + new Vector3(Position.x, (float)Position.y / 2 + 0.6f, Position.z);
    }

    [Command(requiresAuthority = false)]
    public virtual void ApplyEffect(Piece piece)
    {
        // Apply effect to the piece
        // For example, increase the piece's attack power or defense
        // This is just a placeholder for the actual effect logic
        Debug.Log($"Applying effect to {piece.name} at position {Position}");
    }


    //private void LateUpdate()
    //{
    //    Vector3 cameraPosition = _mainCamera.transform.position;
    //    cameraPosition.y = transform.position.y;
    //    transform.LookAt(cameraPosition);
    //    transform.Rotate(0, 180f, 0);
    //}

    protected void OnMouseDown()
    {
        SearchingMethod.FindSquareByPosition(Position).MouseSelected();
    }

    public override void MouseSelected()
    {
        //if (InputBlocker.IsPointerOverUI()) return;
        //ClickSquare.Instance.SelectSquare(Position);
    }



    [Command(requiresAuthority = false)]
    public new void Delete()
    {
        Destroy(gameObject);
    }
}
