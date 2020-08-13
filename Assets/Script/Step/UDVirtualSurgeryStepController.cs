using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UDVirtualSurgeryStepController : UDOperationSetpBase
{
    public override void DoStart()
    {
        base.DoStart();
        UDDeBug.DeBugCommon("虚拟手术");
       // _buttonFinish.GetComponent<Image>().color = Color.blue;
    }

    public override void DoUpdate()
    {
        base.DoUpdate();
        UDDeBug.DeBugCommon("虚拟手术ing");
    }

    public override void DoEnd()
    {
        _buttonFinish.GetComponent<Image>().color = Color.yellow;
        base.DoEnd();
        UDDeBug.DeBugCommon("虚拟手术结束");
    }
}
