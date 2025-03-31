using UnityEngine;
using Unity.Cinemachine;
public class CameraRotation : NewMonoBehaviour
{
    public CinemachineCamera cinemachineCamera; 
    public CinemachineRotationComposer rotationComposer; // Cinemachine FreeLook Camera
    public CinemachineInputAxisController cinemachineInputAxisController; // Cinemachine FreeLook Camera

    //[Header("Rotation")]
    //[SerializeField]
    private KeyCode rotateKey = KeyCode.Mouse1;  // Giữ chuột trái để xoay

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (cinemachineCamera == null)
        {
            cinemachineCamera = GetComponent<CinemachineCamera>();
        }
        if (rotationComposer == null)
        {
            rotationComposer = GetComponent<CinemachineRotationComposer>();
        }
        if(cinemachineInputAxisController == null)
        {
            cinemachineInputAxisController = GetComponent<CinemachineInputAxisController>();
        }
    }





    private void Update()
    {
        cinemachineInputAxisController.enabled = Input.GetKey(rotateKey);
    }
}