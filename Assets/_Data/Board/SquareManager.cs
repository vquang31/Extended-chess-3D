using UnityEngine;

public class SquareManager : Singleton<SquareManager> 
{
    protected GameObject SquaresGameObject;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        SquaresGameObject = GameObject.Find("BoardSquare");
    }

    public void HideSquare(Vector2Int pos)
    {
        Square square = SearchingMethod.FindSquareByPosition(pos);   
        square.gameObject.SetActive(false);
    }

    public void HideSquare(Vector3Int pos)
    {
        HideSquare(Method2.Pos3dToPos2d(pos));
    }

    public void DisplayAll()
    {
        foreach (Transform child in SquaresGameObject.transform)
        {
            child.gameObject.SetActive(true);
        }
    }

}
