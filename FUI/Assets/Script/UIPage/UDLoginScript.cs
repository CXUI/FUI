using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDLoginScript : UDUIInterfaceBase {



    private void Start()
    {
      
    }

    public void OnLogin()
    {
        LoginSuccess();
        //if (_accountnumber.text=="123456"&&_password.text=="123456")
        //{
        //    LoginSuccess();
        //}
        //else
        //{
        //    PopInterface();
        //}
    }

    private void LoginSuccess()
    {
        string path= UIResourceDefine.InterfacePrefabPath[(int)UIInterfaceName.Interface_UIMain];
        UDUIManager.Instance.ShowInterface(path);
    }
 
}
