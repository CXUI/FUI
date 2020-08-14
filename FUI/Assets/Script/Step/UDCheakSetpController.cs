using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UDCheakSetpController : UDOperationSetpBase
{

    public override void DoStart()
    {
        base.DoStart();
        UDDeBug.DeBugCommon("体格检查");
        
         _buttonFinish.GetComponent<Image>().color = Color.blue;
    }

    public override void DoUpdate()
    {
        base.DoUpdate();
        UDDeBug.DeBugCommon("体格检查ing");
    }

    public override void DoEnd()
    {
        _buttonFinish.GetComponent<Image>().color = Color.yellow;
        base.DoEnd();
        UDDeBug.DeBugCommon("体格检查结束");
    }
}
