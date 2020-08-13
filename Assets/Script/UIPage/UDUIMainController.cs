using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UDUIMainController : UDUIInterfaceBase {

    public Button _button;

    private void Awake()
    {

    }
    GameObject obj;

    void Start ()
    {
        if (obj != null) Destroy(obj);
        _button.onClick.AddListener(() => { UDLoadSceneController.Instance.LoadAnsy("AddScene",delegate { GameObject obj = new GameObject("script");
            obj.AddComponent<UDUIOperationSetp>(); },true,true); });
    }

}
