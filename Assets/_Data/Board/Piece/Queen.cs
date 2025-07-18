using System;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
{
    public override List<Vector3Int> GetValidMoves()
    {
        List<Vector3Int> validMoves = new List<Vector3Int>();
        Vector2Int[] directionMoves = {
            new Vector2Int( -1,  1),
            new Vector2Int( -1, -1),
            new Vector2Int( 1,  1),
            new Vector2Int(1,  -1),
            new Vector2Int( 0,  1),
            new Vector2Int( 0, -1),
            new Vector2Int( 1,  0),
            new Vector2Int(-1,  0)
        };

        for (int i = 0; i < directionMoves.Length; i++)
        {
            int movePoint = MovePoint;
            Vector3Int currentPosition = Position;
            while (movePoint >= 1)
            {
                Vector2Int nextPosition2d = ConvertMethod.Pos3dToPos2d(currentPosition) + directionMoves[i];

                if (SearchingMethod.CanStandOnSquare(nextPosition2d) == false)
                {
                    break;
                }
                Vector3Int nextPosition3d = ConvertMethod.Pos2dToPos3d(nextPosition2d);
                int diffHeight = Math.Abs(nextPosition3d.y - currentPosition.y);

                if (diffHeight > _jumpPoint)
                {
                    break;
                }
                else
                {
                    if (diffHeight > 1) // jumpPoint >=  diffHeight > 1
                    {
                        if (movePoint > 1)
                            movePoint--;
                        else break;
                    }
                }
                validMoves.Add(nextPosition3d);
                currentPosition = nextPosition3d;
                movePoint--;
            }
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


}