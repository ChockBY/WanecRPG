using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Progressbar : MonoBehaviour {

    //UI进度条
    public Image image;  //滑动条
                                //目的是对场景进行控制 获取进度值 和允许显示
    private AsyncOperation _async;
    //UI应该达到的进度
    private int _currProgress;
    //1. 获取滑动条
    //协同加载(异步加载 不断获取进度值 经过计算赋值给滑动条)
    // Use this for initialization
    void Start()
    {
        _currProgress = 0;
        _async = null;
        StartCoroutine("LoadScene");
    }

    // Update is called once per frame
    void Update()
    {
        //目的就是现实进度
        image.fillAmount = _currProgress / 100.0f;
    }

    IEnumerator LoadScene()
    {
        //临时的进度
        int tmp;
        //异步加载
        _async = SceneManager.LoadSceneAsync("Demo");  //跳转场景为S3

        //先不显示场景 等到进度为100%的时候显示场景 必须的!!!!
        _async.allowSceneActivation = false;
        #region 优化进度的 
        while (_async.progress < 0.9f)
        {
            //相当于滑动条应该到的位置
            tmp = (int)_async.progress * 100;

            //当滑动条 < tmp 就意味着滑动条应该变化
            while (_currProgress < tmp)
            {
                ++_currProgress;
                yield return new WaitForEndOfFrame();
            }
        }//while end   进度为90%

        tmp = 100;
        while (_currProgress < tmp)
        {

            ++_currProgress;
            yield return new WaitForEndOfFrame();
        }
        #endregion
        //处理进度为0 ~0.9的0

        //进度条完成 允许显示
        _async.allowSceneActivation = true;

    }
}
