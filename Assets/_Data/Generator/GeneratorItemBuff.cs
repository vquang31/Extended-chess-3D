using UnityEngine;


public class GeneratorItemBuff : NetworkSingleton<GeneratorItemBuff>
{
    protected GameObject itemBuffGameObject;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.itemBuffGameObject = GameObject.Find("Prefab_ItemBuff");
    }

    public void Generate(int n)
    {
        for(int i = 0; i < n; i++)
        {
            for (int attempts = 0; attempts < 100; attempts++) // Limit attempts to avoid infinite loop
            {
                int x = Random.Range(2, Const.MAX_BOARD_SIZE);
                int z = Random.Range(2, Const.MAX_BOARD_SIZE);
                Square square = SearchingMethod.FindSquareByPosition(new Vector2Int(x, z));
                if (square.PieceGameObject == null && square._buffItem == null)
                {
                    // Create new itemBuff on Square
                    GenerateItemBuff(square);
                    break;
                }
            }
 
        }
    }

    public void GenerateItemBuff(Square square)
    {
        int ran = Random.Range(1, 4 + 1);
        //ran = 2;
        BuffItem newBuffItem = null;
        GameObject newItemBuffGameObject = Instantiate(itemBuffGameObject);
        switch (ran)
        {
            case 1:
                newBuffItem = newItemBuffGameObject.AddComponent<AttackBuff>();
                break;
            case 2:
                newBuffItem = newItemBuffGameObject.AddComponent<ChangeMap>();
                break;
            case 3:
                newBuffItem = newItemBuffGameObject.AddComponent<HpBuff>();
                break;
            case 4:
                newBuffItem = newItemBuffGameObject.AddComponent<BuffManaPlayer>();
                break;
        }
        

        newBuffItem.SetPosition(square.Position);
        newBuffItem.transform.SetParent(GameObject.Find("BuffItems").transform);
        newBuffItem.name  = Method2.NameItemBuff(square.Position);
        square._buffItem = newBuffItem;
    }
}