using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEnumType : MonoBehaviour
{


}

/// <summary>
/// 窗口弹出提示框类型
/// </summary>
public enum UIInterfaceTop
{
    No,//不提示
    WarningTop,//弹出警告提示框
}
//窗口显示类型
public enum UIInterfaceShowMode
{
    Freeze,  //冻结
    NoFreeze, //不冻结
}
/// <summary>
/// 窗口类型
/// </summary>
public enum UIInterfaceType
{
    Fixed,     //固定
    Activity,  //活动窗口
    Pop,       //弹出
    Top        //最上层窗口
}
/// <summary>
/// 窗口名称
/// </summary>
public enum UIInterfaceName
{
    //第一层级
    Interface_Login = 0,
    Interface_Canvas,
    Interface_UIMain,
    //第二层级
    Interface_TheoreticalStudy,
    Interface_DeviceCognition,
    Interface_OperationSteps,
    Interface_VirtualExercises,
    Interface_VirtualTesting,

    //第三层级

    //第四层级
    Interface_UIPop,
    Interface_Progress,
}

public enum AddressConfigure
{
    MainConfigure,//初始配置页面
    Knowledge,
}

/// <summary>
/// 器械展示类型
/// </summary>
public enum DeviceShowState
{
    video,
    animation,
    no,
}

/// <summary>
/// ab包平台
/// </summary>
public enum BuildTargets
{
    Windows,
    Webgl,
    Iphone,
    Android,
}
/// <summary>
/// 视频平台
/// </summary>
public enum VideoTargets
{
    Windows,
    Webgl,
}



public static class UIResourceDefine
{
    /// <summary>
    /// 界面预制体字典
    /// </summary>
    public static Dictionary<int, string> InterfacePrefabPath = new Dictionary<int, string>()
        {
            { (int)UIInterfaceName.Interface_Login,                          "UILogin" },
            { (int)UIInterfaceName.Interface_Canvas,                         "MainStartUICanvas" },
            { (int)UIInterfaceName.Interface_UIMain,                         "UIMain" },
            { (int)UIInterfaceName.Interface_UIPop,                          "UIPopInterface" },
            { (int)UIInterfaceName.Interface_TheoreticalStudy,               "UITheoreticalStudy" },
            { (int)UIInterfaceName.Interface_DeviceCognition,                "Interface_VirtualTesting" },
            { (int)UIInterfaceName.Interface_OperationSteps,                 "UIOperationSteps" },
            { (int)UIInterfaceName.Interface_VirtualExercises,               "UIVirtualExercises" },
            { (int)UIInterfaceName.Interface_VirtualTesting,                 "UIVirtualTesting" },
            { (int)UIInterfaceName.Interface_Progress,                       "UISceneLoading" },

        };
    public static Dictionary<int, string> ResourceModelPath = new Dictionary<int, string>()
    {
        {(int)BuildTargets.Windows,"/Model/AssetBundles/Windows/" },//window 平台包
        {(int)BuildTargets.Webgl,"/Model/AssetBundles/Webgl/" },//webgl 平台包
        {(int)BuildTargets.Android,"/Model/AssetBundles/Android/" },//安卓包
        {(int)BuildTargets.Iphone,"/Model/AssetBundles/Iphone/" },//苹果包
    };

}

[System.Serializable]
public class ScoreDetail
{
    public uint id;
    public string itemid;
    public string setpName;
    public string correctOperation;
    public string actualOperation;
    public string score;
    public int getScore;
    public uint time;
    public ScoreDetail(string setpName, string correctOperation, string actualOperation, string score, int getScore)
    {

        this.setpName = setpName;
        this.correctOperation = correctOperation;
        this.score = score;
        this.actualOperation = actualOperation;
        this.score = score;
        this.getScore = getScore;
        //this.time = time;
    }
    public ScoreDetail()
    { }
}


