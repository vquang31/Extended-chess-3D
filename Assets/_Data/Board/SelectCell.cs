using UnityEngine;

public class SelectCell : NewMonoBehaviour
{

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pos"> pos = tranform.position of Square</param>
   public void UpdatePosition(Vector3 pos)
    {
        float height = GeneratorSquare.Instance.SquarePrefab1.transform.localScale.y / 2;
        Vector3 newPos = new Vector3(0,1f,0) * (height) + pos + new Vector3(0, 0.02f, 0);
        gameObject.transform.position = newPos;
    }
}
