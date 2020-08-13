
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UDResourcesLoadManager : MonoBehaviour
{
    //泛型加载物体
    public static T LoadGameObject<T>(string path) where T : Component
    {
        GameObject obj = null;
        GameObject temp = (GameObject)Resources.Load("UIPrefabs/" + path);
        if (temp != null)
        {
            obj = Instantiate(temp);
            obj.name = path;
            return obj.GetComponent<T>();
        }
        else
        {
            Debug.LogError("这是一个错误的路径" + path);
            return null;
        }
    }

    //加载物体，并设置名字
    public static GameObject LoadGameObject(string name, string path)
    {
        GameObject obj = null;
        GameObject temp = (GameObject)Resources.Load(path);

        if (temp != null)
        {
            obj = Instantiate(temp, Vector3.zero, Quaternion.identity);
            obj.name = name;

        }
        else
        {
             Debug.LogError("这是一个错误的路径" + path);
            return null;
        }
        return obj;
    }
    //加载物体
    public static GameObject LoadGameObject(string path,Vector3 vec, Transform parent =null, string name="")
    {
        GameObject obj = null;
        GameObject temp = (GameObject)Resources.Load(path);

        if (temp != null)
        {
             obj=   parent != null? Instantiate(temp, vec, Quaternion.identity, parent): Instantiate(temp, Vector3.zero, Quaternion.identity);
            if (name!="")
            {
                obj.name = name;
            }
        }
        else
        {
            Debug.LogError("这是一个错误的路径" + path);
            return null;
        }
        return obj;
    }
   // 加载物体
    public static GameObject LoadGameObject(string path, Transform parent = null, string name = "")
    {
        GameObject obj = null;
        GameObject temp = (GameObject)Resources.Load(path);

        if (temp != null)
        {
            obj = parent != null ? Instantiate(temp, parent) : Instantiate(temp, Vector3.zero, Quaternion.identity);
            if (name != "")
            {
                obj.name = name;
            }
        }
        else
        {
            Debug.LogError("这是一个错误的路径" + path);
            return null;
        }
        return obj;
    }

    public static GameObject LoadGameObject(string path, string name, Transform parent = null)
    {
        GameObject obj = null;
        GameObject temp = (GameObject)Resources.Load(path);
        if (temp != null)
        {
            obj = parent != null ? Instantiate(temp, parent) : Instantiate(temp);
            obj.gameObject.name = name;
        }
        else
        {
            Debug.LogError("这是一个错误的路径" + path);
            return null;
        }
        return obj;
    }

    public static GameObject InstantiateModel(GameObject model)
    {
        return Instantiate(model, Vector3.zero, Quaternion.identity); ;
    }
    public static GameObject InstantiateModel(GameObject model,Transform parent)
    {
        return Instantiate(model, parent); ;
    }
}
