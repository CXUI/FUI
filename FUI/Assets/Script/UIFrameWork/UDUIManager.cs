using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDUIManager : UDSingleton<UDUIManager>
{

    //字典：保存所有的页面
    public Dictionary<UIInterfaceName, UDUIInterfaceBase> _allInterfaceDic = new Dictionary<UIInterfaceName, UDUIInterfaceBase>();
    //字典：保存所有页面的路径
    public Dictionary<UIInterfaceName, string> _allInterfacePath = new Dictionary<UIInterfaceName, string>();
    //存放所有窗体UIWindowBase
    public Dictionary<string, UDUIInterfaceBase> _dicAllWindows = new Dictionary<string, UDUIInterfaceBase>();
    // 存放当前激活的窗体
    public Dictionary<UIInterfaceType, Stack<UDUIInterfaceBase>> _activeWindowsStack = new Dictionary<UIInterfaceType, Stack<UDUIInterfaceBase>>();
   // public Stack<UDUIInterfaceBase> _activeWindowsStack=new Stack<UDUIInterfaceBase>(); 
    //UI画布
    public Canvas _canvasRoot;
    public Transform _fixedInterfaceRoot;
    public Transform _activityInterfaceRoot;
    public Transform _popInterfaceRoot;
    public Transform _topInterfaceRoot;
    public void Awake()
    {
        _singName = "UIManager";
    }
    public void Init()
    {
        LoadCanvas();

        _activeWindowsStack.Add(UIInterfaceType.Activity, new Stack<UDUIInterfaceBase>());
        _activeWindowsStack.Add(UIInterfaceType.Fixed, new Stack<UDUIInterfaceBase>());
        _activeWindowsStack.Add(UIInterfaceType.Pop, new Stack<UDUIInterfaceBase>());
        _activeWindowsStack.Add(UIInterfaceType.Top, new Stack<UDUIInterfaceBase>());


        _fixedInterfaceRoot = UDFindObject.FindNote<Transform>(_canvasRoot.gameObject, "Fixed");
        _activityInterfaceRoot = UDFindObject.FindNote<Transform>(_canvasRoot.gameObject, "Activity");
        _popInterfaceRoot = UDFindObject.FindNote<Transform>(_canvasRoot.gameObject, "Pop");
        _topInterfaceRoot = UDFindObject.FindNote<Transform>(_canvasRoot.gameObject, "Top");
        UDXmlDataManager.Instance.PathConfig();
    }
    private GameObject _canvas;
    /// <summary>
    /// 加载Canvas
    /// </summary>
    /// <returns></returns>
    private Transform LoadCanvas()
    {
        string strPrePath = UIResourceDefine.InterfacePrefabPath[(int)UIInterfaceName.Interface_Canvas];
#if UNITY_EDITOR
         GameObject temp = UDResourcesLoadManager.LoadGameObject<Transform>(strPrePath).gameObject;

#else
          // GameObject temp = UDResourcesLoadManager.LoadGameObject<Transform>(strPrePath).gameObject;
         GameObject temp=gameObject;
         UDNetworkManager.Instance.GetAssetBundel<GameObject>("UICanvas", strPrePath, t=> { temp= UDResourcesLoadManager.InstantiateModel(t); }, true);
#endif
        temp.gameObject.name = "MainStartUICanvas";
        _canvas=temp;
        temp.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        if (temp != null)
        {
            _canvasRoot = temp.GetComponent<Canvas>();
        }
        else
        {
            Debug.LogError("Canvas is null");
        }
        return temp.transform;
    }
    /// <summary>
    /// 加载窗口
    /// </summary>
    /// <param name="interfaceName"></param>
    /// <returns></returns>
    public  UDUIInterfaceBase ShowInterface(string interfaceName,object data=null)
    {
        if (string.IsNullOrEmpty(interfaceName)) return null;
        UDUIInterfaceBase baseWindow = null;
        baseWindow = IsExistedWindow(interfaceName);
        if (baseWindow == null)
        {
            baseWindow = LoadInterface(interfaceName);
            //SetParentWindow(baseWindow);
            WindowPushStack(baseWindow);
            baseWindow.Init(data);
         
        }
        else
        {
            WindowPushStack(baseWindow);
            InterfaceState(baseWindow);
        }
        //baseWindow.Init(data);
    
        return baseWindow;
    }
    /// <summary>
    /// 入栈
    /// </summary>
    /// <param name="baseWindow"></param>
    private void WindowPushStack(UDUIInterfaceBase baseWindow)
    {
        //if (_activeWindowsStack.Contains(baseWindow)) return;
        //if (baseWindow._interfaceType==UIInterfaceType.Pop) return;
        //if (baseWindow._interfaceType == UIInterfaceType.Activity) return;
        //栈中元素
        //if (_activeWindowsStack.Count > 0)
        //{
            ///UDUIInterfaceBase InterfaceTop = _activeWindowsStack.Peek();//隐藏栈顶元素
            switch (baseWindow._interfaceType)
            {
                case UIInterfaceType.Activity:
                    SetInterfaceState(baseWindow);
                    break;
                case UIInterfaceType.Fixed:
                    SetInterfaceState(baseWindow);
                    break;
                case UIInterfaceType.Pop:
                    //if (InterfaceTop._interfaceType != baseWindow._interfaceType) break;
                     SetInterfaceState(baseWindow);
                    break;
                //case UIInterfaceType.Top: break;

            }
        //}
       // _activeWindowsStack.Push(baseWindow);//入栈当前窗口并显示
    }

    private void SetInterfaceState(UDUIInterfaceBase InterfaceTop)
    {
        if (_activeWindowsStack[InterfaceTop._interfaceType].Contains(InterfaceTop)) return;
 
        //if (_activeWindowsStack[InterfaceTop._interfaceType] == null) { _activeWindowsStack.Add(InterfaceTop._interfaceType, new Stack<UDUIInterfaceBase>()); }
        if (_activeWindowsStack[InterfaceTop._interfaceType].Count > 0)
        {
            UDUIInterfaceBase InterfaceStack= _activeWindowsStack[InterfaceTop._interfaceType].Peek();
            if (InterfaceStack._showMode == UIInterfaceShowMode.NoFreeze)
            {
                UDUIInterfaceBase stackTop = _activeWindowsStack[InterfaceTop._interfaceType].Pop();//出栈 
                //Debug.Log(InterfaceTop.gameObject.name+ InterfaceTop._interfaceType.ToString());
                stackTop.Hide();
            }
        }

        //if (InterfaceTop._showMode == UIInterfaceShowMode.NoFreeze)
        //{
        //    UDUIInterfaceBase stackTop = _activeWindowsStack.Pop();//出栈 
        //    stackTop.Hide();
        //}
        //_activeWindowsStack[InterfaceTop._interfaceType].Push(InterfaceTop);
        //test.Push(InterfaceTop);
        _activeWindowsStack[InterfaceTop._interfaceType].Push(InterfaceTop);
        //_activeWindowsStack.Add(InterfaceTop._interfaceType, new Stack<UDUIInterfaceBase>());
    }
    public  void SetInterfaceState()
    {
        foreach (Stack<UDUIInterfaceBase> temp in _activeWindowsStack.Values)
        {

            foreach (UDUIInterfaceBase item in temp)
            {
                item.Hide();
            }
            temp.Clear();
        }
     
    }
    /// <summary>
    /// 窗口状态
    /// </summary>
    private void InterfaceState(UDUIInterfaceBase baseWindow)
    {
        if (baseWindow.gameObject.activeInHierarchy) return;
        baseWindow.Show();
    }
    /// <summary>
    /// 判断窗口是否存在
    /// </summary>
    /// <param name="interfaceName"></param>
    /// <returns></returns>
    public UDUIInterfaceBase IsExistedWindow(string interfaceName)  
    {
        if (string.IsNullOrEmpty(interfaceName)) return null;
        UDUIInterfaceBase baseWindow = null;
        bool isExisted = _dicAllWindows.TryGetValue(interfaceName, out baseWindow);
        return baseWindow;
    }
    /// <summary>
    ///  加载窗口
    /// </summary>
    /// <param name="windowName"></param>
    /// <returns></returns>
    private UDUIInterfaceBase LoadInterface(string windowName)     
    {
        UDUIInterfaceBase baseWindow = UDResourcesLoadManager.LoadGameObject<UDUIInterfaceBase>(windowName);
        SetParentWindow(baseWindow.GetComponent<UDUIInterfaceBase>());
        _dicAllWindows.Add(windowName, baseWindow.GetComponent<UDUIInterfaceBase>());
        return baseWindow;
    }
    /// <summary>
    /// 设置窗口的父物体
    /// </summary>
    /// <param name="UIWindowBase"></param>
    public void SetParentWindow(UDUIInterfaceBase UIWindowBase)
    {
        switch (UIWindowBase._interfaceType)
        {
            case UIInterfaceType.Fixed:
                UIWindowBase.transform.SetParent(_fixedInterfaceRoot, false);
                return;
            case UIInterfaceType.Activity:
                UIWindowBase.transform.SetParent(_activityInterfaceRoot, false);
                return;
            case UIInterfaceType.Pop:
                UIWindowBase.transform.SetParent(_popInterfaceRoot, false);
                return;
            case UIInterfaceType.Top:
                UIWindowBase.transform.SetParent(_topInterfaceRoot, false);
                return;
            default:
                UIWindowBase.transform.SetParent(_canvasRoot.transform, false);
                return;
        }
    }

    public void CanvasRenderMode(RenderMode renderMode)
    {
       // _canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;

        if (renderMode==RenderMode.ScreenSpaceCamera)
        {
            _canvas.GetComponent<Canvas>().worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        }
        _canvas.GetComponent<Canvas>().renderMode = renderMode;
    }
}
