using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System;
/// <summary>
/// 手术模块管理器
/// </summary>
public class UDOperationSetpManager : UDSingleton<UDOperationSetpManager> {


    public  Dictionary<EOperationStep, UDOperationSetpBase> _operationSetpDic;
    
    public UDOperationSetpBase _setp;

    public UDUISurgerySetp _surgerySetp;
    public void Awake()
    {
        _surgerySetp = new UDUISurgerySetp();
             _singName = "Operation";
       // SetpInit();
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

        _surgerySetp.SurgerySetp();
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
/// <summary>
/// 整体手术步骤UI
/// </summary>
public class UDUISurgerySetp
{
    private Transform _canvas;
    protected  Transform _setpGroup;
    public void SurgerySetp()
    {

        _canvas = UDResourcesLoadManager.LoadGameObject<Transform>("LoadSceneCanvas").transform;
        _setpGroup = UDResourcesLoadManager.LoadGameObject<Transform>("SetpGroup").transform;
        SceneManager.MoveGameObjectToScene(_canvas.gameObject,UDLoadSceneController.Instance._scene);
     
        
        _setpGroup.SetParent(_canvas);
        _setpGroup.GetComponent<RectTransform>().localPosition = new Vector3(0, 490, 0);

        Transform trans = UDFindObject.FindNote<Transform>(_setpGroup.gameObject, "Button");
        foreach (var temp in UDOperationSetpManager.Instance._operationSetpDic)
        {
            Debug.Log(temp.Key);

            GameObject butt = UDResourcesLoadManager.InstantiateModel(trans.gameObject);
            butt.SetActive(true);
            butt.transform.SetParent(_setpGroup);
            temp.Value._buttonFinish = butt.transform;
            UDFindObject.FindNote<Text>(butt, "Text").text = temp.Key.ToString();
        }

    }
}
/// <summary>
/// 虚拟手术步骤UI
/// </summary>
public class UDUIVirSurgerySetp
{


    private Transform _canvas;
    public Transform _setpGroup;
    public void SurgerySetp(Dictionary<string,UDOperationSetpBase> SetpDic)
    {

        _canvas = GameObject.Find("LoadSceneCanvas").transform;
        _setpGroup = UDResourcesLoadManager.LoadGameObject<Transform>("SetpGroup").transform;

        _setpGroup.SetParent(_canvas);
        _setpGroup.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

        Transform trans = UDFindObject.FindNote<Transform>(_setpGroup.gameObject, "Button");
        foreach (var temp in SetpDic)
        {
            Debug.Log(temp.Key);
            GameObject butt = UDResourcesLoadManager.InstantiateModel(trans.gameObject);
            butt.SetActive(true);
            butt.transform.SetParent(_setpGroup);
            temp.Value._buttonFinish = butt.transform;
            UDFindObject.FindNote<Text>(butt, "Text").text = temp.Key.ToString();
        }

    }
}

 