using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//卡牌战斗初始化
public class FightInit : FightUnit
{
    //进入该页面时调用
    public void Start()
    {

        //初始化配置表
        GameConfigManager.Instance.Init();

        //初始化战斗数值
        FightManager.Instance.Init();


        //播放战斗bgm，这里只需要输入bgm的名字就可以
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayBGM("fightBGM");


        //初始化战斗卡牌数据
        FightCardManager.Instance.Init();

        //加载战斗元素
        UIManager.Instance.ShowUI<FightUI>("fightBackground");

        //加载关卡资源
        switch (LevelManager.Instance.level)
        {
            case 0:
                EnemyManager.Instance.loadRes("10000");
                break;
            case 1:
                EnemyManager.Instance.loadRes("10001");
                break;
            case 2:
                EnemyManager.Instance.loadRes("10002");
                break;
            case 3:
                EnemyManager.Instance.loadRes("10003");
                break;
            case 4:
                EnemyManager.Instance.loadRes("10004");
                break;
        }

        //切换到玩家回合
        FightManager.Instance.ChangeType(FightType.Player);
    }

    public override void OnUpdate()
    {

    }
}
