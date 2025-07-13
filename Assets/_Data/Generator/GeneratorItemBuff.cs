using Mirror;
using UnityEngine;
using UnityEngine.Splines.ExtrusionShapes;


public class GeneratorItemBuff : NetworkSingleton<GeneratorItemBuff>
{
    protected GameObject itemBuffPrefab;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.itemBuffPrefab = SearchingMethod.FindRegisteredPrefab("Prefab_ItemBuff");
    }

    [ServerCallback]
    //[Server]
    public void Generate(int n)
    {

        for (int i = 0; i < n; i++)
        {
            for (int attempts = 0; attempts < 100; attempts++) // Limit attempts to avoid infinite loop
            {
                int x = Random.Range(2, Const.MAX_BOARD_SIZE);
                int z = Random.Range(2, Const.MAX_BOARD_SIZE);
                Square square = SearchingMethod.FindSquareByPosition(new Vector2Int(x, z));
                if (square.PieceGameObject == null && square._buffItem == null)
                {
                    // Create new itemBuff on Square
                    GenerateItemBuff(new Vector2Int(x,z));
                    break;
                }
            }
 
        }
    }

    public void GenerateItemBuff(Vector2Int pos)
    {

        int ran = Random.Range(1, 4 + 1);
        ran = 2;
        GameObject newItemBuffGameObject = Instantiate(itemBuffPrefab);
        NetworkServer.Spawn(newItemBuffGameObject);
        GameManager.Instance.RpcSaveItemBuff(newItemBuffGameObject.GetComponent<NetworkIdentity>(), pos, ran);
    }



}