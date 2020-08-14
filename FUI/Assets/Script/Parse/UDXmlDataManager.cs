using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.Events;
public partial class UDXmlDataManager : UDSingleton<UDXmlDataManager>
{

    //路径配置文件的路径
    private string _pathOfDirectoryConfig = "/Config/path_config.xml";
    private string path;
    public bool localVideo;//本地视频
    public bool localModel;//本地模式
 
    private UnityAction<string> configEvent;
 
    void Awake()
    {
        _singName = "XmlDataManager";
        _pathOfDirectoryConfig = Application.streamingAssetsPath + _pathOfDirectoryConfig;
 
    }


    public void PathConfig()
    {
        _pathOfDirectoryConfig = Application.streamingAssetsPath + "/Config/global_config.xml";
        configEvent = ConfigPath;
        ReadXml(_pathOfDirectoryConfig, configEvent);
    }

    /// <summary>
    /// 读取xml
    /// </summary>
    /// <param name="path">xml路径</param>
    /// <param name="ReadxmlEvent">解析xml事件</param>
    public void ReadXml(string path, UnityAction<string> ReadxmlEvent)
    {
        StartCoroutine(ReadXmlInformation(path, ReadxmlEvent));
    }

    /// <summary>
    /// 读取xml
    /// </summary>
    /// <param name="path">xml路径</param>
    /// <param name="ReadxmlEvent">解析xml事件</param>
    /// <returns></returns>
    private IEnumerator ReadXmlInformation(string path, UnityAction<string> ReadxmlEvent)
    {
        WWW www = new WWW(path);
        while (!www.isDone)
        {
            // Debug.LogError("解析xml错误");
            yield return 0;
        }

       if(ReadxmlEvent!=null) ReadxmlEvent(www.text);
    }
   
    //路径配置
    private void ConfigPath(string xml)
    {
        if (path == null)
        {
            //ToolSpriteDic = new Dictionary<string, SpriteAtlasConfig>();
            var xmlNode = UDXmlParse.LoadXmlFile(xml);
            var pathList = xmlNode.SelectNodes("config");
  
            foreach (XmlNode temp in pathList)
            {
                path = temp.Attributes["path"].Value;
            }
            var videoOpen = xmlNode.SelectNodes("local");
            foreach (XmlNode temp in videoOpen)
            {
                localVideo = temp.Attributes["video"].Value == "true" ? true : false;
                localModel = temp.Attributes["model"].Value == "true" ? true : false;
            }
            var toolsSprite = xmlNode.SelectNodes("Atlas/Sprite");
            foreach (XmlNode temp in toolsSprite)
            {
                SpriteAtlasConfig sa = new SpriteAtlasConfig()
                {
                    _key = temp.Attributes["key"].Value,
                    _name = temp.Attributes["name"].Value,
                    _path = temp.Attributes["path"].Value,
                };
            }
        }
    }
}

