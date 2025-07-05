using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ActionPointBar : SliderBar
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
        _pointText = transform.Find("ActionPointText").gameObject;
    }

    protected override void Start()
    {
        base.Start();
        SetMaxPoint();
    }   

    public override void SetMaxPoint()
    {
        slider.maxValue = Const.MAX_POINT_PER_TURN;
        slider.value = Const.MAX_POINT_PER_TURN;
    }


}