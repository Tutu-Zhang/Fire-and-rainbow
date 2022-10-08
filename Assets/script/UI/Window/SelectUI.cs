using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
//开始界面
public class SelectUI : UIBase
{
    private void Awake()
    {
        Register("back").onClick = onSelectGameBtn;
    }

    private void onSelectGameBtn(GameObject obj, PointerEventData pData)
    {
        Hide();

        UIManager.Instance.ShowUI<LoginUI>("LoginUI");

    }
}