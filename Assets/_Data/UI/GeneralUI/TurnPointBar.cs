using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TurnPointBar : SliderBar
{

    protected override void LoadComponents()
    {
        base.LoadComponents();
        _pointText = transform.Find("TurnPointText").gameObject;
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