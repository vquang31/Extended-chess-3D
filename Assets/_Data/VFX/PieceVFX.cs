using UnityEngine;

public class PieceVFX : NewMonoBehaviour
{
    private Animator _animator;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        _animator = GetComponent<Animator>();
    }
    public void OffVFX()
    {
        //_animator.SetBool("TakeDamage", false);
    }
}
