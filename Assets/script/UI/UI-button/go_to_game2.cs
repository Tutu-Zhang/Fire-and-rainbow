using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class go_to_game2 : MonoBehaviour
{
    // Start is called before the first frame update
    public Button button;

    private void Start()//��ʼʱִ��
    {
        button.onClick.AddListener(GotoNew);//����Ϊbutton1��button�����ʱ��ִ��Gotonew����,button1��Ҫ��unity�и�ֵ
    }
    private void GotoNew()
    {
        AudioManager.Instance.PlayEffect("��ť");
        LevelManager.Instance.level = 2;

        SceneManager.LoadScene("BeforeGame");
    }
}
