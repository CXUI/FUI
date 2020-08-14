using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 第一层UI配置
/// </summary>
public class MainPathConfig
{
    public string _key;
    public string _path;
    public string _text;
    public string _value;
}
/// <summary>
/// 第二层UI配置
/// </summary>
public class PathConfig
{
    public string _key;
    public string _path;
    public string _text;
    public string _scene;
    public bool _istrigger;
    public string _value;
}


/// <summary>
/// 第三层配置
/// </summary>
public class ContentConfig
{
    public string _index;
    public string _text;
    public string _layer;
    public bool _localVideo;
    public string _value;
}

/// <summary>
/// 器械认知
/// </summary>
public class ToolsPathConfig
{
    public string _index;//编号
    public string _name;//按钮名称
    public string _type;//器械分类
    public string _modelName;//物体的名称
    public string _path;//模型路径
    public string _sprite;//模型图标
    public string _normalFrame;//按钮选中图标
    public string _notFrame;//按钮未选中图标
    public string _Text;//文字介绍
    public DeviceShowState _deviceShow;//工具展示类型
    public string _video;//视频路径
}

/// <summary>
/// 虚拟的安装方法介绍
/// </summary>
public class VirtualMethoIntroduction
{
    public string _name;
    public bool _isAnimatorSele;
    public string _sprite;
    public bool _isVideoSele;
    public string _video;
}

/// <summary>
/// 操作步骤 ：虚拟演示，
/// </summary>
public class VirtualDemonStationConfig
{
    public string _key;
    public string _stepOrder;
    public string _score;
    public string _timeLimit;
    public string _step;
    public string _animatorName;
    public string _animatorModel;
}
/// <summary>
/// 虚拟训练，选择题  
/// </summary>
public class ChoiceQuestion
{
    public ChoiceQuestionType _type;//题目类型
    public string _state;//题目类型
    public string _index;//题目编号
    public string _title;//题目标题
    public string _answer;//题目答案
    public string _problem;//问题
}
/// <summary>
///选择题类型
/// </summary>
public enum ChoiceQuestionType
{
    Single,//单选
    Multiple,//多选
    Cognitive,//认知题
}

/// <summary>
/// 操作步骤：视频演示
/// </summary>
public class VideoDemonStratedConfig
{
    public string _type;
    public string _path;
    public bool _islocal;
}

/// <summary>
/// 图片图集
/// </summary>
public class SpriteAtlasConfig
{
    public string _key;
    public string _name;
    public string _path;
}
public class UDDataConfigManager
{

}


