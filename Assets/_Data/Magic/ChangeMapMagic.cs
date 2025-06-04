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
        square.ChangeHeight(Const.HEIGHT_INCREASE_MAGIC,Const.TIME_TO_CHANGE_HEIGHT_EFFECT);
    }
}
