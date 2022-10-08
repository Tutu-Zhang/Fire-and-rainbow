using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//��Ϸ���
public class GameStart : MonoBehaviour
{
    //��ʼ���������չʾ��ʼUI������BGM
    void Start()
    {

        //��ʼ����Ƶ������
        AudioManager.Instance.Init();

        //��LoginUI���ؽ�UI�б�չʾ
        UIManager.Instance.ShowUI<LoginUI>("LoginUI");


        //����BGM
        AudioManager.Instance.PlayBGM("beginBGM");//�ڴ������ʼBGM
    }

}
