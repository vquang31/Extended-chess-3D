using Mono.Cecil;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Const;

public class Piece : NewMonoBehaviour
{
   [SerializeField] private Vector3Int _position;
    public Vector3Int Position
    {
        get => _position;
    }
    private int _side;

    protected int _maxHp;

    protected int _hp;

    protected int _attackPoint;

    protected int _jumpPoint;

    // height range attack of piece
    protected int _heightRangeAttack;

    //private int movePoint;
    //protected int speed;

    protected List<Effect> effects;

    protected override void Reset()
    {
        this.LoadComponents();
        this.ResetValues();
    }
     

    protected virtual void LoadSide()
    {
        
    }
    protected override void ResetValues()
    {
        this.LoadSide();
        // switch expression C# 8.0
        PieceType pieceType = this switch
        {
            Pawn => PieceType.Pawn,
            Rook => PieceType.Rook,
            Knight => PieceType.Knight,
            Bishop => PieceType.Bishop,
            Queen => PieceType.Queen,
            King => PieceType.King,
            _ => throw new InvalidOperationException("Unknown piece type")
        };

         var stats = Const.DEFAULT_STATS[pieceType];
        // Gán giá trị cho các thuộc tính
        this._maxHp = stats.MaxHp;
        this._hp = this._maxHp;
        this._attackPoint = stats.AttackPoint;
        this._jumpPoint = stats.JumpPoint;
        this._heightRangeAttack = stats.RangeAttack;

        this.effects = new List<Effect>();
    }

    protected virtual List<Vector2Int> GetValidMoves()
    {
        return new List<Vector2Int>();
    }

    protected virtual List<Vector2Int> GetValidAttacks(int side)
    {
        return new List<Vector2Int>();
    }

    protected virtual void Move(Vector2Int newPos)
    {

    }
    protected virtual void Attack(Vector2Int targetPos)
    {

    }
    protected void Delete()
    {
        Destroy(gameObject);
    }


    /// <summary>
    ///  Set _position and transform.position of Piece
    /// </summary>
    /// <param name="pos"> new Position </param>
    public virtual void SetPosition(Vector3Int pos)
    {
        // DO NOT CHANGE
        _position = pos;
        // Đặt piece lên trên Square
        // kể cả khi Prefab_Square thay đổi height(localScale.y) thì piece vẫn nằm ở trên Square(sàn)
        // transform.position.y = Square.localScale.y/2 + _position.y/2
        float height = GeneratorSquare.Instance.SquarePrefab1.transform.localScale.y / 2;
        transform.position = new Vector3(0,1,0) * (height) + new Vector3(_position.x, (float)_position.y / 2, _position.z);
    }

    public void OnMouseDown()
    {
        Debug.Log("OnMouseDown");
    }   

}
