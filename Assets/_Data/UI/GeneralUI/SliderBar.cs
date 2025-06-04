using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderBar : NewMonoBehaviour
{
    protected Slider slider;
    protected GameObject _pointText;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        slider = GetComponent<Slider>();
    }

    protected override void Start()
    {
        base.Start();
    }


    public virtual void SetMaxPoint()
    {
    }

    public virtual void SetMaxPoint(int maxPoint)
    {
        slider.maxValue = maxPoint;
    }

    public virtual void SetPoint(int x)
    {
        slider.value = x;
        _pointText.GetComponent<TextMeshProUGUI>().text = x.ToString() + " / " + slider.maxValue.ToString();
    }

}
