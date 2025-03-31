using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraMovement : NewMonoBehaviour
{
    public CinemachineCamera cinemachineCamera;
    private GameObject targetGameObject;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        cinemachineCamera = GetComponent<CinemachineCamera>();
        targetGameObject = GameObject.Find("TargetCamera");
        cinemachineCamera.Follow = targetGameObject.transform;
    }

    internal void SetPositionTarget(Transform target)
    {
        targetGameObject.transform.position = target.position;
    }
    internal void SetPositionTarget(Vector3 pos)
    {
        targetGameObject.transform.position = pos;
    }
}
