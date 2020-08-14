using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
/// <summary>
/// 加载ab包
/// </summary>
public class UDLoadAB : MonoBehaviour {
    /// <summary>
    /// www加载方式
    /// </summary>
    /// <returns></returns>
    public  static  IEnumerator LoadABObject()
    {

        while (!Caching.ready) //检查cache 缓存是否准备好了
            yield return null;

        //远程服务器下载
        WWW www = WWW.LoadFromCacheOrDownload(@"http://localhost/AssetBundles/cubetest.unity_mode", 1); //从远程服务器上下载AB包
        yield return www;

        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.error);
            yield break; //跳出协程
        }

        AssetBundle ab = www.assetBundle;

        //使用资源
        GameObject gameObj = ab.LoadAsset<GameObject>("cubetest");
        Instantiate(gameObj);

        yield return null;
    }

  //static  AssetBundle abShare;
    /// <summary>
    /// WebRequest加载方式
    /// </summary>
    /// <returns></returns>
    public  static  IEnumerator LoadABGameObject<T>(string name,string path,UnityAction<T> unityEvent=null)where T:Object
    {

#if UNITY_EDITOR
            //path = Application.streamingAssetsPath + UIResourceDefine.ResourceModelPath[(int)BuildTargets.Webgl] + path;
            path = Application.streamingAssetsPath + UIResourceDefine.ResourceModelPath[(int)BuildTargets.Windows] + path;
#else
        path = Application.streamingAssetsPath + UIResourceDefine.ResourceModelPath[(int)BuildTargets.Webgl] + path;
#endif
  

        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(path);
        //if(abShare==null)abShare= AssetBundle.LoadFromFile(Application.streamingAssetsPath+ "/Model/AssetBundles/WebGL/material.mater");
        yield return request.SendWebRequest();  //注意是异步方式所以必须等待他完成后再执行
        //yield return abShare;  //注意是异步方式所以必须等待他完成后再执行
        if (!string.IsNullOrEmpty(request.error))
        {
            Debug.LogError(request.error+path);
            yield break; //跳出协程
        }
        AssetBundle ab = (request.downloadHandler as DownloadHandlerAssetBundle).assetBundle;
        //使用资源
        AssetBundleRequest modelAB = ab.LoadAssetAsync<T>(name);
        //GameObject obj = modelAB as GameObject;
        while (!modelAB.isDone)
        {
            yield return null;
        }
        //T model = Instantiate( modelAB.asset as T);
        // model.transform.localPosition = Vector3.zero;会导致碰撞无法检测
        T model = modelAB.asset as T;
        //model.name = name;
        //RefreshShader(model);
        if (unityEvent != null) unityEvent(model);
        ab.Unload(false);//卸载未使用的资源
        yield return null; //  
    }
    /// <summary>
    /// 刷新shader
    /// </summary>
    /// <param name="model"></param>
    private static void RefreshShader(GameObject model)
    {
        FindChlid(model.transform);
        for (int i=0;i< templist.Count;i++)
        {
            if (templist[i].GetComponent<MeshRenderer>() != null)
            {
                string shadername = templist[i].GetComponent<MeshRenderer>().material.shader.name;
                templist[i].GetComponent<MeshRenderer>().material.shader = Shader.Find(shadername);
                //templist[i].gameObject.AddComponent<BoxCollider>();
                Debug.Log(templist[i].GetComponent<BoxCollider>());
            }

        }
        templist.Clear();
    }
   static  List<Transform> templist = new List<Transform>();
    private static   void  FindChlid(Transform parents)
    {
        if (parents.childCount > 0)
        {
            foreach (Transform item in parents)
            {
                FindChlid(item);
               
            }
        }
        templist.Add(parents);
    }
}
