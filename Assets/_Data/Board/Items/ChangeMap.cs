using UnityEngine;

public class ChangeMap : BuffItem
{
    [SerializeField] private int _amount ;

    protected override void InitValue()
    {
        base.InitValue();
        _amount = Random.Range(2, 5);
    }
    public override string Description()
    {
        return "Change height of " + _amount + " square";
    }
    public override void ApplyEffect(Piece piece)
    {
        base.ApplyEffect(piece);
        for(int i = 0; i < _amount; )
        {   
            int x = Random.Range(2, Const.MAX_BOARD_SIZE);
            int z = Random.Range(2, Const.MAX_BOARD_SIZE);
            Square square = SearchingMethod.FindSquareByPosition(new Vector2Int(x, z));
            
            if(Random.Range(1, 2+1) == 1)
            {
                if(square.Position.y < Const.MAX_HEIGHT_GROUND )
                {
                    square.ChangeHeight(Random.Range(1, Const.MAX_HEIGHT_GROUND - square.Position.y + 1 ), Const.CHANGE_HEIGHT_EFFECT_DURATION);
                    i++;
                }
            }
            else
            {
                if(square.Position.y > 1)
                {
                    square.ChangeHeight(-Random.Range(1, square.Position.y ), Const.CHANGE_HEIGHT_EFFECT_DURATION);
                    i++;
                }
            }
        }

    }
}
