using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ChangeMapMagic : Magic
{

    protected override void Start()
    {
        InitValue();
    }
    protected override void InitValue()
    {
        Cost = 100;
        MaxQuantity = 3;
        Type = Const.FX_CHANGE_HEIGHT;
    }
    public override string Description()
    {
        return $"Player can up square {Const.HEIGHT_INCREASE_MAGIC} unit 1-3 times" +
            $"\nCost: {Cost} mana / square";
    }
    protected override void ApplyEffectToSquare(Vector3Int position)
    {
        base.ApplyEffectToSquare(position);
        ///
        Square square = SearchingMethod.FindSquareByPosition(position);
        square.CmdChangeHeight(Const.HEIGHT_INCREASE_MAGIC,Const.CHANGE_HEIGHT_EFFECT_DURATION);
    }
}
