using UnityEngine;

public class ChangeMap : BuffItem
{
    [SerializeField] private int _amount = 10;

    public override void ApplyEffect(Piece piece)
    {
        base.ApplyEffect(piece);
        _amount = Random.Range(1, _amount);
        for(int i = 0; i < _amount; i++)
        {   
            int x = Random.Range(2, Const.MAX_BOARD_SIZE);
            int z = Random.Range(2, Const.MAX_BOARD_SIZE);
            Square square = SearchingMethod.FindSquareByPosition(new Vector2Int(x, z));
            
            if(Random.Range(1, 2+1) == 1)
            {
                square.ChangeHeight(Random.Range(1, Const.MAX_HEIGHT_GROUND - square.Position.y + 1));
            }
            else
            {
                if(square.Position.y > 1)
                    square.ChangeHeight(-Random.Range(1, square.Position.y));
            }
        }

    }
}
