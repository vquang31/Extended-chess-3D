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
                Vector2Int newPosition2d = ConvertMethod.Pos3dToPos2d(Position) + newDirectionMove;
                if (SearchingMethod.IsSquareValid(newPosition2d) == false || SearchingMethod.IsSquareEmpty(newPosition2d) == false)
                {
                    continue;
                }
                Vector3Int newPosition3d = ConvertMethod.Pos2dToPos3d(newPosition2d);
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
        return base.GetValidAttacks();
    }

    public override List<Vector2Int> GetAttackDirection()
    {
        List<Vector2Int> rangeAttacks = new List<Vector2Int>()
        {
            new Vector2Int(-1, -1),
            new Vector2Int(-1,  0),
            new Vector2Int(-1,  1),
            new Vector2Int( 0, -1),
            new Vector2Int( 0,  1),
            new Vector2Int( 1, -1),
            new Vector2Int( 1,  0),
            new Vector2Int( 1,  1)
        };
        return rangeAttacks;
    }



    public override void SetPosition(Vector3Int pos)
    {
        base.SetPosition(pos);
        transform.position = new Vector3(0, -0.1f, 0) + transform.position;
    }
}