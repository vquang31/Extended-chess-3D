using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraMovement : NewMonoBehaviour
{
    public CinemachineCamera cinemachineCamera;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        cinemachineCamera = GetComponent<CinemachineCamera>();

    }

    internal void SetTarget(Transform target)
    {
        cinemachineCamera.Follow = target;
    }
}
