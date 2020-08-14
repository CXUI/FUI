using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class UDNetworkManager : MonoBehaviour {

    //private Dictionary<string, AssetBundle> _assetBundle = new Dictionary<string, AssetBundle>();
    private static UDNetworkManager _instance;

    public static UDNetworkManager Instance
    {
        get
        {if (_instance == null) { _instance = new GameObject("NetWorkManager").AddComponent<UDNetworkManager>(); DontDestroyOnLoad(_instance); }
            return _instance;}

       private set
        {    _instance = value;}
    }
    public Dictionary<string, AssetBundle> _dicAssetBundle = new Dictionary<string, AssetBundle>();
   
    /// <summary>
    /// 加载AB包资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="abName"></param>
    /// <param name="path"></param>
    /// <param name="unityEvent"></param>
    /// <param name="str"></param>
    public void GetAssetBundel<T>(string abName, string path, UnityAction<T> unityEvent = null,bool isState=false)where T : Object
    {
           
        if (_dicAssetBundle.ContainsKey(abName))
        {
            if (unityEvent != null) StartCoroutine(LoadObject(_dicAssetBundle[abName], abName, unityEvent, isState));
        }
        else
        {
            StartCoroutine(LoadAssetBundle(abName, path, unityEvent, isState));
        }
    }

    /// <summary>
    /// 加载音频资源
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="url"></param>
    /// <param name="unityEvent"></param>
    public void GetAudioClip(string abName, string url, UnityAction<AudioClip> unityEvent)
    {
        StartCoroutine(LoadAudioClip(abName, url, unityEvent));
    }

    /// <summary>
    /// 加载AB包
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="abName"></param>
    /// <param name="url"></param>
    /// <param name="unityEvent"></param>
    /// <returns></returns>
    private IEnumerator LoadAssetBundle<T>(string abName, string url, UnityAction<T> unityEvent=null,bool isState=false)where T:Object
    {
#if UNITY_EDITOR
        url = Application.streamingAssetsPath + UIResourceDefine.ResourceModelPath[(int)BuildTargets.Windows] + url;
#else
        url = Application.streamingAssetsPath + UIResourceDefine.ResourceModelPath[(int)BuildTargets.Webgl] + url;
#endif
        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(url);
        
        yield return request.SendWebRequest();  //注意是异步方式所以必须等待他完成后再执行

        if (!string.IsNullOrEmpty(request.error))
        {
            Debug.LogError(request.error + url);
            yield break; //跳出协程
        }
        AssetBundle ab = (request.downloadHandler as DownloadHandlerAssetBundle).assetBundle;
        if (!_dicAssetBundle.ContainsKey(abName))
        {
            _dicAssetBundle.Add(abName, ab);
        }
        if (unityEvent != null) StartCoroutine(LoadObject(ab, abName, unityEvent, isState));

    }

    /// <summary>
    /// 解压资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="asset"></param>
    /// <param name="abName"></param>
    /// <param name="unityEvent"></param>
    /// <returns></returns>
    private IEnumerator LoadObject<T>(AssetBundle asset, string abName, UnityAction<T> unityEvent,bool isState=false) where T : Object
    {
     
        //使用资源
        AssetBundleRequest modelAB = asset.LoadAssetAsync<T>(abName);
        while (!modelAB.isDone)
        {
            yield return null;
        }
        // model.transform.localPosition = Vector3.zero;会导致碰撞无法检测
        T model = modelAB.asset as T;
        //model.name = abName;
        if (unityEvent != null) unityEvent(model);
       if(isState)asset.Unload(false);//卸载未使用的资源
        yield return null;
    }

    /// <summary>
    /// 加载音频
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="url"></param>
    /// <param name="unityEvent"></param>
    /// <returns></returns>
    private IEnumerator LoadAudioClip(string abName, string url, UnityAction<AudioClip> unityEvent)
    {
#if UNITY_EDITOR
        //path = Application.streamingAssetsPath + UIResourceDefine.ResourceModelPath[(int)BuildTargets.Webgl] + path;
        url = Application.streamingAssetsPath + UIResourceDefine.ResourceModelPath[(int)BuildTargets.Windows] + url;
#else
        url = Application.streamingAssetsPath + UIResourceDefine.ResourceModelPath[(int)BuildTargets.Webgl] + url;
#endif
        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(url);
        //if(abShare==null)abShare= AssetBundle.LoadFromFile(Application.streamingAssetsPath+ "/Model/AssetBundles/WebGL/material.mater");
        yield return request.SendWebRequest();  //注意是异步方式所以必须等待他完成后再执行
        //yield return abShare;  //注意是异步方式所以必须等待他完成后再执行
        if (!string.IsNullOrEmpty(request.error))
        {
            Debug.LogError(request.error + url);
            yield break; //跳出协程
        }
        AudioClip ab = (request.downloadHandler as DownloadHandlerAudioClip).audioClip;
        if (unityEvent != null) unityEvent(ab);
    }

    /// <summary>
    /// 字节流上传服务器
    /// </summary>
    /// <param name="url"></param>
    /// <param name="bytes"></param>
    /// <param name="action"></param>
    /// <param name="contentType"></param>
    /// <returns></returns>
    private IEnumerator UPLoadByByte(string url,byte[] bytes,UnityAction action,string contentType="application/octet-stream")
    {
        using (UnityWebRequest uwr=new UnityWebRequest())
        {
            UploadHandler upload = new UploadHandlerRaw(bytes);
            upload.contentType = contentType;
            yield return uwr.SendWebRequest();
            if (uwr.isNetworkError||uwr.isHttpError)
            {
                Debug.LogError("错误");
            }
            if (action != null) action();
        }

    }

}
