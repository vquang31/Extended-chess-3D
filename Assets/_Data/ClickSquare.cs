using UnityEngine;
using UnityEngine.EventSystems;

public class ClickSquare : Singleton<ClickSquare>
{
    private float lastClickTime = 0f;
    private float doubleClickThreshold = 0.3f; // Giới hạn thời gian giữa 2 lần click (300ms)
    private SelectCell selectCell;


    [SerializeField]
    Square square;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        selectCell = GameObject.Find("SelectCell").GetComponent<SelectCell>();
    }


    public void selectSquare(Square square)
    {
        if (square == null)
        {
            selectCell.UpdatePosition(square.transform.position);
            this.square = square;
            lastClickTime = Time.time;
            return;
        }
        else
        {
            if(square == this.square)
            {
                if(Time.time - lastClickTime < doubleClickThreshold)
                {
                    changeTargetCamera();
                }
            }
            selectCell.UpdatePosition(square.transform.position);
            this.square = square;
            lastClickTime = Time.time;
            return;
        }
    }

    public void changeTargetCamera()
    {
        CameraManager.Instance.SetTarget(square);
    }
}
