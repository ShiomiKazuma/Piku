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
        //�X�e�[�g�}�V�[���̏�����
        _stateMachine = new StateMachine<Enemy>(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //���̃X�e�[�g���`
    private class Idle{}

    private class Chase { }

    private class Attack { }
}
