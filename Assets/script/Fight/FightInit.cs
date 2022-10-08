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

        //��ʼ���ƿ��б�
        //RoleManager.Instance.Init();

        //��ʼ��ս����ֵ
        FightManager.Instance.Init();

        //����ս��bgm������ֻ��Ҫ����bgm�����־Ϳ���
        AudioManager.Instance.Init();
        AudioManager.Instance.PlayBGM("fightBGM");

        //���ص�����Դ
        EnemyManager.Instance.loadRes("10001");

        //��ʼ��ս����������
        FightCardManager.Instance.Init();

        //����ս��Ԫ��
        UIManager.Instance.ShowUI<FightUI>("fightBackground");
        //UIManager.Instance.ShowUI<LoginUI>("LoginUI");

        //�л�����һغ�
        FightManager.Instance.ChangeType(FightType.Player);
    }

    public override void OnUpdate()
    {
        
    }
}
