using UnityEngine;

public class EffectManager : Singleton<EffectManager>
{

    protected GameObject gameObjectEffect;
    
    protected GameObject lightningMagicVFXPrefab;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        gameObjectEffect = GameObject.Find("VFX");
        lightningMagicVFXPrefab = GameObject.Find("Prefab_LightningMagicVFX");

    }
    
    public void PlayEffect(int typeVFX, Vector3Int position)
    {
        GameObject vfxPrefab = null;
        Vector3 newPosition = new();
        
        Vector3 cameraForward = Camera.main.transform.forward;
        switch (typeVFX)
        {
            case Const.FX_LIGHTNING_MAGIC:
                vfxPrefab = lightningMagicVFXPrefab;
                newPosition = new Vector3(position.x - cameraForward.x / 2, position.y + 9f, position.z - cameraForward.z / 2);
                break;
            // Add more cases for different VFX types as needed
            case Const.FX_CHANGE_HEIGHT:
                break;
        }
        if(typeVFX < 3) // Assuming typeVFX < 3 means it's a valid VFX type with a prefab assigned
                        // and it have VFX
        {

            GameObject vfxInstance = Instantiate(vfxPrefab, newPosition, Quaternion.identity);
            vfxInstance.AddComponent<VFX_2D>(); // Add VFX_2D component for 2D effects
            vfxInstance.transform.parent = gameObjectEffect.transform;
            Destroy(vfxInstance, Const.VFX_LIGHTNING_MAGIC_DURATION ); 
        }
        SoundFXMananger.Instance.PlaySoundFX(typeVFX, gameObjectEffect.transform, 1f); // Play sound effect without VFX

        
    }
}
