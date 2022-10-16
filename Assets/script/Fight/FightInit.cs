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
        if(AudioManager.Instance != null)
            AudioManager.Instance.PlayBGM("fightBGM");


        //��ʼ��ս����������
        FightCardManager.Instance.Init();

        //����ս��Ԫ��
        UIManager.Instance.ShowUI<FightUI>("fightBackground");
        //UIManager.Instance.ShowUI<LoginUI>("LoginUI");

        //���ص�����Դ
        EnemyManager.Instance.loadRes("10001");

        //�л�����һغ�
        FightManager.Instance.ChangeType(FightType.Player);
        //FightManager.Instance.GetPlayHit(15);
    }

    public override void OnUpdate()
    {
        
    }
}
