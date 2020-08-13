using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;



/// <summary>
/// 场景控制脚本
/// </summary>
public class UDLoadSceneController : MonoBehaviour
{

    private static UDLoadSceneController instance;

    public static UDLoadSceneController Instance
    {
        get
        {
            if (instance == null) { instance = new GameObject("SceneController").AddComponent<UDLoadSceneController>(); }
            return instance;
        }
    }

    public bool _isProgress = false;
    public Transform _canvas;
    UDLoadSceneProgress progress;
    private IEnumerator _waitShowProcess;
    public AsyncOperation _loadSceneOperation;
    public void Awake()
    {
        if (instance == null) instance = this;
        DontDestroyOnLoad(this);
       _canvas= ((GameObject)Instantiate(Resources.Load("UIPrefabs/LoadSceneCanvas"))).transform;
        DontDestroyOnLoad(_canvas);
    }

    public void LoadAnsy(string sceneName,UnityAction action,bool isProgress, bool isadditive)
    {
        if (SceneManager.GetSceneByName(sceneName).isLoaded) return;
        //if (SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName))) return;
        StartCoroutine(LoadAnsyScene(sceneName, action, isProgress, isadditive));
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private  IEnumerator LoadAnsyScene(string sceneName, UnityAction action = null,bool isProgress=false,bool additive=false)
    {
        if (additive) { _loadSceneOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive); } else
        {_loadSceneOperation = SceneManager.LoadSceneAsync(sceneName);}

        _loadSceneOperation.allowSceneActivation = false;
        if (isProgress) {
          
            if (_waitShowProcess!=null)
            {
                StopCoroutine(_waitShowProcess);
            }
            _waitShowProcess = WaitShowProcess();
            StartCoroutine(_waitShowProcess);
         //   ShowProcess();
        } else { _loadSceneOperation.allowSceneActivation = true; }
        yield return _loadSceneOperation;
        if (action != null) { action(); }

        Scene scene = SceneManager.GetSceneByName(sceneName);

        SceneManager.SetActiveScene(scene);

        // Debug.Log();
    }
    private AsyncOperation _unloadScene;
    public void UnLoadScene(string scenName)
    {
        SceneManager.UnloadSceneAsync(scenName);
    }
    public void UnLoadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.UnloadSceneAsync(scene);
    }

    public IEnumerator UnLoadAnsyScene(string scenName)
    {
        if (SceneManager.GetSceneByName(scenName).isLoaded)
        {
            _unloadScene = SceneManager.UnloadSceneAsync(scenName);

            yield return _unloadScene;
            Debug.Log("场景卸载完成");
        }
        else
        {
            Debug.Log("场景未加载");
        }

        yield return null;
    }

    private IEnumerator WaitShowProcess()
    {
        if (progress == null) { progress = ((GameObject)Instantiate(Resources.Load("UIPrefabs/UISceneLoading"), _canvas)).GetComponent<UDLoadSceneProgress>(); };
       // progress= slider.GetComponent<UDLoadSceneProgress>();
        progress.Init();
        _isProgress = true;
        while (_isProgress)
        {
            yield return new WaitForFixedUpdate();
 
            _isProgress= progress.ShowProcress(_loadSceneOperation.progress/0.9f);

        }
       
        if (!_isProgress)
        {
             StartCoroutine(SceneActivation());
        }
        //
    }
    public  IEnumerator SceneActivation()
    {
        // yield return new WaitForSeconds(1f);
        yield return new WaitForFixedUpdate();
        //_loadSceneOperation.allowSceneActivation = true;
           progress.SceneActia();
    }
 
}
