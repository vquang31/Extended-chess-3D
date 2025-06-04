using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManaPointBar : SliderBar
{

    protected override void LoadComponents()
    {
        base.LoadComponents();
        slider = GetComponent<Slider>();
        _pointText = transform.Find("ManaPointText").gameObject;
    }

    protected override void Start()
    {
        base.Start();

    }


    public override void SetMaxPoint()
    {
        slider.maxValue = Const.MAX_MANA;
        slider.value = Const.MAX_MANA / 2;
    }


}