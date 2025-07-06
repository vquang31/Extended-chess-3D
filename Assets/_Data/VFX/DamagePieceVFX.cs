using UnityEngine;

public class DamagePieceVFX : NewMonoBehaviour
{
    public void PlayVFX()
    {
        // Play the VFX for damage piece
        // This could be a particle system or any other visual effect
        Debug.Log("Playing damage piece VFX");

        // Example: Assuming you have a ParticleSystem component attached
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            particleSystem.Play();
        }
    }
}
