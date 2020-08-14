using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDUIStart : MonoBehaviour {

    public void Awake()
    {
        UDUIManager.Instance.Init();
    } 

    private void Start()
    {
        UDUIManager.Instance.ShowInterface(UIResourceDefine.InterfacePrefabPath[(int)UIInterfaceName.Interface_Login]);
    }
}
