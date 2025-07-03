using UnityEngine;

public class MoveTargetWithMouse : Singleton<MoveTargetWithMouse>
{

    public Transform targetObject; // Đối tượng cần di chuyển
    public float moveFactor = 0.8f; // Biên độ di chuyển

    private bool isDragging = false;
    private Vector3 startMousePosition;
    private Vector3 startObjectPosition;




    protected override void LoadComponents()
    {
        base.LoadComponents();
        targetObject = transform;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Khi nhấn chuột
        {
            isDragging = true;
            startMousePosition = GetMouseWorldPosition();
            startObjectPosition = targetObject.position;
        }

        if (Input.GetMouseButton(0) && isDragging) // Khi giữ chuột
        {
            Vector3 currentMousePosition = GetMouseWorldPosition();
            Vector3 delta = currentMousePosition - startMousePosition;
            targetObject.position = new Vector3(
                startObjectPosition.x - delta.x * moveFactor,
                startObjectPosition.y,
                startObjectPosition.z - delta.z * moveFactor
            );
            if(targetObject.position.x > Const.MAX_BOARD_SIZE)
            {
                targetObject.position = new Vector3(Const.MAX_BOARD_SIZE, targetObject.position.y, targetObject.position.z);
            }
            if (targetObject.position.x < 0)
            {
                targetObject.position = new Vector3(0, targetObject.position.y, targetObject.position.z);
            }
            if (targetObject.position.z > Const.MAX_BOARD_SIZE)
            {
                targetObject.position = new Vector3(targetObject.position.x, targetObject.position.y, Const.MAX_BOARD_SIZE);
            }
            if (targetObject.position.z < 0)
            {
                targetObject.position = new Vector3(targetObject.position.x, targetObject.position.y, 0);
            }

        }

        if (Input.GetMouseButtonUp(0)) // Khi thả chuột
        {
            isDragging = false;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.WorldToScreenPoint(targetObject.position).z;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }



    public void MoveToPosition(Vector3 position)
    {
        targetObject.position = new Vector3(
            Mathf.Clamp(position.x, 0, Const.MAX_BOARD_SIZE),
            targetObject.position.y,
            Mathf.Clamp(position.z, 0, Const.MAX_BOARD_SIZE)
        );
    }

}
