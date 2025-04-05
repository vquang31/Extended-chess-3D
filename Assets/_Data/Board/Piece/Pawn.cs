using System;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    public override List<Vector3Int> GetValidMoves()
    {
        List<Vector3Int> validMoves = new List<Vector3Int>();

        Vector2Int[] directionMoves = {
            new Vector2Int( 0,  1),
            new Vector2Int( 0, -1),
            new Vector2Int( 1,  0),
            new Vector2Int(-1,  0)
        };
        
        for(int i = 0; i < directionMoves.Length; i++)
        {
            int movePoint = MovePoint;
            Vector3Int currentPosition = Position;
            while (movePoint >= 1)
            {
                Vector2Int nextPosition2d =Method2.Pos3dToPos2d(currentPosition) + directionMoves[i];
                //Vector2Int nextPosition2d = Method2.Pos3dToPos2d(nextPosition3d);
                if (SearchingMethod.IsSquareValid(nextPosition2d) == false ||  SearchingMethod.IsSquareEmpty(nextPosition2d) == false)
                {
                    break;
                }
                Vector3Int nextPosition3d = Method2.Pos2dToPos3d(nextPosition2d);
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
        //List<Vector3Int> validMoves = new List<Vector3Int>();
        //List<Vector3Int> validAttacks = new List<Vector3Int>();

        //validMoves = GetValidMoves();
        //var attacks = new List<KeyValuePair<Vector3Int, Vector2Int>>();

        //{
        //    List<Vector2Int> tmp = new List<Vector2Int>() { 
        //        new Vector2Int(-1, -1), 
        //        new Vector2Int(-1, 1),
        //        new Vector2Int( 1,-1),
        //        new Vector2Int( 1, 1)
        //    };

        //    foreach(var direction in tmp)
        //    {
        //        if (BoardManager.Instance.IsSquareOccupiedByOpponent(Method2.Pos3dToPos2d(Position) + direction, -Side))
        //        {
        //            attacks.Add(new KeyValuePair<Vector3Int, Vector2Int>(Position, direction));
        //        }
        //    }                                               
        //}                                          

        //foreach (var move in validMoves)
        //{
        //    Vector3Int directionMove =  move - Position ;
        //    Vector2Int directionMove2d = Method2.Pos3dToPos2d(directionMove);
        //    Vector2Int newDirectionAttack2d;
        //    if(directionMove2d.x == 0)
        //    {
        //        // di chuyển theo chiều Z
        //        if (directionMove2d.y > 0)
        //        {
        //            newDirectionAttack2d = directionMove2d + new Vector2Int(-1, 1);
        //            if (BoardManager.Instance.IsSquareOccupiedByOpponent(Method2.Pos3dToPos2d(Position) + newDirectionAttack2d, -Side))
        //            {
        //                attacks.Add(new KeyValuePair<Vector3Int, Vector2Int>(move, new Vector2Int(-1, 1)));
        //            }
        //            newDirectionAttack2d = directionMove2d + new Vector2Int(1, 1);
        //            if (BoardManager.Instance.IsSquareOccupiedByOpponent(Method2.Pos3dToPos2d(Position) + newDirectionAttack2d, -Side))
        //            {
        //                attacks.Add(new KeyValuePair<Vector3Int, Vector2Int>(move, new Vector2Int(1, 1)));
        //            }
        //        }
        //        else
        //        {
        //            newDirectionAttack2d = directionMove2d + new Vector2Int(-1, -1);
        //            if (BoardManager.Instance.IsSquareOccupiedByOpponent(Method2.Pos3dToPos2d(Position) + newDirectionAttack2d, -Side))
        //            {
        //                attacks.Add(new KeyValuePair<Vector3Int, Vector2Int>(move, new Vector2Int(-1, -1)));
        //            }
        //            newDirectionAttack2d = directionMove2d + new Vector2Int(1, -1);
        //            if (BoardManager.Instance.IsSquareOccupiedByOpponent(Method2.Pos3dToPos2d(Position) + newDirectionAttack2d, -Side))
        //            {
        //                attacks.Add(new KeyValuePair<Vector3Int, Vector2Int>(move, new Vector2Int(1, -1)));
        //            }
        //        }
        //    }
        //    else
        //    {
        //        // di chuyển theo chiều X
        //        if (directionMove2d.x > 0)
        //        {
        //            newDirectionAttack2d = directionMove2d + new Vector2Int(1, 1);
        //            if (BoardManager.Instance.IsSquareOccupiedByOpponent(Method2.Pos3dToPos2d(Position) + newDirectionAttack2d, -Side))
        //            {
        //                attacks.Add(new KeyValuePair<Vector3Int, Vector2Int>(move, new Vector2Int(1, 1)));
        //            }
        //            newDirectionAttack2d = directionMove2d + new Vector2Int(1, -1);
        //            if (BoardManager.Instance.IsSquareOccupiedByOpponent(Method2.Pos3dToPos2d(Position) + newDirectionAttack2d, -Side))
        //            {
        //                attacks.Add(new KeyValuePair<Vector3Int, Vector2Int>(move, new Vector2Int(1, -1)));
        //            }
        //        }
        //        else
        //        {
        //            newDirectionAttack2d = directionMove2d + new Vector2Int(-1, 1);
        //            if (BoardManager.Instance.IsSquareOccupiedByOpponent(Method2.Pos3dToPos2d(Position) + newDirectionAttack2d, -Side))
        //            {
        //                attacks.Add(new KeyValuePair<Vector3Int, Vector2Int>(move, new Vector2Int(-1, 1)));
        //            }
        //            newDirectionAttack2d = directionMove2d + new Vector2Int(-1, -1);
        //            if (BoardManager.Instance.IsSquareOccupiedByOpponent(Method2.Pos3dToPos2d(Position) + newDirectionAttack2d, -Side))
        //            {
        //                attacks.Add(new KeyValuePair<Vector3Int, Vector2Int>(move, new Vector2Int(-1, -1)));
        //            }
        //        }
        //    }
        //}
        //foreach (var attack in attacks) {
        //    Vector3Int standPosition3d = attack.Key;
        //    Vector2Int targetPosition2d = Method2.Pos3dToPos2d(standPosition3d) + attack.Value;
        //    Vector3Int targetPosition3d = Method2.Pos2dToPos3d(targetPosition2d);
        //    int diffHeight = Math.Abs(standPosition3d.y - targetPosition3d.y);
        //    if (diffHeight <= HeightRangeAttack) {
        //        validAttacks.Add(targetPosition3d);
        //    }
        //}


        List<Vector3Int> validAttacks = new List<Vector3Int>();
        List<Vector3Int> validMoves = GetValidMoves();
        validMoves.Add(Position);
        List<Vector2Int> attackDirections = GetAttackDirection();

        for(int i = 0; i < validMoves.Count; i++)
        {
            Vector3Int move = validMoves[i];
            Vector2Int move2d = Method2.Pos3dToPos2d(move);
            for (int j = 0; j < attackDirections.Count; j++)
            {
                Vector2Int attackDirection = attackDirections[j];
                Vector2Int targetPosition2d = move2d + attackDirection;
                if(CheckValidAttack(move, targetPosition2d))
                {
                    Vector3Int attackPosition3d =  Method2.Pos2dToPos3d(targetPosition2d);
                    if(!validAttacks.Contains(attackPosition3d))
                        validAttacks.Add(attackPosition3d);
                }
            }
        }

        return validAttacks;
    }

    public override List<Vector2Int> GetAttackDirection()
    {
        List<Vector2Int> rangeAttacks = new List<Vector2Int>()
        {
            new Vector2Int(-1, -1),
            new Vector2Int(-1,  1),
            new Vector2Int( 1, -1),
            new Vector2Int( 1,  1)
        };
        return rangeAttacks;
    }


}

