using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Const;

public class Piece : NewMonoBehaviour, IAnimation
{

    // bishop
    // elephant
    [SerializeField] private Vector3Int _position;
    public Vector3Int Position
    {
        get => _position;
    }
    [SerializeField] private int _side;

    protected int _maxHp;

    protected int _hp;

    protected int _attackPoint;

    protected int _jumpPoint;

    // height range attack of piece
    protected int _heightRangeAttack;

    private int _movePoint;

    public int Side
    {
        get => _side;
    }

    public int HeightRangeAttack
    {
        get => _heightRangeAttack;
    }


    public int MovePoint
    {
        get => _movePoint;
        set => _movePoint = value;
    }
    //protected int speed;

    protected List<Effect> effects;

    protected override void Reset()
    {
        this.LoadComponents();
        this.ResetValues();
    }
     

    public virtual void Start()
    {
        this.Reset();
    }

    protected virtual void LoadSide()
    {
        if (this.gameObject.name[0] == 'W')
        {
            _side = 1;
        }
        else _side = -1;
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
        this._movePoint = stats.MovePoint;
        this.effects = new List<Effect>();
    }

    protected virtual List<Vector3Int> GetValidMoves()
    {
        return new List<Vector3Int>();
    }

    protected virtual List<Vector3Int> GetValidAttacks()
    {
        return new List<Vector3Int>();
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
    ///  Set _position, set transform.position of Piece
    ///  and set PieceGameObject of Square
    /// </summary>
    /// <param name="pos"> new Position </param>
    public virtual void SetPosition(Vector3Int newPos)
    {
        // DO NOT CHANGE
        _position = newPos;
        // Đặt piece lên trên Square
        // kể cả khi Prefab_Square thay đổi height(localScale.y) thì piece vẫn nằm ở trên Square(sàn)
        // transform.position.y = Square.localScale.y/2 + _position.y/2
        float height = GeneratorSquare.Instance.SquarePrefab1.transform.localScale.y / 2;
        transform.position = new Vector3(0,1,0) * (height) + new Vector3(_position.x, (float)_position.y / 2, _position.z);

        SearchingMethod.FindSquareByPosition(newPos).PieceGameObject = this.gameObject;
    }

    public void OnMouseDown()
    {
        //SearchingMethod.FindSquareByPosition(Position).MoveUp(1);
        if (TurnManager.Instance.Turn() == _side) // nếu cùng side thì chuyển select
            MouseSelected();
    }

    public void MouseSelected()
    {
        if (BoardManager.Instance.selectedPiece == this.gameObject)
        {
            BoardManager.Instance.CancelHighlightAndSelectedChess();
        }
        else
        {
            BoardManager.Instance.CancelHighlightAndSelectedChess();
            // Debug
            Debug.Log(this.gameObject.name);

            BoardManager.Instance.selectedPiece = this.gameObject;
            // Camera
            CameraManager.Instance.SetTarget();


            // Highlight
            HighlightManager.Instance.HighlightValidAttacks(GetValidAttacks());
            HighlightManager.Instance.HighlightValidMoves(GetValidMoves());

        }

    }


    public virtual void Move(Vector3Int newPos)
    {
        // update physicPosition
        // this :  BoardManager.instance.selectedPiece.GetComponent<Piece>()
        // update data Piece.position
        // update data board
        // update data square

        BoardManager.Instance.CancelHighlightAndSelectedChess();

        SearchingMethod.FindSquareByPosition(Position).PieceGameObject = null;
        SetPosition(newPos);
        TurnManager.Instance.ChangeTurn();
    }



    public void MoveUp(int n)
    {
        _position.y += n;
        StartCoroutine(MoveUpRoutine(n));
    }

    IEnumerator MoveUpRoutine(int n)
    {
        Vector3 start = transform.position;
        Vector3 target = start + Vector3.up * (float)n / 2;
        float duration = 1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(start, target, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null; // Chờ 1 frame trước khi tiếp tục
        }
        transform.position = target; // Đảm bảo đạt đúng vị trí

    }


}
