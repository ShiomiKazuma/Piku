using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State = StateMachine<Enemy>.State;

public class Enemy : MonoBehaviour
{
    StateMachine<Enemy> _stateMachine;

    enum Event
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        //ステートマシーンの初期化
        _stateMachine = new StateMachine<Enemy>(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //仮のステートを定義
    private class Idle{}

    private class Chase { }

    private class Attack { }
}
