using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//防御效果卡牌
public class DefendCardItem : CardItem
{
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (TryUse() == true)
        {
            //Id	Name	Script	Type	Des	BgIcon	Icon	Expend	Arg0	Effects
            //唯一的标识（不能重复）	名称	卡牌添加的脚本	卡牌类型的Id	描述	卡牌的背景图资源路径	图标资源的路径	消耗的费用	属性值	特效
            //1000	普通攻击	AttackCardItem	10001	对单个敌人进行{0}点的伤害	Icon/BlueCard	Icon/sword_03e	1	3	Effects/GreenBloodExplosion
           
            //使用效果
            int val = int.Parse(data["数值栏"]);

            //播放使用后声音
            AudioManager.Instance.PlayEffect("文件路径/防御卡音效");

            //发挥效果
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
