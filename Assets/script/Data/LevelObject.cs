using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObject : MonoBehaviour
{

    private void Awake()
    {
        //初始化音频管理器
        //AudioManager.Instance.Init();

        GameObject[] objs = GameObject.FindGameObjectsWithTag("level");

        if (objs.Length > 1)
        {
            Destroy(objs[1]);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}