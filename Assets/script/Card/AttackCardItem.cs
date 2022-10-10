using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

//����Ч������
public class AttackCardItem : CardItem, IPointerDownHandler
{


    private IEnumerator OnMouseDownRight(PointerEventData pData)
    {
        while (true)
        {
            //����ڴΰ�������Ҽ�������ѭ��
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
                //�������Լ���Ƿ���������
                CheckRayToEnemy();
            }

            yield return null;
        }

        //����ѭ������ʾ���
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

            hitEnemy.OnSelect();//ѡ��

            //������������� ʹ�ù�����
            if (Input.GetMouseButtonDown(0))
            {
                //�ر�����Эͬ����
                StopAllCoroutines();

                //�����ʾ
                Cursor.visible = true;

                if (TryUse() == true)
                {
                    //������Ч
                    PlayEffect(hitEnemy.transform.position);

                    //���Ŵ����Ч
                    AudioManager.Instance.PlayEffect("�����Ч·��");

                    //��������
                    int val = int.Parse(data["��ֵ"]);
                    hitEnemy.Hited(val);

                }
                //����δѡ��
                hitEnemy.OnUnSelect();

                //���õ��˽ű�Ϊnull
                hitEnemy = null;
            }

        }
        else
        {
            //δѡ�й���
            if (hitEnemy != null)
            {
                hitEnemy.OnUnSelect();
                hitEnemy = null;

            }
        }
    }
}
