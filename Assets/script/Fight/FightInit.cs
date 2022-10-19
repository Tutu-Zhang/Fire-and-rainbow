using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//����ս����ʼ��
public class FightInit : FightUnit
{
    //�����ҳ��ʱ����
    public void Start()
    {

        //��ʼ�����ñ�
        GameConfigManager.Instance.Init();

        //��ʼ��ս����ֵ
        FightManager.Instance.Init();


        //����ս��bgm������ֻ��Ҫ����bgm�����־Ϳ���
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayBGM("fightBGM");


        //��ʼ��ս����������
        FightCardManager.Instance.Init();

        //����ս��Ԫ��
        UIManager.Instance.ShowUI<FightUI>("fightBackground");

        //���عؿ���Դ
        switch (LevelManager.Instance.level)
        {
            case 0:
                EnemyManager.Instance.loadRes("10000");
                break;
            case 1:
                EnemyManager.Instance.loadRes("10001");
                break;
            case 2:
                EnemyManager.Instance.loadRes("10002");
                break;
            case 3:
                EnemyManager.Instance.loadRes("10003");
                break;
            case 4:
                EnemyManager.Instance.loadRes("10004");
                break;
        }

        //�л�����һغ�
        FightManager.Instance.ChangeType(FightType.Player);
    }

    public override void OnUpdate()
    {

    }
}
