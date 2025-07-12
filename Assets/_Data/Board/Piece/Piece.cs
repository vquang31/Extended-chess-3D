using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Const;
using Mirror;

public class Piece : NewNetworkBehaviour, IAnimation
{
    [SerializeField]
    [SyncVar] private Vector3Int _position = Vector3Int.zero;
    [SerializeField] 
    [SyncVar] private int _side;
    [SyncVar] private int _maxHp;
    [SyncVar] protected int _attackPoint;
    [SerializeField]
    [SyncVar] protected int _hp;
    [SyncVar] protected int _jumpPoint;
    [SyncVar] protected int _heightRangeAttack;    // height range attack of piece
    [SyncVar] private int _movePoint;
    [SyncVar] private int _cost;

    [SyncVar]private bool _isMoving = false; // check if piece is moving or not

    private Animator animator;

    public Vector3Int Position => _position;
    public int MaxHp => _maxHp;
    public int Hp => _hp;
    public int JumpPoint => _jumpPoint;
    public void BuffHp(int buff)
    {
        _hp += buff;
        if (_hp > _maxHp)
        {
            _hp = _maxHp;
        }
    }
    public int AttackPoint
    {
        get => _attackPoint;
        set => _attackPoint = value;
    }   
    public int Side => _side;

    public int HeightRangeAttack  => _heightRangeAttack;

    public int MovePoint
    {
        get => _movePoint;
        set => _movePoint = value;
    }
    public int Cost
    {
        get => _cost;
        set => _cost = value;
    }

    protected List<Effect> effects;

    public void Update()
    {
        if (_side < -100)
        {
            TestUpdate();
        }
    }

    public void TestUpdate()
    {
        _side = 1000;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        animator = GetComponent<Animator>();
    }

    protected override void Start()
    {
        this.Reset();
        //Debug.Log("Piece OnStart" + this.gameObject.name);
    }

    public override void OnStartClient()
    {
        //Debug.Log("Piece OnStartClient" + this.gameObject.name);
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        //Debug.Log("Piece OnStartServer" + this.gameObject.name);

    }

    protected override void Reset()
    {
        this.LoadComponents();
        this.ResetValues();
    }
    protected virtual void LoadSide()
    {
        if (this.gameObject.name.Contains("White"))
        {
            _side = Const.SIDE_WHITE;
        }
        else _side = Const.SIDE_BLACK;
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
        this._cost = stats.Cost;
        this.effects = new List<Effect>();
    }
    public virtual List<Vector3Int> GetValidMoves()
    {
        return new List<Vector3Int>();
    }
    public virtual List<Vector2Int> GetAttackDirection()
    {
        return new List<Vector2Int>();
    }
    
    // Display valid Attack
    protected virtual List<Vector3Int> GetValidAttacks()
    {
        List<Vector3Int> validAttacks = new ();
        List<Vector3Int> validMoves = GetValidMoves();
        validMoves.Add(Position);
        List<Vector2Int> attackDirections = GetAttackDirection();

        for (int i = 0; i < validMoves.Count; i++)
        {
            Vector3Int move = validMoves[i];
            Vector2Int move2d = ConvertMethod.Pos3dToPos2d(move);
            for (int j = 0; j < attackDirections.Count; j++)
            {
                Vector2Int attackDirection = attackDirections[j];
                Vector2Int targetPosition2d = move2d + attackDirection;
                if (CheckValidAttack(move, targetPosition2d))
                {
                    Vector3Int attackPosition3d = ConvertMethod.Pos2dToPos3d(targetPosition2d);
                    if (!validAttacks.Contains(attackPosition3d))
                        validAttacks.Add(attackPosition3d);
                }
            }
        }

        return validAttacks;
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

        Vector3Int targetPosition3d = ConvertMethod.Pos2dToPos3d(targetPosition2d);
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
        // DO NOT CHANGE

        _position = newPos;

        // Đặt piece lên trên Square
        // kể cả khi Prefab_Square thay đổi height(localScale.y) thì piece vẫn nằm ở trên Square(sàn)
        float height = GeneratorSquare.Instance.SquarePrefab1.transform.localScale.y / 2;
        transform.position = new Vector3(0,1,0) * (height) + new Vector3(_position.x, (float)_position.y / 2, _position.z);

        SearchingMethod.FindSquareByPosition(newPos).PieceGameObject = this.gameObject;
    }

