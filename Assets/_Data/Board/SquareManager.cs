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
        //Debug.Log("Hide square at position: " + pos);
        Square square = SearchingMethod.FindSquareByPosition(pos);
        //Debug.Log(square.name);
        if (square == null)
        {
            Debug.LogWarning("Square not found at position: " + pos);
            return;
        }

        Renderer renderer = square.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = false;
        }
        else
        {
            Debug.LogWarning("Renderer not found on square at position: " + pos);
        }

        square.transform.localScale = Vector3.zero;
        //square.CanClick = false; // Nếu cần vô hiệu hóa tương tác
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
