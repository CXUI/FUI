using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UDVirtualSurgeryStepController : UDOperationSetpBase
{
    private Dictionary<string, UDOperationSetpBase> _operationSetpDic;
    private UDOperationSetpBase _setp;
    public  UDUIVirSurgerySetp _uiSetp;
    public override void DoStart()
    {
        base.DoStart();
        UDDeBug.DeBugCommon("虚拟手术");
        _uiSetp = new UDUIVirSurgerySetp();
        SetpInit();
        _buttonFinish.GetComponent<Image>().color = Color.blue;
    }

    public override void DoUpdate()
    {
        //base.DoUpdate();
        UDDeBug.DeBugCommon("虚拟手术ing");
        if (_setp == null) return;
        if (_setp._stateType == EStepState.DOING)
        {
            _setp.DoUpdate();
        }
        else
        {
            UpdateSetp();
        }
    }

    public override void DoEnd()
    {
        _buttonFinish.GetComponent<Image>().color = Color.yellow;
        base.DoEnd();
        UDDeBug.DeBugCommon("虚拟手术结束");
    }


    /// <summary>
    /// 初始化步骤
    /// </summary>
    public void SetpInit()
    {
        _operationSetpDic = new Dictionary<string, UDOperationSetpBase>();
        _operationSetpDic.Add("手术一", new UDOperaA(this));
        _operationSetpDic.Add("手术二", new UDOperaB(this));
        _operationSetpDic.Add("手术三", new UDOperaC(this));
        _uiSetp.SurgerySetp(_operationSetpDic);
        UpdateSetp();
    }
    /// <summary>
    /// 更新步骤
    /// </summary>
    public void UpdateSetp()
    {
        foreach (UDOperationSetpBase setp in _operationSetpDic.Values)
        {
            if (setp._stateType == EStepState.START)
            {
                _setp = setp;
                setp.DoStart();
                return;
            }
        }

    }


}

public class UDOperaA: UDOperationSetpBase
{
    private UDOperationSetpBase _operation;

    public UDOperaA(UDOperationSetpBase operation)
    {
        _operation = operation;
    }
    public override void DoStart()
    {
        base.DoStart();
        UDDeBug.DeBugCommon("开始步骤一");
        _buttonFinish.GetComponent<Image>().color = Color.blue;
    }

    public override void DoUpdate()
    {
        base.DoUpdate();
        UDDeBug.DeBugCommon("步骤一ing");
    }

    public override void DoEnd()
    {
        _buttonFinish.GetComponent<Image>().color = Color.yellow;
        base.DoEnd();
        UDDeBug.DeBugCommon("步骤一结束");
    }
}
public class UDOperaB : UDOperationSetpBase
{
    private UDOperationSetpBase _operation;

    public UDOperaB(UDOperationSetpBase operation)
    {
        _operation = operation;
    }
    public override void DoStart()
    {
        base.DoStart();
        UDDeBug.DeBugCommon("步骤二");
        _buttonFinish.GetComponent<Image>().color = Color.blue;
    }

    public override void DoUpdate()
    {
        base.DoUpdate();
        UDDeBug.DeBugCommon("步骤二ing");
    }

    public override void DoEnd()
    {
        _buttonFinish.GetComponent<Image>().color = Color.yellow;
        base.DoEnd();
        UDDeBug.DeBugCommon("步骤二结束");
    }
}

public class UDOperaC : UDOperationSetpBase
{
    private UDOperationSetpBase _operation;

    public UDOperaC(UDOperationSetpBase operation)
    {
        _operation = operation;
    }

    public override void DoStart()
    {
        base.DoStart();
        UDDeBug.DeBugCommon("步骤三");
        _buttonFinish.GetComponent<Image>().color = Color.blue;
    }

    public override void DoUpdate()
    {
        base.DoUpdate();
        UDDeBug.DeBugCommon("步骤三ing");
    }

    public override void DoEnd()
    {
        _buttonFinish.GetComponent<Image>().color = Color.yellow;
        base.DoEnd();
        UDDeBug.DeBugCommon("步骤三结束");
        UDOperationSetpManager.Instance._operationSetpDic[EOperationStep.虚拟手术].DoEnd();
        ((UDVirtualSurgeryStepController)_operation)._uiSetp._setpGroup.gameObject.SetActive(false);
    }
}