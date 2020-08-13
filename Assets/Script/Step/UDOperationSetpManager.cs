using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
/// <summary>
/// 手术模块管理器
/// </summary>
public class UDOperationSetpManager : UDSingleton<UDOperationSetpManager> {


    public  Dictionary<EOperationStep, UDOperationSetpBase> _operationSetpDic;
    
    public UDOperationSetpBase _setp;
    public void Awake()
    {
          _singName = "Operation";
        SetpInit();
    } 

    /// <summary>
    /// 初始化步骤
    /// </summary>
    public void SetpInit()
    {
        _operationSetpDic = new Dictionary<EOperationStep, UDOperationSetpBase>();
        _operationSetpDic.Add(EOperationStep.体格检查, new UDCheakSetpController());
       // _operationSetpDic.Add(EOperationStep.辅助检查, new UDSuppleExaminationStepController());
        _operationSetpDic.Add(EOperationStep.虚拟手术, new UDVirtualSurgeryStepController());
        //_operationSetpDic.Add(EOperationStep.出院, new UDWardTaskSetpController());
        //_operationSetpDic.Add(EOperationStep.复诊, new UDReturnVisitSetpController());
        _operationSetpDic.Add(EOperationStep.评分, new UDScoreStepController());

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
    /// <summary>
    /// 更新步骤
    /// </summary>
    private void Update()
    {
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


}
