using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UDScoreStepController : UDOperationSetpBase {

    public override void DoStart()
    {
        base.DoStart();
        UDDeBug.DeBugCommon("成绩");
       _buttonFinish.GetComponent<Image>().color = Color.blue;
    }

    public override void DoUpdate()
    {
        base.DoUpdate();
        UDDeBug.DeBugCommon("成绩ing");
    }

    public override void DoEnd()
    {
        _buttonFinish.GetComponent<Image>().color = Color.yellow;
        base.DoEnd();
        UDDeBug.DeBugCommon("成绩结束");
        UDLoadSceneController.Instance.UnLoadScene();
    }
}
