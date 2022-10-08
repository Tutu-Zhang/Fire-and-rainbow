using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//游戏入口
public class GameStart : MonoBehaviour
{
    //开始界面仅负责展示开始UI，播放BGM
    void Start()
    {

        //初始化音频管理器
        AudioManager.Instance.Init();

        //将LoginUI加载进UI列表并展示
        UIManager.Instance.ShowUI<LoginUI>("LoginUI");


        //播放BGM
        AudioManager.Instance.PlayBGM("beginBGM");//在此填入初始BGM
    }

}
