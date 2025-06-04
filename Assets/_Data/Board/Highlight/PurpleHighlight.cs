using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PurpleHighlight : Highlight
{
    protected override void OnMouseDown()
    {
        if (!canClick) return;
        if (InputBlocker.IsPointerOverUI()) { return; }


        base.OnMouseDown();
    }
}