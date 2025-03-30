using Unity.Cinemachine;
using UnityEngine;

public class CameraZoom : NewMonoBehaviour
{

    private CinemachineFollowZoom cinemachineFollowZoom;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        cinemachineFollowZoom = GetComponent<CinemachineFollowZoom>();
    }   

    void Update()
    {
        if(Input.mouseScrollDelta.y != 0)
        {
            if(cinemachineFollowZoom.Width - Input.mouseScrollDelta.y >= 0 && cinemachineFollowZoom.Width - Input.mouseScrollDelta.y <= 10)
                cinemachineFollowZoom.Width -= Input.mouseScrollDelta.y;
        }
    }
}
