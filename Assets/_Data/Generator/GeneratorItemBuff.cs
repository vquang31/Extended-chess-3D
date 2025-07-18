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
    public void Generate(int n)
    {

        for (int i = 0; i < n; i++)
        {
            for (int attempts = 0; attempts < 100; attempts++) // Limit attempts to avoid infinite loop
            {
                int x = Random.Range(2, Const.MAX_BOARD_SIZE);
                int z = Random.Range(2, Const.MAX_BOARD_SIZE);
                Square square = SearchingMethod.FindSquareByPosition(new Vector2Int(x, z));
                if (square.ObjectGameObject == null)
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
        GameObject newItemBuffGameObject = Instantiate(itemBuffPrefab);
        NetworkServer.Spawn(newItemBuffGameObject);
        RpcSaveItemBuff(newItemBuffGameObject.GetComponent<NetworkIdentity>(), pos, ran);
    }


    [ClientRpc]
    public void RpcSaveItemBuff(NetworkIdentity newItemBuffId, Vector2Int pos, int type)
    {
        GameObject newItemBuffGO = newItemBuffId.gameObject;

        Square square = SearchingMethod.FindSquareByPosition(pos);

        switch (type)
        {
            case 1:
                newItemBuffGO.GetComponent<AttackBuff>().enabled = true;
                break;
            case 2:
                newItemBuffGO.GetComponent<ChangeMap>().enabled = true;
                break;
            case 3:
                newItemBuffGO.GetComponent<HpBuff>().enabled = true;
                break;
            case 4:
                newItemBuffGO.GetComponent<BuffManaPlayer>().enabled = true;
                break;
        }

        BuffItem[] buffItems = newItemBuffGO.GetComponents<BuffItem>();
        foreach (var buffItem in buffItems)
        {
            if (buffItem.enabled == false)
            {
                DestroyImmediate(buffItem);
            }
        }

        newItemBuffGO.GetComponent<BuffItem>().SetPosition(ConvertMethod.Pos2dToPos3d(pos));
        newItemBuffGO.transform.SetParent(GameObject.Find("BuffItems").transform);
        newItemBuffGO.name = Method2.NameItemBuff(square.Position);
        square.ObjectGameObject = newItemBuffGO;
    }

}