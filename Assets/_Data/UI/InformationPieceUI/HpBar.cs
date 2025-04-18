using UnityEngine;
using UnityEngine.UI;

public class HpBar : NewMonoBehaviour
{
    private Slider slider;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        slider = GetComponent<Slider>();
    }


    public void SetMaxHp(int hp)
    {
        slider.maxValue = hp;
        slider.value = hp;
    }
    public void SetHp(int hp)
    {
        slider.value = hp;
    }

    public void SetValue(Piece piece)
    {
        SetMaxHp(piece.MaxHp);
        SetHp(piece.Hp);
    }

}
