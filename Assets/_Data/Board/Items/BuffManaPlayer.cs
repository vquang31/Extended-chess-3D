using Mirror;
using UnityEngine;

public class BuffManaPlayer : BuffItem
{
    [SerializeField] private int _manaBuff = 1;

    protected override void InitValue()
    {
        base.InitValue();
        _manaBuff = Random.Range(10, 30);
    }

    public override string Description()
    {
        return "Player gain " + _manaBuff + "mana point ";
    }

    [Command(requiresAuthority = false)]
    public override void ApplyEffect(Piece piece)
    {
        TurnManager.Instance.GetPlayer(piece.Side).IncreaseMana(_manaBuff);
    }

}
