using UnityEngine;

public class EffectManager : Singleton<EffectManager>
{

    protected GameObject gameObjectEffect;
    
    protected GameObject lightningMagicVFXPrefab;

    protected GameObject attackPieceVFXPrefab;
    protected override void LoadComponents()
    {
        base.LoadComponents();

        gameObjectEffect = GameObject.Find("VFX");
        lightningMagicVFXPrefab = GameObject.Find("Prefab_LightningMagicVFX");
        attackPieceVFXPrefab = GameObject.Find("Prefab_AttackPieceVFX");
    }
    
    public void PlayEffect(int typeVFX, Vector3Int position)
    {
        PlayEffect(typeVFX, position, Vector3Int.zero);
    }

    public void PlayEffect(int typeVFX, Vector3Int position, Vector3Int direction)
    {
        GameObject vfxPrefab = null;
        Vector3 newPosition = new();

        Vector3 cameraForward = Camera.main.transform.forward;
        bool vfxExists = false;
        float timeDuration = 0f;
        bool isVFX2D = false;
        switch (typeVFX)
        {
            case Const.FX_LIGHTNING_MAGIC:
                vfxPrefab = lightningMagicVFXPrefab;
                isVFX2D = true;
                newPosition = new Vector3(position.x - cameraForward.x / 4, (float)position.y / 2 + 9.5f, position.z - cameraForward.z / 4);
                vfxExists = true;
                timeDuration = Const.VFX_LIGHTNING_MAGIC_DURATION;
                break;
            case Const.FX_ATTACK_PIECE:
                vfxPrefab = attackPieceVFXPrefab;
                newPosition = new Vector3(position.x , (float)position.y + 5f , position.z );
                vfxExists = true;
                timeDuration = Const.VFX_ATTACK_PIECE_DURATION;
                break;
            case Const.FX_CHANGE_HEIGHT:
                break;
        }
        if (vfxExists)

        {
            GameObject vfxInstance = Instantiate(vfxPrefab, newPosition, Quaternion.identity);

            if(direction != Vector3Int.zero)
            {
                vfxInstance.transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            }
            else
            {
                vfxInstance.transform.rotation = Quaternion.LookRotation(cameraForward);
            }

            // Add VFX_2D component for 2D effects
            if (isVFX2D) { 
                vfxInstance.AddComponent<VFX_2D>();
            }
            vfxInstance.transform.parent = gameObjectEffect.transform;
            Destroy(vfxInstance, timeDuration);
        }
        SoundFXMananger.Instance.PlaySoundFX(typeVFX, gameObjectEffect.transform); // Play sound effect without VFX
    }
}
