using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State = StateMachine<Enemy>.State;

public class Enemy : MonoBehaviour
{
    StateMachine<Enemy> _stateMachine;
    enum Event
    {
        //���G
        Idle,
        //������
        Lost,
        //�v���C���[��������
        FindPlayer,
        //�U��
        Attack,
        //�U���I��
        finish,
        //���S
        Dead,

    }
    // Start is called before the first frame update
    void Start()
    {
        //�X�e�[�g�}�V�[���̏�����
        _stateMachine = new StateMachine<Enemy>(this);

        //�X�e�[�g�}�V�[���̃g�����W�V�����o�^

        //�G����������ǐՂ���悤�ɂ���
        _stateMachine.AddTransition<EnemyIdleState, Chase>((int)Event.FindPlayer);
        //�G�����G����O�ꂽ�Ƃ�
        _stateMachine.AddTransition<Chase, EnemyIdleState>((int)Event.Lost);
        //�ǐՂ���U��
        _stateMachine.AddTransition<Chase, Attack>((int)Event.Attack);
        //�U����ɒǐ�
        _stateMachine.AddTransition<Attack, Chase>((int)Event.finish);

        //���񂾂Ƃ�
        _stateMachine.AddAnyTransition<Dead>((int)Event.Dead);

        //�ŏ��Ɏ��s����X�e�[�g
        _stateMachine.Start<EnemyIdleState>();
    }

    // Update is called once per frame
    void Update()
    {
        //�X�e�[�g�}�V�[����
        _stateMachine.Update();
    }

    //���̃X�e�[�g���`
    private class Chase:State { }

    private class Attack:State { }

    private class Dead : State { }
}
