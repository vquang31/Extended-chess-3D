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
    [SerializeField] private Vector3Int _position = Vector3Int.zero;
    public Vector3Int Position
    {
        get => _position;
    }
    [SerializeField] private int _side;

    protected int _maxHp;

    [SerializeField]protected int _hp;

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
     
    protected override void Start()
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

    public virtual List<Vector3Int> GetValidMoves()
    {
        return new List<Vector3Int>();
    }

    // Display valid Attack
    protected virtual List<Vector3Int> GetValidAttacks()
    {
        List<Vector3Int> validAttacks = new List<Vector3Int>();
        List<Vector3Int> validMoves = GetValidMoves();
        validMoves.Add(Position);
        List<Vector2Int> attackDirections = GetAttackDirection();

        for (int i = 0; i < validMoves.Count; i++)
        {
            Vector3Int move = validMoves[i];
            Vector2Int move2d = Method2.Pos3dToPos2d(move);
            for (int j = 0; j < attackDirections.Count; j++)
            {
                Vector2Int attackDirection = attackDirections[j];
                Vector2Int targetPosition2d = move2d + attackDirection;
                if (CheckValidAttack(move, targetPosition2d))
                {
                    Vector3Int attackPosition3d = Method2.Pos2dToPos3d(targetPosition2d);
                    if (!validAttacks.Contains(attackPosition3d))
                        validAttacks.Add(attackPosition3d);
                }
            }
        }

        return validAttacks;
    }

    public virtual List<Vector2Int> GetAttackDirection()
    {
        return new List<Vector2Int>();
    }

    public virtual bool CheckValidAttack(Vector3Int currentPosition3d, Vector2Int targetPosition2d)
    {
        if (SearchingMethod.IsSquareValid(targetPosition2d) == false || SearchingMethod.IsSquareEmpty(targetPosition2d))
        {
            return false;
        }
        if (SearchingMethod.FindPieceByPosition(targetPosition2d).Side == Side)
        {
            return false;
        }

        Vector3Int targetPosition3d = Method2.Pos2dToPos3d(targetPosition2d);
        int diffHeight = Math.Abs(currentPosition3d.y - targetPosition3d.y);
        if (diffHeight <= HeightRangeAttack)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    ///  Set _position, set transform.position of Piece
    ///  and set PieceGameObject of Square
    /// </summary>
    /// <param name="pos"> new Position </param>
    public virtual void SetPosition(Vector3Int newPos)
    {
        // ???
        //if(SearchingMethod.FindSquareByPosition(Position) != null)
        //{
        //    SearchingMethod.FindSquareByPosition(Position).PieceGameObject = null;
        //}
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
        if (InputBlocker.IsPointerOverUI()) return; 
        ClickSquare.Instance.selectSquare(SearchingMethod.FindSquareByPosition(Position));
        //SearchingMethod.FindSquareByPosition(Position).MoveUp(1);
        if (TurnManager.Instance.Turn() == _side) // nếu cùng side thì chuyển select
        { 
            MouseSelected();

            /// show information of piece   
            /// 
        }
        else
        {
            // neu mà quân cơ này đứng trên ô đỏ thì
            List<GameObject> listHighlight = HighlightManager.Instance.highlights;
            foreach(var hightlight in listHighlight)
            {
                if (hightlight.TryGetComponent<RedHighlight>(out var redHighlight))
                {
                    Vector3Int highlightPos = hightlight.GetComponent<AbstractSquare>().Position;
                    if (highlightPos == Position)
                    {
                        hightlight.GetComponent<RedHighlight>().Click();
                    }
                }
            }
        }
       
    }

    public void MouseSelected()
    {
        if (BoardManager.Instance.selectedPiece == this.gameObject)
        {
            //BoardManager.Instance.ReturnSelectedPosition();
            //BoardManager.Instance.CancelHighlightAndSelectedChess();
        }
        else
        {
            if (BoardManager.Instance.selectedPiece != null)
            {
                BoardManager.Instance.ReturnSelectedPosition();
            }
            BoardManager.Instance.CancelHighlightAndSelectedChess();
            // Debug
            Debug.Log(this.gameObject.name);

            BoardManager.Instance.SelectedPiece(this.gameObject);
            // Camera
            CameraManager.Instance.SetTarget();


            // Highlight // dont change order
            // reason: When call HighlightValidMoves() first, square will hide and we can not see and select this square
            HighlightManager.Instance.HighlightSelf(Position);
            HighlightManager.Instance.HighlightValidAttacks(GetValidAttacks());
            HighlightManager.Instance.HighlightValidMoves(GetValidMoves());

            // UI
            SelectPieceUIManager.Instance.ShowUI();
        }

    }

    public virtual void Move()
    {
        // update physicPosition
        // this :  BoardManager.instance.selectedPiece.GetComponent<Piece>()
        // update data Piece.position
        // update data board
        // update data square

        BoardManager.Instance.CancelHighlightAndSelectedChess();
        //SearchingMethod.FindSquareByPosition(Position).PieceGameObject = null;
        TurnManager.Instance.ChangeTurn();
    }

    public virtual void FakeMove(Vector3Int newPos)
    {
        SearchingMethod.FindSquareByPosition(Position).PieceGameObject = null;
        SetPosition(newPos);
    }
    public virtual void AttackChess()
    {
        BoardManager.Instance.targetPiece.GetComponent<Piece>().TakeDamage(_attackPoint);
        Move();
    }

    public virtual void KillChess()
    {
        BoardManager.Instance.targetPiece.GetComponent<Piece>().Delete();
        FakeMove(BoardManager.Instance.targetPiece.GetComponent<Piece>().Position);
        Move();
    }

    protected virtual void TakeDamage(int damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            Delete();
        }
    }


    protected void Delete()
    {
        Destroy(gameObject);
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
