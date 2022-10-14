using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
//��ʼ����
public class LoginUI : UIBase
{
    private void Awake()
    {
        Register("beginButton").onClick = onStartGameBtn;
    }

    private void onStartGameBtn(GameObject obj,PointerEventData pData)
    {
        //ͨ���رտ�ʼ������ʵ����תҳ��
        Hide();

        //���ز���ʾѡ�����
        UIManager.Instance.ShowUI<SelectUI>("SelectUI");
    }
}
