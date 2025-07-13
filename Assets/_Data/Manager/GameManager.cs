using Mirror;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : NetworkSingleton<GameManager>
{
    [SyncVar]public List<Piece> pieces = new();


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
        square._buffItem = newItemBuffGO.GetComponent<BuffItem>();
    }


    [ClientRpc]
    public void RpcSpawnSquare(NetworkIdentity newItemBuffId , Vector2Int v)
    {
        GameObject newSquareGO = newItemBuffId.gameObject;
        newSquareGO.name = Method2.NameSquare(v);
        newSquareGO.transform.parent = GameObject.Find("BoardSquare").transform;

        Square square = newSquareGO.GetComponent<Square>();
        square.InitHeight(v);
    }



}
