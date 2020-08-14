using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDFindObject : MonoBehaviour
{

    /// <summary>
    /// 寻找物体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parents"></param>
    /// <param name="objName"></param>
    /// <returns></returns>
    public static T FindNote<T>(GameObject parents, string objName) where T : Component
    {
        Transform trans = FindChlid(parents, objName);

        if (trans != null)
        {
            return trans.gameObject.GetComponent<T>();
        }
        else
        {
 
            return null;
        }
    }

    private static Transform FindChlid(GameObject parents, string objName)
    {
        Transform obj = parents.transform.Find(objName);

        if (obj == null)
        {
            foreach (Transform temp in parents.transform)
            {
                obj = FindChlid(temp.gameObject, objName);

                if (obj != null)
                {
                    return obj;
                }
            }
        }
        return obj;
    }

    /// <summary>
    /// 获得物体下所有子物体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parents"></param>
    /// <returns></returns>
    public static List<T> FindAllChilds<T>(GameObject parents)
    {

        List<T> tempList = new List<T>();
        foreach (Transform temp in parents.transform)
        {
            if (temp.GetComponent<T>() != null)
            {
                tempList.Add(temp.GetComponent<T>());
            }
        }

        return tempList;
    }

    public   static Transform FindParnet<T>(GameObject parents) where T:Object
    {
        Transform parent = parents.transform.parent;

        if (parent!=null)
        {
            if (parent.GetComponent<T>()!=null)
            {
                return parent;
            }
            else
            {
                parent= FindParnet<T>(parent.gameObject);
            }
        }

        return parent;
    }
}
