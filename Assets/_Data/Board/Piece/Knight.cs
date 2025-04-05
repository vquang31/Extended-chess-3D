using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;

public class Knight : Piece
{


    public override List<Vector3Int> GetValidMoves()
    {
        List<Vector3Int> validMoves = new List<Vector3Int>();

        int tmp = 0;
        for (int z = -MovePoint ; z <= MovePoint ; z ++)
        {
            for(int x = -tmp; x<=tmp; x++)
            {
                Vector2Int newDirectionMove = new Vector2Int(x,z);
                Vector2Int newPosition2d = Method2.Pos3dToPos2d(Position) + newDirectionMove;
                if (SearchingMethod.IsSquareValid(newPosition2d) == false || SearchingMethod.IsSquareEmpty(newPosition2d) == false)
                {
                    continue;
                }
                Vector3Int newPosition3d = Method2.Pos2dToPos3d(newPosition2d);
                int diffHeight = Math.Abs(Position.y - newPosition3d.y);
                if (diffHeight <= _jumpPoint)
                {
                    validMoves.Add(newPosition3d);
                }
            }

            if (z >= 0) tmp--;
            else tmp++;
        }
        return validMoves;
    }

    protected override List<Vector3Int> GetValidAttacks()
    {
        List<Vector3Int> validAttacks = new List<Vector3Int>();

        int tmp = 0;
        for (int z = -MovePoint; z <= MovePoint; z++)
        {
            for (int x = -tmp; x <= tmp; x++)
            {
                Vector2Int newDirectionMove = new Vector2Int(x, z);
                Vector2Int newPosition2d = Method2.Pos3dToPos2d(Position) + newDirectionMove;
                if (!BoardManager.Instance.IsSquareOccupiedByOpponent(newPosition2d, -Side))
                {
                    continue;
                }
                Vector3Int newPosition3d = Method2.Pos2dToPos3d(newPosition2d);
                int diffHeight = Math.Abs(Position.y - newPosition3d.y);
                if (diffHeight <= _jumpPoint)
                {
                    validAttacks.Add(newPosition3d);
                }
            }

            if (z >= 0) tmp--;
            else tmp++;
        }
        return validAttacks;
    }


    public override void SetPosition(Vector3Int pos)
    {
        base.SetPosition(pos);
        transform.position = new Vector3(0, -0.1f, 0) + transform.position;
    }
}