using TMPro;
using UnityEngine;

public class AnnouncementTurn : NewMonoBehaviour
{
    private Animator _animator;
    private GameObject _description;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        _animator = GetComponent<Animator>();
        _description = transform.GetChild(0).gameObject; // Assuming the description is the first child
    }
    protected override void Start()
    {
        base.Start();
    }


    public void ShowAnnouncementTurn(int side)
    {
        //EndLoop = false;
        //_animator.SetBool("EndLoop", false);
        _animator.SetTrigger("ShowAnnouncement");
        _description.GetComponent<TextMeshProUGUI>().text = ((side == Const.SIDE_WHITE) ? "White" : "Black") +" Turn";
    }

    public void ShowAnnouncementTurn(string text)
    {
        //EndLoop = false;
        _animator.SetTrigger("ShowAnnouncement");
        _description.GetComponent<TextMeshProUGUI>().text = text;
    }


    private void endLoop()
    {
        _animator.SetBool("EndLoop", true);
    }



}
