using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//抽卡效果卡牌
public class DrawCardItem : CardItem
{
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (TryUse() == true)
        {
            int val = int.Parse(data["数值栏"]);//抽卡数量

            if (FightCardManager.Instance.HasCard() == true)
            {
                UIManager.Instance.GetUI<FightUI>("FightUI").CreatCardItem(val);

                UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardItemPos();

                Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2.5f));

                PlayEffect(pos);
            }
            else
            {
                base.OnEndDrag(eventData);
            }
        }
        else
        {
            base.OnEndDrag(eventData);
        }   
    }
}
