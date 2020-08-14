using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using UnityEngine.Networking;
using System.Text;

public class UDXmlParse
{
    /// <summary>
    /// 打开xml文件
    /// </summary>
    /// <param name="filePath">xml路径</param>
    /// <returns></returns>
    public static XmlNode OpenXmlFile(string filePath)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(filePath);
        XmlNode rootNode = xmlDoc.DocumentElement;
        return rootNode;
    }
    /// <summary>
    /// 加载xml文件
    /// </summary>
    /// <param name="fileXml">xml文件内容</param>
    /// <returns></returns>
    public static XmlNode LoadXmlFile(string fileXml)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(fileXml);
        XmlNode rootNode = xmlDoc.DocumentElement;
        return rootNode;
    }

}
