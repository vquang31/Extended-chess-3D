using System.ComponentModel;
using UnityEngine;

public class Square : NewMonoBehaviour
{


    private Vector3Int _position;
    public Vector3Int Position
    {
        get => _position;
        set => _position = value;
    }



    private ItemBuff itemBuff;
    private GameObject pieceGameObject;


    public void InitRandomHeight(Vector2Int pos)
    {
        if(pos.y == 1 || pos.y == Const.MAX_BOARD_SIZE)
        {
            _position.y = 3;
        }
        else if (pos.y == 2 || pos.y == Const.MAX_BOARD_SIZE - 1)
        {
            _position.y = 2;
        }
        else if (pos.y == 3 || pos.y == Const.MAX_BOARD_SIZE - 2)
        {
            _position.y = 1;
        } 
        else
        {
            //_position.y = Random.Range(Const.MIN_HEIGHT_GROUND, Const.MAX_BEGIN_HEIGHT_GROUND + 1);
            _position.y = 1;
        }
        SetPosition(new Vector3Int(pos.x, Position.y, pos.y));
    }

    protected override void Reset()
    {
        this.LoadComponents();
        this.ResetValues();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
    }

    public void SetPosition(Vector3Int pos)
    {
        _position = pos;
        transform.position = _position;
    }

}
