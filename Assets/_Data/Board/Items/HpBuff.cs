using Mirror;
using UnityEngine;

public class HpBuff : BuffItem
{
    [SerializeField] private int _hpBuff;
    protected override void InitValue()
    {
        base.InitValue();
        _hpBuff = Random.Range(50, 100);
    }

    public override string Description()
    {
        return "Heal for piece " + _hpBuff + " HP" ;
    }

    [Command(requiresAuthority = false)]
    public override void ApplyEffect(Piece piece)
    {
        base.ApplyEffect(piece);
        piece.BuffHp(_hpBuff);
    }
    
}
