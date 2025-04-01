using UnityEngine;
using UnityEngine.UIElements;

public class CameraManager : Singleton<CameraManager>
{
    private CameraMovement  cameraMovement;
    private CameraRotation  cameraRotation;
    private CameraZoom      cameraZoom;
    private GameObject      targetGameObject;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        cameraMovement = GetComponent<CameraMovement>();
        cameraRotation = GetComponent<CameraRotation>();
        cameraZoom = GetComponent<CameraZoom>();
        targetGameObject = GameObject.Find("TargetCamera");

    }

    public void SetTarget()
    {
        Transform target = BoardManager.Instance.selectedPiece.transform;
        cameraMovement.SetPositionTarget(target);
    }

    public void SetTarget(Square square)
    {
        Transform target = square.gameObject.transform;
        float height = GeneratorSquare.Instance.SquarePrefab1.transform.localScale.y / 2;
        Vector3 pos = new Vector3(0, 1, 0) * (height) + target.transform.position;
        cameraMovement.SetPositionTarget(pos);
    }

    public void CancelTarget() { 
        //cameraMovement.SetTarget(null);
    }
}
