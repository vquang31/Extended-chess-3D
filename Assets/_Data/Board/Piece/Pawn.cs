using System;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    protected override List<Vector3Int> GetValidMoves()
    {
        List<Vector3Int> validMoves = new List<Vector3Int>();
        Vector3Int[] DirectionMoves = {
            new Vector3Int(0,  0,  1),
            new Vector3Int(0,  0, -1),
            new Vector3Int(1,  0,  0),
            new Vector3Int(-1, 0,  0)
        };
        
        for(int i = 0; i < DirectionMoves.Length; i++)
        {
            int movePoint = MovePoint;
            Vector3Int currentPosition = Position;
            while (movePoint >= 1)
            {
                Vector3Int nextPosition3d = currentPosition + DirectionMoves[i]; 
                //Vector2Int nextPosition2d = Method2.Pos3dToPos2d(nextPosition3d);
                
                if (SearchingMethod.IsSquareValid(nextPosition3d) == false ||  SearchingMethod.IsSquareEmpty(nextPosition3d))
                {
                    break;
                }
                nextPosition3d = SearchingMethod.FindSquareByPosition(nextPosition3d).Position;
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
        List<Vector3Int> validAttacks = new List<Vector3Int>();

        
        return validAttacks;
    }

}

