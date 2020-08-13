using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//步骤状态
public enum EStepState
{
    START,
    DOING,
    END,
}
//步骤名称
public enum EOperationStep
{
    病史采集,
    体格检查,
    辅助检查,
    诊断,
    医疗计划,
    虚拟手术,
    出院,
    复诊,
    评分,
}
public class UDOperationSetpBase
{
    public EStepState _stateType;
    public Transform _buttonFinish;
    public UDOperationSetpManager _setpManager;

    public virtual void DoStart()
    {
        _stateType = EStepState.DOING;
        _setpManager = UDOperationSetpManager.Instance;
    }

    public virtual void DoUpdate()
    {
        //Debug.Log(this);
        // _stateType = EStepState.DOING;
        if (Input.GetKeyDown(KeyCode.B))
        {
            DoEnd();
        }
    }

    public virtual void DoEnd()
    {
        _stateType = EStepState.END;
    }


}
