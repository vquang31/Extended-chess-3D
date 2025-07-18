using Mirror;
using Mirror.BouncyCastle.Bcpg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : ObjectOnSquare
{
    [SyncVar(hook = nameof(OnPointChanged))] private int _point = 0;
    [SyncVar(hook = nameof(OnSideChanged))] private int _side = 0; // 1: trắng, -1: đens
    private ParticleSystem _particleSystem;

    [SyncVar] private List<Vector2Int> _effectedPosition = new();

    public int Point => _point;
    public int Side => _side;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        _particleSystem = GetComponent<ParticleSystem>();
    }


    private void OnPointChanged(int oldValue, int newValue)
    {
        // Handle point change logic here, e.g., update UI
        Debug.Log($"Tower point changed from {oldValue} to {newValue}");
    }

    private void OnSideChanged(int oldValue, int newValue)
    {
        // Handle side change logic here, e.g., update UI or visuals
        Debug.Log($"Tower side changed from {oldValue} to {newValue}");
    }


    [Command(requiresAuthority = false)]
    public override void SetPosition(Vector3Int newPos)
    {
        Position = newPos;
        float height = GeneratorSquare.Instance.SquarePrefab1.transform.localScale.y / 2;
        transform.position = Vector3.up * (height) + new Vector3(Position.x, (float)Position.y / 2, Position.z);
        if(_effectedPosition.Count == 0)
        {
            InitValue();

        }
    }

    public void InitValue()
    {
        foreach(var direction in Const.DIRECTION_EFFECTED_TOWER)
        {
            _effectedPosition.Add(ConvertMethod.Pos3dToPos2d(Position) + direction);
        }
    }

    protected void OnMouseDown()
    {
        //MouseSelected();
    }

    public override void MouseSelected()
    {
        //base.MouseSelected();
        //if (InputBlocker.IsPointerOverUI()) return;
        //ClickSquare.Instance.SelectSquare(Position);
    }




}
