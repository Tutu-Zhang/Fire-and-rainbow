using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class CardItem : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler, IPointerDownHandler, IPointerClickHandler
{
    public Dictionary<string, string> data;//������Ϣ
    int num;
    private bool IsInPlayArea = false;
    protected int cardNum;

    public int GetCardNum()
    {
        return cardNum;
    }

    public void Init(Dictionary<string, string> data,int index)
    {
        this.data = data;
        this.num = index;
    }

    private int index;

    //������
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(1.1f, 0.2f);//�Ŵ�
        index = transform.GetSiblingIndex();
        transform.SetAsLastSibling();

    }
    //����Ƴ�
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1, 0.2f);
        transform.SetSiblingIndex(index);


    }
    public void OnPointerDown(PointerEventData eventData)
    {
        transform.DOScale(1.2f, 0.25f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("click");
        if (!IsInPlayArea && FightManager.Instance.fightUnit is FightPlayerTurn)
        {
            if(UIManager.Instance.GetUI<FightUI>("fightBackground").MoveCardToPlayArea(this))
                IsInPlayArea = true;
        }
        else if(IsInPlayArea && FightManager.Instance.fightUnit is FightPlayerTurn)
        {
            UIManager.Instance.GetUI<FightUI>("fightBackground").MoveCardToHandArea(this);
            IsInPlayArea = false;
        }
    }

    public void SetPlayArea(bool temp)
    {
        IsInPlayArea = temp;
    }

    private void Start()
    {
        //Id	Name	Script	Type	Des	BgIcon	Icon	Expend	Arg0	Effects
        //Ψһ�ı�ʶ�������ظ���	����	������ӵĽű�	�������͵�Id	����	���Ƶı���ͼ��Դ·��	ͼ����Դ��·��	���ĵķ���	����ֵ	��Ч
        //1000	��ͨ����	AttackCardItem	10001	�Ե������˽���{0}����˺�	Icon/BlueCard	Icon/sword_03e	1	3	Effects/GreenBloodExplosion
        //Debug.Log("���γ��Ƶ�"+ num +"���Ƶ�����Ϊ"+data["BgIcon"]);
        //Transform cardArea = GameObject.Find("CardArea").transform;
        this.GetComponent<Image>().sprite = Resources.Load<Sprite>(data["BgIcon"]);
    }


    //��������ʹ�ú���Ч
    public void PlayEffect(Vector3 pos)
    {
        GameObject effectobj = Instantiate(Resources.Load(data["Effects"])) as GameObject;
        effectobj.transform.position = pos;
        Destroy(effectobj, 2);
    }

}
