using UnityEngine;

public class HpBuff : BuffItem
{

    [SerializeField] private int _hpBuff = 100;
    public override void ApplyEffect(Piece piece)
    {
        base.ApplyEffect(piece);
        piece.BuffHp(_hpBuff);
    }

}
