using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDSingleton<T> : MonoBehaviour where T:UDSingleton<T> {

    protected static T _instance;

    private static  GameObject _singleton;
    public  static T Instance
    {
        get
        {
            if (_instance==null)
            {
                if (_singleton==null)
                {
                    _singleton = new GameObject("Singleton");
                    DontDestroyOnLoad(_singleton);
                    
                }
                _instance = _singleton.GetComponent<T>();
                if (_instance==null)
                {
                    _instance = _singleton.AddComponent<T>();
                    if(_singName != "")_instance.name = _singName;
                }

            }
            return _instance;
        }
    }



    public static string _singName="";

    public virtual void Singleton() { }
}
