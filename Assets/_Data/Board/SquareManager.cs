using UnityEngine;

public class SquareManager : Singleton<SquareManager> 
{
    public GameObject SquaresGameObject;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        SquaresGameObject = GameObject.Find("BoardSquare");
    }

    public void HideSquare(Vector2Int pos)
    {
        Square square = SearchingMethod.FindSquareByPosition(pos);   
        square.gameObject.GetComponent<Renderer>().enabled = false;
        square.transform.transform.localScale = Vector3.zero; // Hide the square by scaling it down
        //square.CanClick = false;
    }

    public void HideSquare(Vector3Int pos)
    {
        HideSquare(ConvertMethod.Pos3dToPos2d(pos));
    }

    public void DisplayAll()
    {
        foreach (Transform child in SquaresGameObject.transform)
        {
            child.gameObject.GetComponent<Renderer>().enabled = true;
            child.transform.localScale = new Vector3 (1,10,1); // Reset the scale to make it visible
            child.GetComponent<Square>().CanClick = true; 
        }
    }

}
