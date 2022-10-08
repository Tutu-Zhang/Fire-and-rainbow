using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//����Ч������
public class DefendCardItem : CardItem
{
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (TryUse() == true)
        {
            //Id	Name	Script	Type	Des	BgIcon	Icon	Expend	Arg0	Effects
            //Ψһ�ı�ʶ�������ظ���	����	������ӵĽű�	�������͵�Id	����	���Ƶı���ͼ��Դ·��	ͼ����Դ��·��	���ĵķ���	����ֵ	��Ч
            //1000	��ͨ����	AttackCardItem	10001	�Ե������˽���{0}����˺�	Icon/BlueCard	Icon/sword_03e	1	3	Effects/GreenBloodExplosion
           
            //ʹ��Ч��
            int val = int.Parse(data["��ֵ��"]);

            //����ʹ�ú�����
            AudioManager.Instance.PlayEffect("�ļ�·��/��������Ч");

            //����Ч��
            FightManager.Instance.DefCount += val;
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateDef();

            Vector3 pos = Camera.main.transform.position;
            pos.y = 0;
            PlayEffect(pos);

        }
        else
        {
            base.OnEndDrag(eventData);
        }
    }
}
