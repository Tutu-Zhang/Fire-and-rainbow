using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmObject : MonoBehaviour
{

    private void Awake()
    {
        //��ʼ����Ƶ������
        //AudioManager.Instance.Init();

        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");

        if (objs.Length > 1)
        {
            Destroy(objs[1]);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}


