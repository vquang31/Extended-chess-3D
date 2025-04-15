using UnityEngine;

public class AttackBuff : BuffItem
{
    [SerializeField] private int _attackBuff = 10;
    public override void ApplyEffect(Piece piece)
    {
        base.ApplyEffect(piece);
        piece.AttackPoint += _attackBuff;
    }
}
