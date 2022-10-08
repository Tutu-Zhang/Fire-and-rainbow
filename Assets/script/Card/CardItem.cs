using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class CardItem : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public Dictionary<string, string> data;//������Ϣ
    int num;

    public void Init(Dictionary<string, string> data,int index)
    {
        this.data = data;
        this.num = index+3;
    }

    private int index;

    //������
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(1.5f, 0.25f);//�Ŵ�
        index = transform.GetSiblingIndex();
        transform.SetAsLastSibling();

        transform.Find("bg").GetComponent<Image>().material.SetColor("_LineColor",Color.yellow);//_LineColor�Ǹ�ʲô����
        transform.Find("bg").GetComponent<Image>().material.SetFloat("_LineWidth", 10);
    }
    //����Ƴ�
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1, 0.25f);
        transform.SetSiblingIndex(index);
        transform.Find("bg").GetComponent<Image>().material.SetColor("_LineColor", Color.black);//���Ǹ�ʲô����
        transform.Find("bg").GetComponent<Image>().material.SetFloat("_LineWidth", 1);

    }

    private void Start()
    {
        //Id	Name	Script	Type	Des	BgIcon	Icon	Expend	Arg0	Effects
        //Ψһ�ı�ʶ�������ظ���	����	������ӵĽű�	�������͵�Id	����	���Ƶı���ͼ��Դ·��	ͼ����Դ��·��	���ĵķ���	����ֵ	��Ч
        //1000	��ͨ����	AttackCardItem	10001	�Ե������˽���{0}����˺�	Icon/BlueCard	Icon/sword_03e	1	3	Effects/GreenBloodExplosion
        Debug.Log(data["BgIcon"]);

        GameObject fightBackground = GameObject.Find("fightBackground");
        fightBackground.transform.GetChild (num).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(data["BgIcon"]);
        //transform.Find("cardBackground(Clone)").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["Icon"]);
        //transform.Find("����ģ��/����").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["Des"]);
        //transform.Find("����ģ��/����").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["Name"]);
        //transform.Find("����ģ��/����").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["Extend"]);
        //�Դ����ƣ�ͨ�����ַ�ʽ����һ�ſ���

        //transform.Find("����ģ��/����").GetComponent<Text>().text = GameConfigManager.Instance.GetCardTypeById(data["Type"])["Name"];


        //����bg����img��������
        //transform.Find("bg").GetComponent<Image>().material = Instantiate(Resources.Load<Material>("Mats/outline"));//ѡ�к�������һ����ɫ��Ч��

    }


    //����ʹ�ÿ���
    public virtual bool TryUse()
    {
        //������Ҫ�ķ���
        //int cost = int.Parse(data["Extend"]);

/*        if (cost>FightManager.Instance.CurPowerCount)
        {
            //���ò���
            AudioManager.Instance.PlayEffect("Effect/loss");//ʹ��ʧ��

            //��ʾ
            UIManager.Instance.ShowTip("���ò���",Color.red);

            return false;
        }
        else
        {*/
            //FightManager.Instance.CurPowerCount -= cost;
            //ˢ�·�������
            //UIManager.Instance.GetUI<FightUI>("FightUI").UpdatePower();

            //ɾ��ʹ�ù��Ŀ���
            UIManager.Instance.GetUI<FightUI>("FightUI").RemoveCard(this);

            return true;
        //}
    }
    //��������ʹ�ú���Ч
    public void PlayEffect(Vector3 pos)
    {
        GameObject effectobj = Instantiate(Resources.Load(data["Effects"])) as GameObject;
        effectobj.transform.position = pos;
        Destroy(effectobj, 2);
    }

    Vector2 initPos;//��ק��ʼʱ��¼����λ��

    //��ʼ��ק
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        initPos = transform.GetComponent<RectTransform>().anchoredPosition;

        //��������
        AudioManager.Instance.PlayEffect("�ļ�·��");
    }

    //��ק��
    public virtual void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(),
            eventData.position,
            eventData.pressEventCamera,
            out pos))
        {
            transform.GetComponent<RectTransform>().anchoredPosition = pos;
        }
    }

    //������ק
    public virtual void OnEndDrag(PointerEventData eventData)
    {
        transform.GetComponent<RectTransform>().anchoredPosition = initPos;
        transform.SetSiblingIndex(index);
    }
}
