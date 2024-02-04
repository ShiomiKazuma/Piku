using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class Pikmin : MonoBehaviour
{
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] bool IsFollow;
    //public PikminPlayer _controller;
    //public Transform _playerGathPos;
    [Header("追跡するもの")] public Transform _targetTransform;
    public Transform _homPos;
    public GameObject _playerObject;
    Vector3 _destination;
    PikminState _state;
    Rigidbody _rb;
    public enum PikminState
    {
        Idle,
        Follow,
        Carry,
        Jump
    }

    private void Start()
    {
        _state = PikminState.Idle;
    }

    private void Update()
    {
        if(_state == PikminState.Follow)
        {
            //_destination = _playerGathPos.position;
            //_agent.SetDestination(_playerGathPos.position);
            _agent.isStopped = false;
            SetDestination(_targetTransform.position);
            _agent.SetDestination(GetDestination());
            var dir = (GetDestination() - transform.position).normalized;
            dir.y = 0;
            Quaternion setRotation = Quaternion.LookRotation(dir);
            if(dir == Vector3.zero)
            {
                transform.rotation = Quaternion.identity;
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, setRotation, 120.0f * 0.1f * Time.deltaTime);
            }
        }
        else if(_state == PikminState.Idle)
        {
            _agent.isStopped = true;
        }
        
    }

    ///<summary>目的地を設定するメソッド</summary>
    public void SetDestination(Vector3 position)
    {
        _destination = position;
    }

    ///<summary>目的地を取得するメソッド</summary>
    public Vector3 GetDestination()
    {
        return _destination;
    }

    ///<summary>目的地を設定するメソッド</summary>
    public void SetPikminState(PikminState state)
    {
        _state = state;
    }

    ///<summary>状態を取得するメソッド</summary>
    public PikminState GetPikminState()
    {
        return _state;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(_state == PikminState.Jump)
        {
            //色を変える
            _state = PikminState.Idle;
            if(collision.gameObject.tag == "Enemy")
            {
                //エネミーに攻撃する

                //ノックバックさせる
                Vector3 normal = collision.contacts[0].normal;
            }
        }
    }
}
