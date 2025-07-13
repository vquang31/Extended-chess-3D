using Mirror;
using UnityEngine;

public class AttackBuff : BuffItem
{
    [SerializeField] private int _attackBuff;

    protected override void InitValue()
    {
        base.InitValue();
        _attackBuff = Random.Range(5, 40);
    }

    public override string Description()
    {
        return "Buff attack for piece " + _attackBuff + " point";
    }


    [Command(requiresAuthority = false)]
    public override void ApplyEffect(Piece piece)
    {
        base.ApplyEffect(piece);
        piece.AttackPoint += _attackBuff;
    }
}
