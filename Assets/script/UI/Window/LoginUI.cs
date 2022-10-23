using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
//开始界面
public class LoginUI : UIBase
{
    private void Start()
    {
        Register("beginButton").onClick = onStartGameBtn;

        if (!AudioManager.Instance.isPlayingBeginBGM)
        {
            Debug.Log("LoginUI播放BGM");
            AudioManager.Instance.PlayBGM("开场BGM");
        }

    }

    private void onStartGameBtn(GameObject obj,PointerEventData pData)
    {
        /*//通过关闭开始界面来实现跳转页面
        Hide();

        //加载并显示选择界面
        UIManager.Instance.ShowUI<SelectUI>("SelectUI");*/

        AudioManager.Instance.PlayEffect("按钮");
        
        //跳转到关卡选择场景
        SceneManager.LoadScene("selectScene");
    }
}
