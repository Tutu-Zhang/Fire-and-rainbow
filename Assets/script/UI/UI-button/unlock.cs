using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class unlock : MonoBehaviour
{

    public Button button;

    private void Start()//��ʼʱִ��
    {
        button.onClick.AddListener(Unlock);//����Ϊbutton1��button�����ʱ��ִ��Gotonew����,button1��Ҫ��unity�и�ֵ
    }
    private void Unlock()
    {
        PlayerPrefs.SetString("ifSaved", "noSave");

        PlayerPrefs.SetString("lv0Passed", "no");
        PlayerPrefs.SetString("lv1Passed", "no");
        PlayerPrefs.SetString("lv2Passed", "no");
        PlayerPrefs.SetString("lv3Passed", "no");
        PlayerPrefs.SetString("lv4Passed", "no");

        PlayerPrefs.Save();

        Debug.Log(PlayerPrefs.GetString("ifSaved"));
        Debug.Log("lv0: " + PlayerPrefs.GetString("lv0Passed"));


    }
}
