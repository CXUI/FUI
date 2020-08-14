using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UDUIMainController : UDUIInterfaceBase
{

    public Button _button;

    private void Awake()
    {

    }
    GameObject obj;

    void Start()
    {
        if (obj != null) obj.gameObject.SetActive(false);
        _button.onClick.AddListener(() =>
        {
            UDLoadSceneController.Instance.LoadAnsy("AddScene", delegate
             {
                 //    obj = new GameObject("script");
                 //    obj.AddComponent<UDUIOperationSetp>();
                 // UDOperationSetpManager.Instance._surgerySetp.SurgerySetp();
                 UDOperationSetpManager.Instance.SetpInit();
             }, true, true);
         //   UDOperationSetpManager.Instance.SetpInit();
        });
    }

}
