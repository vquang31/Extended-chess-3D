using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    private CameraMovement  cameraMovement;
    private CameraRotation  cameraRotation;
    private CameraZoom      cameraZoom;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        cameraMovement = GetComponent<CameraMovement>();
        cameraRotation = GetComponent<CameraRotation>();
        cameraZoom = GetComponent<CameraZoom>();
    }

    public void SetTarget()
    {
        Transform target = BoardManager.Instance.selectedPiece.transform;
        cameraMovement.SetTarget(target);
    }

    public void CancelTarget() { 
        cameraMovement.SetTarget(null);
    }
}
