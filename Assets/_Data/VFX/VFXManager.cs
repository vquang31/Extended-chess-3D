using UnityEngine;

public class VFXManager : Singleton<VFXManager>
{

    protected GameObject gameObjectVFX;
    
    protected GameObject lightningMagicVFXPrefab;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        gameObjectVFX = GameObject.Find("VFX");
        lightningMagicVFXPrefab = GameObject.Find("Prefab_LightningMagicVFX");

    }
    
    public void PlayVFXMagic(int type, Vector3Int position)
    {
        GameObject vfxPrefab = null;
        Vector3 newPosition = new();
        
        Vector3 cameraForward = Camera.main.transform.forward;
        switch (type)
        {
            case Const.VFX_LIGHTNING_MAGIC:
                vfxPrefab = lightningMagicVFXPrefab;
                newPosition = new Vector3(position.x - cameraForward.x / 2, position.y + 9f, position.z - cameraForward.z / 2);
                break;
                // Add more cases for different VFX types as needed
        }
        if (vfxPrefab != null)
        {
            GameObject vfxInstance = Instantiate(vfxPrefab, newPosition, Quaternion.identity);
            vfxInstance.AddComponent<VFX_2D>(); // Add VFX_2D component for 2D effects
            vfxInstance.transform.parent = gameObjectVFX.transform;
            Destroy(vfxInstance, 0.75f); 
        }
    }
}
