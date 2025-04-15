using Unity.VisualScripting;
using UnityEngine;

public class NewMonoBehaviour : MonoBehaviour
{


    // Dont use this method in child class
    // if use this method,  you must call base.Awake() in child class
    //                      and don't call LoadComponents() again
    protected virtual void Awake()
    {
        this.LoadComponents();
    }

    protected virtual void Start()
    {

    }


    protected virtual void Reset()
    {
        this.LoadComponents();
        this.ResetValues();
    }

    // sử dụng khi awake hoặc reset
    // load các component cần thiết
    protected virtual void LoadComponents()
    {

    }

    protected virtual void ResetValues()
    {

    }
    public void Delete()
    {
        Destroy(gameObject);
    }
}
