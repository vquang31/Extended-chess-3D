using UnityEngine;

public class CancelButton : SelectButton
{

    protected override void OnClick()
    {
        // trả lại trạng thái ban đầu cho bàn cờ và ẩn UI chọn quân cờ
        BoardManager.Instance.ReturnSelectedPosition();
        //
        base.OnClick(); 
    }
}
