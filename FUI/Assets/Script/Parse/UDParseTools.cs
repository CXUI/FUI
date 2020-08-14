using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// csv、txt解析工具
/// </summary>
public class UDParseTools : MonoBehaviour {

    private static UDParseTools instance;
    private UnityAction unity;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }
    public WWW _www;

    public static UDParseTools Instance
    {
        get
        {
            if (instance==null) { instance = new GameObject("ParseTools").AddComponent<UDParseTools>(); }
            return instance;
        }
        set
        {
            instance = value;
        }
    }

    //private IEnumerator ReadAddress(string address)
    //{
    //    string path = null;
    //    path = Application.streamingAssetsPath + "/"+address;
    //    //#if UNITY_EDITOR
    //    //        path = "file://" + Application.streamingAssetsPath + "/InterfaceAddress.txt";
    //    //#endif
    //    //#if !UNITY_EDITOR
    //    //        path = Application.streamingAssetsPath + "/InterfaceAddress.txt";
    //    //#endif
    //    _www = new WWW(path);
    //    yield return _www;
    //}

    public IEnumerator ReadAddress(string address, UnityAction<WWW> action)
    {
        string path = null;
        path = Application.streamingAssetsPath + "/" + address;
        //#if UNITY_EDITOR
        //        path = "file://" + Application.streamingAssetsPath + "/InterfaceAddress.txt";
        //#endif
        //#if !UNITY_EDITOR
        //        path = Application.streamingAssetsPath + "/InterfaceAddress.txt";
        //#endif
        _www = new WWW(path);
        yield return _www;
        action(_www);

    }
    //public IEnumerator ReadCSVAddress(string address,UnityAction<WWW> action)
    //{
    //    string path = null;
    //    path = Application.streamingAssetsPath + "/" + address;
    //    //#if UNITY_EDITOR
    //    //        path = "file://" + Application.streamingAssetsPath + "/InterfaceAddress.txt";
    //    //#endif
    //    //#if !UNITY_EDITOR
    //    //        path = Application.streamingAssetsPath + "/InterfaceAddress.txt";
    //    //#endif
    //    _www = new WWW(path);
    //    yield return _www;
    //    action(_www);

    //}

    //public IEnumerator  ReadTxtAddress(string address)
    //{
    //    string path = null;
    //    //path = Application.streamingAssetsPath + "/" + address;
    //    path = Application.streamingAssetsPath + "/TextTXT.txt";
    //    //#if UNITY_EDITOR
    //    //        path = "file://" + Application.streamingAssetsPath + "/InterfaceAddress.txt";
    //    //#endif
    //    //#if !UNITY_EDITOR
    //    //        path = Application.streamingAssetsPath + "/InterfaceAddress.txt";
    //    //#endif
    //    _www = new WWW(path);
    //    yield return _www;
    //}
}
