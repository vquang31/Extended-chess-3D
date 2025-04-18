﻿using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Const
{
    public const int SIDE_WHITE = 1;
    public const int SIDE_BLACK = -1;


    public const int MAX_HEIGHT_GROUND = 10;

    public const int MIN_HEIGHT_GROUND = 1; 

    public const int MAX_BEGIN_HEIGHT_GROUND = 3;

    public const int MAX_BOARD_SIZE = 16;


    public const double PERCENTAGE_OF_MAX_HP_TO_KILL = 0.1f;

    public enum PieceType
    {
        Pawn = 0,
        Rook = 1,
        Knight = 2,
        Bishop = 3,
        Queen = 4,
        King = 5
    }

    public static readonly Dictionary<PieceType, (int MaxHp, int AttackPoint, int JumpPoint, int RangeAttack, int MovePoint)> 
        DEFAULT_STATS = new Dictionary<PieceType, (int, int, int,int,int )>
        {
            { PieceType.Pawn,   (100,  10, 2, 1, 3) },
            { PieceType.Rook,   (200,  50, 1, 2, 5) },
            { PieceType.Knight, (150,  30, 4, 3, 3) },
            { PieceType.Bishop, (150,  40, 1, 2, 5) },
            { PieceType.Queen,  (300,  60, 2 ,2, 6) },
            { PieceType.King,   (500, 100, 2, 2, 2) }
        };
}
