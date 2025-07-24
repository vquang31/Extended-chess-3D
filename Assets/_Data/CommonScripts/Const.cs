using System.Collections.Generic;
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

    public const float CHANGE_HEIGHT_INIT_DURATION = 1.5f;

    public const double PERCENTAGE_OF_MAX_HP_TO_KILL = 0.5f;

    public enum PieceType
    {
        Pawn = 0,
        Rook = 1,
        Knight = 2,
        Bishop = 3,
        Queen = 4,
        King = 5
    }

    public static readonly Dictionary<PieceType, (int MaxHp, int AttackPoint, int JumpPoint, int RangeAttack, int MovePoint, int Cost)> 
        DEFAULT_STATS = new()
        {
            { PieceType.Pawn,   (120,  30, 2, 1, 3, 125) },
            { PieceType.Rook,   (200,  50, 1, 2, 5, 175) },
            { PieceType.Knight, (150,  40, 4, 3, 3, 175) },
            { PieceType.Bishop, (150,  40, 1, 2, 5, 175) },
            { PieceType.Queen,  (300,  50, 2, 2, 6, 225) },
            { PieceType.King,   (500, 100, 2, 2, 2, 225) }
        };


    public static List<Vector2Int> DIRECTION_EFFECTED_TOWER = new()
    {
        new Vector2Int(-1, -1),
        new Vector2Int(-1, 0),
        new Vector2Int(-1, 1),

        new Vector2Int(0, -1),
        new Vector2Int(0, 1),

        new Vector2Int(1, -1),
        new Vector2Int(1, 0),
        new Vector2Int(1, 1),

    };

    public static int MAX_POINT_TOWER = 250;
    
    public static int OCCUPY_POINT_PER_TURN_PER_PIECE = 5;

    public static int MAX_OCCUPYING_POINT = 1500; // player






    /// 
    ///

    public const int MAX_POINT_PER_TURN = 500;

    public const int MAX_MANA = 1000;

    public const int INCREASE_MANA_PER_TURN = 100;

    public const int LIGHTNING_DAMAGE = 50;

    public const int HEIGHT_INCREASE_MAGIC = 2;

    public const int MANA_POINT_COST_PER_LIGHTNING_MAGIC = 200;

    public const int MANA_POINT_COST_PER_CHANGE_HEIGHT = 150 ;




    /////////////// VFX


    public const int FX_MOVE_PIECE = 0;
    public const int FX_ATTACK_PIECE = 1;
    public const int FX_LIGHTNING_MAGIC = 2;
    public const int FX_CHANGE_HEIGHT = 3;


    public const float VFX_ATTACK_PIECE_DURATION = (float) 120 / 60 + 0.01f; // Duration for the attack piece VFX
    public const float VFX_LIGHTNING_MAGIC_DURATION =  3f + 0.01f; // Duration for the lightning magic VFX


    public const float VFX_PIECE_TAKE_DAMAGE_DURATION = 0.5f; // Duration for the piece take damage VFX


    public const float CHANGE_HEIGHT_EFFECT_DURATION = 1f; // 1s 

}
