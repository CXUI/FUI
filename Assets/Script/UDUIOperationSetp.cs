using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UDUIOperationSetp : MonoBehaviour {


    private void Start()
    {
        Action();
    } 

    private Transform _canvas;
    private Transform _setpGroup;
    public void Action()
    {
 
        _canvas = UDResourcesLoadManager.LoadGameObject<Transform>("LoadSceneCanvas").transform;
        _setpGroup = UDResourcesLoadManager.LoadGameObject<Transform>("SetpGroup").transform;
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
