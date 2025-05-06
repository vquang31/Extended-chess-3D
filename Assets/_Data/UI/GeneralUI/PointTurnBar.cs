using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PointTurnBar : NewMonoBehaviour
{
    private Slider slider;
    private GameObject _pointText;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        slider = GetComponent<Slider>();
        _pointText = transform.Find("TurnPointText").gameObject;
    }

    protected override void Start()
    {
        base.Start();
        SetMaxPoint();
    }   


    public void SetMaxPoint()
    {
        slider.maxValue = Const.MAX_POINT_PER_TURN;
        slider.value = Const.MAX_POINT_PER_TURN;
    }

    public void SetMaxPoint(int maxPoint)
    {
        slider.maxValue = maxPoint;
    }

    public void SetPoint(int x)
    {
        slider.value = x;
        _pointText.GetComponent<TextMeshProUGUI>().text = x.ToString() + " / " + slider.maxValue.ToString() ;
    }

}