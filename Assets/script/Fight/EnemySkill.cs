using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemySkill : MonoBehaviour
{
    public static EnemySkill Instance = new EnemySkill();
    //敌人行动类型
    public Animator ani;
    
    private int boss1_HpLowerThan10TurnCount = 0;

    //赛博猎犬：没有任何特殊技能
    public IEnumerator EnemyActio0(Enemy enemyInstance, ActionType typeIn)
    {
        //获取动画控件
        GameObject enemy = GameObject.Find("EnemyWaiting(Clone)");
        ani = enemy.GetComponent<Animator>();

        //等待一段时间后执行行为
        yield return new WaitForSeconds(0.5f);//等待0.5秒

        switch (typeIn)
        {

            case ActionType.None:
                break;

            case ActionType.Defend:
                enemyInstance.Defend += 5;
                enemyInstance.UpdateDefend();
                break;

            case ActionType.Attack:
                Debug.Log("敌人行动");
                ani.SetBool("isAttacking", true);
                //等待攻击动画播放完，这里时间也可以配置
                yield return new WaitForSeconds(1);
                //摄像机抖动
                Camera.main.DOShakePosition(0.1f, 1f, 5, 45);
                //玩家扣血
                FightManager.Instance.GetPlayHit(enemyInstance.Attack);
                ani.SetBool("isAttacking", false);
                break;
        }

        //等待动画播放完，这里时间也可以配置
        yield return new WaitForSeconds(1);

        enemyInstance.HideAction();
    }

    
    //打交小兵：在血量低于10时，每回合造成伤害加2
    public IEnumerator EnemyActio1(Enemy enemyInstance, ActionType typeIn,int EnemyHP)
    {
        
        //获取动画控件
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

        //等待一段时间后执行行为
        yield return new WaitForSeconds(0.5f);//等待0.5秒
        
        switch (typeIn)
        {

            case ActionType.None:
                break;
            
            case ActionType.Defend:
                enemyInstance.Defend += 5;
                enemyInstance.UpdateDefend();
                break;
            
            case ActionType.Attack:
                Debug.Log("敌人行动");
                ani.SetBool("isAttacking", true);
                //等待攻击动画播放完，这里时间也可以配置
                yield return new WaitForSeconds(1);
                //摄像机抖动(现在不抖动
                Camera.main.DOShakePosition(0.1f, 1f, 5, 45);
                //玩家扣血
                FightManager.Instance.GetPlayHit(enemyInstance.Attack+ boss1_HpLowerThan10TurnCount*2*HpLowerThan10);
                ani.SetBool("isAttacking", false);
                break;
        }

        //等待动画播放完，这里时间也可以配置
        yield return new WaitForSeconds(1);

        enemyInstance.HideAction();

    }

    //拳头哥：基础血量较高，defend代表回复血量，attack代表造成伤害并回复血量,攻击力每回合加1
    public IEnumerator EnemyActio2(Enemy enemyInstance, ActionType typeIn)
    {
        //获取动画控件
        GameObject enemy = GameObject.Find("EnemyWaiting(Clone)");
        ani = enemy.GetComponent<Animator>();

        //等待一段时间后执行行为
        yield return new WaitForSeconds(0.5f);//等待0.5秒
        
        switch (typeIn)
        {

            case ActionType.None:
                break;

            case ActionType.Defend:
                enemyInstance.CurHp += 10;
                enemyInstance.UpdateHp();
                break;

            case ActionType.Attack:
                Debug.Log("敌人行动");
                ani.SetBool("isAttacking", true);
                //等待攻击动画播放完，这里时间也可以配置
                yield return new WaitForSeconds(1);
                //摄像机抖动(现在不抖动
                Camera.main.DOShakePosition(0.1f, 1f, 5, 45);
                //玩家扣血
                FightManager.Instance.GetPlayHit(enemyInstance.Attack + FightManager.Instance.TurnCount);
                ani.SetBool("isAttacking", false);

                enemyInstance.CurHp += 5;
                enemyInstance.UpdateHp();
                break;
        }

        //等待动画播放完，这里时间也可以配置
        yield return new WaitForSeconds(1);

        enemyInstance.HideAction();
    }

    //刀哥：基础攻击面板较高，技能分为两种，defend对应造成伤害并恢复护甲，attack对应造成大量伤害，若玩家血量低于10则斩杀
    public IEnumerator EnemyActio3(Enemy enemyInstance, ActionType typeIn)
    {
        //获取动画控件
        GameObject enemy = GameObject.Find("EnemyWaiting(Clone)");
        ani = enemy.GetComponent<Animator>();

        //等待一段时间后执行行为
        yield return new WaitForSeconds(0.5f);//等待0.5秒

        switch (typeIn)
        {

            case ActionType.None:
                break;

            case ActionType.Defend:
                enemyInstance.Defend += 5;
                enemyInstance.UpdateDefend();
                
                ani.SetBool("isAttacking", true);
                //等待攻击动画播放完，这里时间也可以配置
                yield return new WaitForSeconds(1);
                //摄像机抖动(现在不抖动
                Camera.main.DOShakePosition(0.1f, 1f, 5, 45);
                //玩家扣血
                FightManager.Instance.GetPlayHit(enemyInstance.Attack);
                ani.SetBool("isAttacking", false);
                break;

            case ActionType.Attack:
                ani.SetBool("isAttacking", true);
                //等待攻击动画播放完，这里时间也可以配置
                yield return new WaitForSeconds(1);
                //摄像机抖动(现在不抖动
                Camera.main.DOShakePosition(0.1f, 1f, 5, 45);
                
                //玩家扣血
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

        //等待动画播放完，这里时间也可以配置
        yield return new WaitForSeconds(1);

        enemyInstance.HideAction();
    }

    //天使：若天使护甲值不为0，造成固定伤害；若护甲值为0，且玩家血量>1，则将玩家打到1点血(50%概率发动)或是造成固定伤害（50%概率发动）；若玩家血量=1，则造成固定伤害（击杀玩家
    public IEnumerator EnemyActio4(Enemy enemyInstance, ActionType typeIn)
    {
        //获取敌人动画控件
        GameObject enemy = GameObject.Find("EnemyWaiting(Clone)");
        ani = enemy.GetComponent<Animator>();

        //等待一段时间后执行行为
        yield return new WaitForSeconds(0.5f);//等待0.5秒

        if (enemyInstance.ifLv4BossConsumeLives)
        {
            enemyInstance.Defend -= 100;
            enemyInstance.CurHp += enemyInstance.MaxHp;
            enemyInstance.UpdateDefend();
            enemyInstance.UpdateHp();

            enemyInstance.ifLv4BossConsumeLives = false;
            typeIn = ActionType.Attack;
        }

        switch (typeIn)
        {
            case ActionType.Defend:
                enemyInstance.Defend += 5;
                enemyInstance.CurHp += 5;
                enemyInstance.UpdateDefend();
                enemyInstance.UpdateHp();
                break;

            case ActionType.Attack:
                ani.SetBool("isAttacking", true);
                //等待攻击动画播放完，这里时间也可以配置
                yield return new WaitForSeconds(1);
                //摄像机抖动(现在不抖动
                Camera.main.DOShakePosition(0.1f, 1f, 5, 45);
                

                if (enemyInstance.Defend >= 0)
                {
                    //玩家扣血
                    FightManager.Instance.GetPlayHit(enemyInstance.Attack);
                }
                else
                {
                    if (FightManager.Instance.CurHP > 1)
                    {
                        int ran = Random.Range(0, 1);

                        if (ran > 0.5)
                        {
                            FightManager.Instance.GetPlayHit(FightManager.Instance.CurHP - 1);
                        }
                        else
                        {
                            FightManager.Instance.GetPlayHit(enemyInstance.Attack);
                        }
                    }
                    else
                    {
                        FightManager.Instance.GetPlayHit(enemyInstance.Attack);
                    }
                }

                ani.SetBool("isAttacking", false);
                break;
        }
    }

}
