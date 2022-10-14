using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
//开始界面
public class LoginUI : UIBase
{
    private void Awake()
    {
        Register("beginButton").onClick = onStartGameBtn;
    }

    private void onStartGameBtn(GameObject obj,PointerEventData pData)
    {
        //通过关闭开始界面来实现跳转页面
        Hide();

        //加载并显示选择界面
        UIManager.Instance.ShowUI<SelectUI>("SelectUI");
    }
}
