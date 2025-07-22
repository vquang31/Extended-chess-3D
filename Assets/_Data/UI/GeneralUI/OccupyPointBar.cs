using UnityEngine;
using UnityEngine.UI;

public class OccupyPointBar : SliderBar
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
        slider = GetComponent<Slider>();
        _pointText = transform.Find("PointText").gameObject;
    }
    protected override void Start()
    {
        base.Start();
        SetMaxPoint();
    }
    public override void SetMaxPoint()
    {
        slider.maxValue = Const.MAX_OCCUPYING_POINT;
        slider.value = 0;
    }
}
