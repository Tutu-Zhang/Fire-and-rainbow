using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

//攻击效果卡牌
public class AttackCardItem : CardItem, IPointerDownHandler
{


    private IEnumerator OnMouseDownRight(PointerEventData pData)
    {
        while (true)
        {
            //如果在次按下鼠标右键，跳出循环
            if (Input.GetMouseButton(1))
            {
                break;
            }

            Vector2 pos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                transform.parent.GetComponent<RectTransform>(),
                pData.position,
                pData.pressEventCamera,
                out pos))
            {
                //进行线性检测是否碰到怪物
                CheckRayToEnemy();
            }

            yield return null;
        }

        //跳出循环后，显示鼠标
        Cursor.visible = true;
    }

    Enemy hitEnemy;

    private void CheckRayToEnemy()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray,out hit,10000,LayerMask.GetMask("Enemy")))
        {
            hitEnemy = hit.transform.GetComponent<Enemy>();

            hitEnemy.OnSelect();//选中

            //如果按下鼠标左键 使用攻击卡
            if (Input.GetMouseButtonDown(0))
            {
                //关闭所有协同程序
                StopAllCoroutines();

                //鼠标显示
                Cursor.visible = true;

                if (TryUse() == true)
                {
                    //播放特效
                    PlayEffect(hitEnemy.transform.position);

                    //播放打击音效
                    AudioManager.Instance.PlayEffect("打击音效路径");

                    //敌人受伤
                    int val = int.Parse(data["数值"]);
                    hitEnemy.Hited(val);

                }
                //敌人未选中
                hitEnemy.OnUnSelect();

                //设置敌人脚本为null
                hitEnemy = null;
            }

        }
        else
        {
            //未选中怪物
            if (hitEnemy != null)
            {
                hitEnemy.OnUnSelect();
                hitEnemy = null;

            }
        }
    }
}
