using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State = StateMachine<Enemy>.State;

public class Enemy : MonoBehaviour
{
    StateMachine<Enemy> _stateMachine;
    enum Event
    {
        //索敵
        Idle,
        //見失う
        Lost,
        //プレイヤーを見つけた
        FindPlayer,
        //攻撃
        Attack,
        //攻撃終了
        finish,
        //死亡
        Dead,

    }
    // Start is called before the first frame update
    void Start()
    {
        //ステートマシーンの初期化
        _stateMachine = new StateMachine<Enemy>(this);

        //ステートマシーンのトランジション登録

        //敵を見つけたら追跡するようにする
        _stateMachine.AddTransition<EnemyIdleState, Chase>((int)Event.FindPlayer);
        //敵が索敵から外れたとき
        _stateMachine.AddTransition<Chase, EnemyIdleState>((int)Event.Lost);
        //追跡から攻撃
        _stateMachine.AddTransition<Chase, Attack>((int)Event.Attack);
        //攻撃後に追跡
        _stateMachine.AddTransition<Attack, Chase>((int)Event.finish);

        //死んだとき
        _stateMachine.AddAnyTransition<Dead>((int)Event.Dead);

        //最初に実行するステート
        _stateMachine.Start<EnemyIdleState>();
    }

    // Update is called once per frame
    void Update()
    {
        //ステートマシーンの
        _stateMachine.Update();
    }

    //仮のステートを定義
    private class Chase:State { }

    private class Attack:State { }

    private class Dead : State { }
}
