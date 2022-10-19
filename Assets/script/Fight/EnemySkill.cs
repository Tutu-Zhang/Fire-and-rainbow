using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemySkill : MonoBehaviour
{
    public static EnemySkill Instance = new EnemySkill();
    //�����ж�����
    public Animator ani;
    
    private int boss1_HpLowerThan10TurnCount = 0;

    //������Ȯ��û���κ����⼼��
    public IEnumerator EnemyActio0(Enemy enemyInstance, ActionType typeIn)
    {
        //��ȡ�����ؼ�
        GameObject enemy = GameObject.Find("EnemyWaiting(Clone)");
        ani = enemy.GetComponent<Animator>();

        //�ȴ�һ��ʱ���ִ����Ϊ
        yield return new WaitForSeconds(0.5f);//�ȴ�0.5��

        switch (typeIn)
        {

            case ActionType.None:
                break;

            case ActionType.Defend:
                enemyInstance.Defend += 5;
                enemyInstance.UpdateDefend();
                break;

            case ActionType.Attack:
                Debug.Log("�����ж�");
                ani.SetBool("isAttacking", true);
                //�ȴ��������������꣬����ʱ��Ҳ��������
                yield return new WaitForSeconds(1);
                //���������(���ڲ�����
                Camera.main.DOShakePosition(0.1f, 0f, 5, 45);
                //��ҿ�Ѫ
                FightManager.Instance.GetPlayHit(enemyInstance.Attack);
                ani.SetBool("isAttacking", false);
                break;
        }

        //�ȴ����������꣬����ʱ��Ҳ��������
        yield return new WaitForSeconds(1);

        enemyInstance.HideAction();
    }

    
    //��С������Ѫ������10ʱ��ÿ�غ�����˺���2
    public IEnumerator EnemyActio1(Enemy enemyInstance, ActionType typeIn,int EnemyHP)
    {
        
        //��ȡ�����ؼ�
        GameObject enemy = GameObject.Find("EnemyWaiting(Clone)");
        ani = enemy.GetComponent<Animator>();
        int HpLowerThan10 = 0;
        

        
        if (EnemyHP < 10)
        {
            HpLowerThan10 = 1;
            boss1_HpLowerThan10TurnCount += 1;
        }
        else
        {
            HpLowerThan10 = 0;
        }

        //�ȴ�һ��ʱ���ִ����Ϊ
        yield return new WaitForSeconds(0.5f);//�ȴ�0.5��
        
        switch (typeIn)
        {

            case ActionType.None:
                break;
            
            case ActionType.Defend:
                enemyInstance.Defend += 5;
                enemyInstance.UpdateDefend();
                break;
            
            case ActionType.Attack:
                Debug.Log("�����ж�");
                ani.SetBool("isAttacking", true);
                //�ȴ��������������꣬����ʱ��Ҳ��������
                yield return new WaitForSeconds(1);
                //���������(���ڲ�����
                Camera.main.DOShakePosition(0.1f, 0f, 5, 45);
                //��ҿ�Ѫ
                FightManager.Instance.GetPlayHit(enemyInstance.Attack+ boss1_HpLowerThan10TurnCount*2*HpLowerThan10);
                ani.SetBool("isAttacking", false);
                break;
        }

        //�ȴ����������꣬����ʱ��Ҳ��������
        yield return new WaitForSeconds(1);

        enemyInstance.HideAction();

    }

    //ȭͷ�磺����Ѫ���ϸߣ�defend����ظ�Ѫ����attack��������˺����ظ�Ѫ��,������ÿ�غϼ�1
    public IEnumerator EnemyActio2(Enemy enemyInstance, ActionType typeIn)
    {
        //��ȡ�����ؼ�
        GameObject enemy = GameObject.Find("EnemyWaiting(Clone)");
        ani = enemy.GetComponent<Animator>();

        //�ȴ�һ��ʱ���ִ����Ϊ
        yield return new WaitForSeconds(0.5f);//�ȴ�0.5��
        
        switch (typeIn)
        {

            case ActionType.None:
                break;

            case ActionType.Defend:
                enemyInstance.CurHp += 10;
                enemyInstance.UpdateHp();
                break;

            case ActionType.Attack:
                Debug.Log("�����ж�");
                ani.SetBool("isAttacking", true);
                //�ȴ��������������꣬����ʱ��Ҳ��������
                yield return new WaitForSeconds(1);
                //���������(���ڲ�����
                Camera.main.DOShakePosition(0.1f, 0f, 5, 45);
                //��ҿ�Ѫ
                FightManager.Instance.GetPlayHit(enemyInstance.Attack + FightManager.Instance.turnCount);
                ani.SetBool("isAttacking", false);

                enemyInstance.CurHp += 5;
                enemyInstance.UpdateHp();
                break;
        }

        //�ȴ����������꣬����ʱ��Ҳ��������
        yield return new WaitForSeconds(1);

        enemyInstance.HideAction();
    }

    //���磺�����������ϸߣ����ܷ�Ϊ���֣�defend��Ӧ����˺����ָ����ף�attack��Ӧ��ɴ����˺��������Ѫ������10��նɱ
    public IEnumerator EnemyActio3(Enemy enemyInstance, ActionType typeIn)
    {
        //��ȡ�����ؼ�
        GameObject enemy = GameObject.Find("EnemyWaiting(Clone)");
        ani = enemy.GetComponent<Animator>();

        //�ȴ�һ��ʱ���ִ����Ϊ
        yield return new WaitForSeconds(0.5f);//�ȴ�0.5��

        switch (typeIn)
        {

            case ActionType.None:
                break;

            case ActionType.Defend:
                enemyInstance.Defend += 10;
                enemyInstance.UpdateDefend();
                
                ani.SetBool("isAttacking", true);
                //�ȴ��������������꣬����ʱ��Ҳ��������
                yield return new WaitForSeconds(1);
                //���������(���ڲ�����
                Camera.main.DOShakePosition(0.1f, 0f, 5, 45);
                //��ҿ�Ѫ
                FightManager.Instance.GetPlayHit(enemyInstance.Attack);
                ani.SetBool("isAttacking", false);
                break;

            case ActionType.Attack:
                ani.SetBool("isAttacking", true);
                //�ȴ��������������꣬����ʱ��Ҳ��������
                yield return new WaitForSeconds(1);
                //���������(���ڲ�����
                Camera.main.DOShakePosition(0.1f, 0f, 5, 45);
                
                //��ҿ�Ѫ
                if (FightManager.Instance.CurHP >= 10)
                {
                    FightManager.Instance.GetPlayHit(enemyInstance.Attack + 5);
                }
                else
                {
                    FightManager.Instance.GetPlayHit(FightManager.Instance.CurHP);
                }
                                            
                ani.SetBool("isAttacking", false);
                break;
        }

        //�ȴ����������꣬����ʱ��Ҳ��������
        yield return new WaitForSeconds(1);

        enemyInstance.HideAction();
    }

/*    public IEnumerator EnemyActio4()
    {

    }
*/
}
