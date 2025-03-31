using UnityEngine;
using UnityEngine.EventSystems;

public class DoubleClickSquare : Singleton<DoubleClickSquare>
{
    private float lastClickTime = 0f;
    private float doubleClickThreshold = 0.3f; // Giới hạn thời gian giữa 2 lần click (300ms)

    [SerializeField]
    Square square;

    public void selectSquare(Square square)
    {
        if (square == null)
        {
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