    public void OnMouseDown()
    {
        SearchingMethod.FindSquareByPosition(_position).MouseSelected();
    }

    public void MouseSelected()
    {
        if (TurnManager.Instance.GetCurrentTurn() == _side) // nếu cùng side thì chuyển select
        {
            if (TurnManager.Instance.GetPlayer(_side).ActionPoint < this.Cost) return;
            if (_isMoving == true) return;
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

                BoardManager.Instance.SelectPiece(this.gameObject);
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
            /// show information of piece   
            /// 
        }
        else
        {

            // neu mà quân cơ này đứng trên ô đỏ thì
            List<GameObject> listHighlight = HighlightManager.Instance.highlights;
            foreach (var hightlight in listHighlight)
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

    public virtual void FakeMove(Vector3Int newPos)
    {
        SearchingMethod.FindSquareByPosition(Position).PieceGameObject = null;
        SetPosition(newPos);
    }

    public virtual void Move()
    {
        // update physicPosition
        // this :  BoardManager.instance.selectedPiece.GetComponent<Piece>()
        // update data Piece.position
        // update data board
        // update data square

        MoveTargetWithMouse.Instance.MoveToPosition(this.transform.position);

        BoardManager.Instance.CancelHighlightAndSelectedChess();
        //SearchingMethod.FindSquareByPosition(Position).PieceGameObject = null;
        Square square = SearchingMethod.FindSquareByPosition(Position);
        if(square._buffItem != null)
        {
            animator.SetTrigger("ReceiveBuff");
            square._buffItem.ApplyEffect(this);
            square._buffItem.Delete();
        }
        _isMoving = true;
        TurnManager.Instance.EndPieceTurn(this.Cost);
    }

    public virtual void AttackChess()
    {
        Move();

        Vector3Int direction = BoardManager.Instance.TargetPiece.GetComponent<Piece>().Position - this.Position;
        Debug.Log(direction);
        EffectManager.Instance.PlayEffect(Const.FX_ATTACK_PIECE, this.Position, direction);
        
        BoardManager.Instance.TargetPiece.GetComponent<Piece>().TakeDamage(_attackPoint ,  Const.VFX_PIECE_TAKE_DAMAGE_DURATION);
    }

    public virtual void KillChess()
    {
        FakeMove(BoardManager.Instance.TargetPiece.GetComponent<Piece>().Position);
        Move();
        BoardManager.Instance.TargetPiece.GetComponent<Piece>().Delete();
    }

    public virtual void TakeDamage(int damage, float timeDie)
    {
        _hp -= damage;

        animator.SetTrigger("TakeDamage");

        if (_hp <= 0)
        {
            Delete(timeDie);
        }
    }
    public void ResetMove(){
        _isMoving = false;
    }
    public void Delete( float timeDie)
    {
        GameManager.Instance.pieces.Remove(this);
        Destroy(gameObject , timeDie );
    }
    public void ChangeHeight(int n, float duration)
    {
        _position.y += n;
        StartCoroutine(ChangeHeightRoutine(n, duration));
    }
    IEnumerator ChangeHeightRoutine(int n , float duration)
    {
        Vector3 start = transform.position;
        Vector3 target = start + Vector3.up * (float)n / 2;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(start, target, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null; // Chờ 1 frame trước khi tiếp tục
        }
        transform.position = target; // Đảm bảo đạt đúng vị trí../ m,7
    }

}
