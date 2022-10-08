using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
//��ʼ����
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