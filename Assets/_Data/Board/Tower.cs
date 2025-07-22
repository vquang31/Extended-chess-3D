using Mirror;
using Mirror.BouncyCastle.Bcpg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : ObjectOnSquare
{
    [SyncVar(hook = nameof(OnPointChanged))][SerializeField] private int _point = 0;
    [SyncVar(hook = nameof(OnSideChanged))][SerializeField] private int _side = 0; // 1: trắng, -1: đens
    [SyncVar] public bool _changedSide = false;
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
        if (newValue < 0)
        {
            _side = -1; 
            if(newValue < - Const.MAX_POINT_TOWER)
            {
                _point = - Const.MAX_POINT_TOWER ;
            }
        }
        else if (newValue > 0)
        {
            _side = 1;
            if(newValue > Const.MAX_POINT_TOWER)
            {
                _point = Const.MAX_POINT_TOWER;
            }
        }
        else if (newValue == 0) {
            _side = 0;
        }
    }

    private void OnSideChanged(int oldValue, int newValue)
    {
        ChangeSideColor();
        if (oldValue * newValue <= 0)
        {
            _changedSide = true;
        }
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
            Vector2Int newPos = ConvertMethod.Pos3dToPos2d(Position) + direction;
            if(SearchingMethod.IsSquareValid(newPos)) _effectedPosition.Add(newPos);
        }
    }

    protected void OnMouseDown()
    {
        SearchingMethod.FindSquareByPosition(Position).MouseSelected();
    }


    public string Description()
    {
        if(_side == 0)
        {
            return $"Tower is neutral.";
        }
        return $"Tower has {_point} points and belongs to side {(_side == 1 ? "White" : "Black")}.";
    }
    public override void MouseSelected()
    {
        //base.MouseSelected();
        //if (InputBlocker.IsPointerOverUI()) return;
        //ClickSquare.Instance.SelectSquare(Position);
    }

    [ClientRpc]
    public void ChangeSideColor()
    {
        StartCoroutine(ChangeSideColorRoutine());
    }


    IEnumerator ChangeSideColorRoutine()
    {
        yield return new WaitForSeconds(0.2f);
        var main = _particleSystem.main;
        if (_side == 1)
        {
            main.startColor = Color.red;
        }
        else if (_side == -1)
        {
            main.startColor = Color.blue;
        }
        else
        {
            main.startColor = Color.white;
        }
    }

    public void DoSomething()
    {
        int countWhite = 0;
        int countBlack = 0;
        int occupyPointWhite = 0;
        int occupyPointBlack = 0;
        foreach (var pos in _effectedPosition)
        {
            Square square = SearchingMethod.FindSquareByPosition(ConvertMethod.Pos2dToPos3d(pos));
            if (square != null && square.objectOnSquare is Piece piece)
            {
                if (piece.Side == Const.SIDE_WHITE) // nếu là quân của mình
                {
                    occupyPointWhite += piece.AttackPoint;
                    countWhite++;
                }
                else if (piece.Side == Const.SIDE_BLACK) // nếu là quân của đối thủ
                {
                    occupyPointBlack += piece.AttackPoint;
                    countBlack++;
                }
            }
        }
        if(occupyPointWhite > occupyPointBlack)
        {
            _point += occupyPointWhite - occupyPointBlack;
        }
        else if (occupyPointBlack >= occupyPointWhite)
        {
            _point -= occupyPointBlack - occupyPointWhite;
        }

        if(_changedSide == true)
        {
            _changedSide = false;
        }
        else
        {
            int count  = (Side == Const.SIDE_WHITE) ? countWhite : countBlack;
            TurnManager.Instance.GetPlayer(Side).IncreaseOccupyingPoint((count + 1) * Const.OCCUPY_POINT_PER_TURN_PER_PIECE);
        }



    }

}
