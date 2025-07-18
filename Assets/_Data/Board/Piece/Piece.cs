using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Const;
using Mirror;

public class Piece : ObjectOnSquare
{
    [SyncVar] private int _side;
    [SyncVar] private int _maxHp;
    [SyncVar] protected int _attackPoint;
    [SyncVar] protected int _hp;
    [SyncVar] protected int _jumpPoint;
    [SyncVar] protected int _heightRangeAttack;    // height range attack of piece
    [SyncVar] private int _movePoint;
    [SyncVar] private int _cost;

    [SyncVar]private bool _isMoving = false; // check if piece is moving or not

    private Animator animator;

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
        if(SearchingMethod.IsSquareValid(targetPosition2d) == false)
            return false;    
        if (SearchingMethod.IsSquareEmpty(targetPosition2d))
            return false;
        if(SearchingMethod.FindSquareByPosition(targetPosition2d).ObjectGameObject.TryGetComponent<Tower>(out var x) == true)      
           return false; // Tower can not attack
        if (SearchingMethod.FindPieceByPosition(targetPosition2d).Side == Side)
            return false;
        
        Vector3Int targetPosition3d = ConvertMethod.Pos2dToPos3d(targetPosition2d);
        int diffHeight = Math.Abs(currentPosition3d.y - targetPosition3d.y);

        if (diffHeight <= HeightRangeAttack) return true;
        else return false;
        
    }


    [Command (requiresAuthority = false)]
    /// <summary>
    ///  Set _position, set transform.position of Piece
    ///  and set PieceGameObject of Square
    /// </summary>
    /// <param name="pos"> new Position </param>
    public override void SetPosition(Vector3Int newPos )
    {
        if (SearchingMethod.FindSquareByPosition(Position) != null)
        {
            SearchingMethod.FindSquareByPosition(Position).ObjectGameObject = null;
        }
        Position = newPos;
        SetPositionTransform(newPos);
        SearchingMethod.FindSquareByPosition(newPos).ObjectGameObject = this.gameObject;
    }

    [Command(requiresAuthority = false)]
    public void SetPositionTransform(Vector3Int newPos)
    {
        // Đặt piece lên trên Square
        // kể cả khi Prefab_Square thay đổi height(localScale.y) thì piece vẫn nằm ở trên Square(sàn)
        float height = GeneratorSquare.Instance.SquarePrefab1.transform.localScale.y / 2;
        transform.position = Vector3.up * (height) + new Vector3(newPos.x, (float)newPos.y / 2, newPos.z);

    }

    public void OnMouseDown()
    {
        SearchingMethod.FindSquareByPosition(Position).MouseSelected();
    }

    public override void MouseSelected()
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
                BoardManager.Instance.ReturnSelectedPosition();
                BoardManager.Instance.CancelHighlightAndSelectedChess();
                // Debug
                Debug.Log(this.gameObject.name);

                BoardManager.Instance.SelectPiece(this.gameObject);
                // Camera
                CameraManager.Instance.SetTarget();

                StartCoroutine(HighlightPiece());

                // UI
                SelectPieceUIManager.Instance.ShowUI();
            }
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
    private IEnumerator HighlightPiece()
    {
        yield return new WaitForSeconds(0.05f);
        // Đảm bảo oardManager.Instance.ReturnSelectedPosition(); chạy trước
        // Highlight // dont change order
        // reason: When call HighlightValidMoves() first, square will hide and we can not see and select this square
        HighlightManager.Instance.HighlightSelf(Position);
        HighlightManager.Instance.HighlightValidMoves(GetValidMoves());
        HighlightManager.Instance.HighlightValidAttacks(GetValidAttacks());
    }

    public virtual void FakeMove(Vector3Int newPos)
    {
        SetPositionTransform(newPos);

        BoardManager.Instance.FakeMovePosition = newPos;
    }

    public virtual void Move()
    {
        // update physicPosition
        // this :  BoardManager.instance.selectedPiece.GetComponent<Piece>()
        // update data Piece.position
        // update data board
        // update data square


        MoveTargetWithMouse.Instance.MoveToPosition(this.transform.position);    /// offline

        BoardManager.Instance.CancelHighlightAndSelectedChess();                 /// offline
        Square square = SearchingMethod.FindSquareByPosition(BoardManager.Instance.FakeMovePosition);
        if(square.ObjectGameObject != null)
        {
            if(square.ObjectGameObject.TryGetComponent<BuffItem> (out var x) == true)
            {
                Debug.Log("Receive Buff " + x.name);
                CmdAnimatorSetTrigger("ReceiveBuff");
                x.ApplyEffect(this);
                x.Delete();
            }
        }
        Debug.Log(BoardManager.Instance.FakeMovePosition);
        SetPosition(BoardManager.Instance.FakeMovePosition);
        _isMoving = true;
        TurnManager.Instance.EndPieceTurn(this.Cost);
    }

    public virtual void AttackChess()
    {
        Move();

        Vector3Int direction = BoardManager.Instance.TargetPiece.GetComponent<Piece>().Position - this.Position;

        EffectManager.Instance.CmdPlayEffect(Const.FX_ATTACK_PIECE, this.Position, direction);
        
        BoardManager.Instance.TargetPiece.GetComponent<Piece>().TakeDamage(_attackPoint ,  Const.VFX_PIECE_TAKE_DAMAGE_DURATION);
    }

    public virtual void KillChess()
    {
        FakeMove(BoardManager.Instance.TargetPiece.GetComponent<Piece>().Position);
        Move();
        BoardManager.Instance.TargetPiece.GetComponent<Piece>().Delete();
    }

    [Command(requiresAuthority = false)]
    public virtual void TakeDamage(int damage, float timeDie)
    {
        _hp -= damage;

        CmdAnimatorSetTrigger("TakeDamage");

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


    [Command(requiresAuthority = false)]
    private void CmdAnimatorSetTrigger(string nameTrigger)
    {
        RpcAnimatorSetTrigger(nameTrigger);
    }
    [ClientRpc]
    private void RpcAnimatorSetTrigger(string nameTrigger)
    {
        animator.SetTrigger(nameTrigger);
    }

}
