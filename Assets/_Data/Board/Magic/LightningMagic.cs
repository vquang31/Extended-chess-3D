using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class LightningMagic : Magic
{
    private int _damage = Const.LIGHTNING_DAMAGE;


    public int Damage
    {
        get => _damage;
        set => _damage = value;
    }
    protected override void Start()
    {
        InitValue();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
    }

    protected override void InitValue()
    {
        Cost = 100;
        MaxQuantity = 1;
        Damage = Const.LIGHTNING_DAMAGE;
        Type = Const.FX_LIGHTNING_MAGIC;
    }
    public override string Description()
    {
        return $"Lightning deals {Damage} damage to a target piece." +
            $"\nCost: {Cost} mana";
    }

    protected override void ApplyEffectToSquare(Vector3Int position)
    {
        base.ApplyEffectToSquare(position);
        ///
        Square square = SearchingMethod.FindSquareByPosition(position);
        if (square.ObjectGameObject.TryGetComponent<Piece>(out Piece piece) == false) return;
            piece.TakeDamage(Damage, Const.VFX_PIECE_TAKE_DAMAGE_DURATION );
    }
}

