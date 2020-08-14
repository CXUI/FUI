using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
/// <summary>
/// 场景进度条
/// </summary>
public class UDLoadSceneProgress : UDUIInterfaceBase
{

    public bool _isProgress = false;
    //private GameObject _processCanvas;//加载进度动画
    public Image _procrssSlider;//进度条
    private Image _processBall;//进度球
    public Text _processText;//进度字符
    private int _sliderSpeed = 1;
    public Text _infromation;

    public Transform _slider;
    public Image _bgImage;

    public void Init(string infromation = "")
    {
        UDLoadSceneController.Instance._canvas.GetComponent<Canvas>().sortingOrder = 100;
        gameObject.SetActive(true);
        _slider.gameObject.SetActive(true);
        _bgImage.transform.localScale=new Vector3(1,1,1);
        _procrssSlider.fillAmount = 0;
        _processText.text = "";
        if (infromation != "") { _infromation.text = infromation; } else { _infromation.text = "内容加载中"; } 
    }

    public bool ShowProcress(float value)
    {
        if (_procrssSlider.fillAmount < value)
        {
            _procrssSlider.fillAmount += Time.deltaTime * _sliderSpeed;
            _processText.text = ((_procrssSlider.fillAmount) * 100).ToString("f0") + "%";
        }
        else
        {
            return false;
        }
        if (_procrssSlider.fillAmount >= value)
        {

          //  StartCoroutine(SceneActivation());
        }
        return true;
    }

    public void SceneActia()
    {
        StartCoroutine(SceneActivation());
    }

    private IEnumerator SceneActivation()
    {
        yield return new WaitForSeconds(1f);
        _slider.gameObject.SetActive(false);
        UDLoadSceneController.Instance._loadSceneOperation.allowSceneActivation = true;
        _bgImage.transform.DOScaleY(0, 1f);
        yield return new WaitForSeconds(1f);
        UDLoadSceneController.Instance._canvas.GetComponent<Canvas>().sortingOrder = 0;
        transform.gameObject.SetActive(false);
    }
}
