using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class unlock : MonoBehaviour
{

    public Button button;

    private void Start()//开始时执行
    {
        button.onClick.AddListener(Unlock);//当名为button1的button被点击时，执行Gotonew函数,button1需要在unity中赋值
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
