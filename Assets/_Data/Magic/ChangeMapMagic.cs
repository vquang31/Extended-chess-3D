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
        return $"";
    }
    protected override void ApplyEffectToSquare(Vector3Int position)
    {
        base.ApplyEffectToSquare(position);
        ///
        Square square = SearchingMethod.FindSquareByPosition(position);
        square.ChangeHeight(1,Const.TIME_TO_CHANGE_HEIGHT_EFFECT);


    }
}
