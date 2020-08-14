using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UDUIInterfaceBase : MonoBehaviour {

    /// <summary>
    /// 窗口类型
    /// </summary>
    public UIInterfaceType _interfaceType;

    /// <summary>
    /// 窗口状态
    /// </summary>
    public UIInterfaceShowMode _showMode;

    public UIInterfaceTop _top;
    
    public virtual void Init(object data)
    {

    }

    public virtual void Show()
    {
        this.gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public virtual void ChangeState()
    {
        this.gameObject.SetActive(!this.gameObject.activeInHierarchy);
    }
	 
}
