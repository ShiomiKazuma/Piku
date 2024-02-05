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
    public GameObject _target;
    GameObject _parentObj;
    public GameObject _playerObject;
    Vector3 _destination;
    public PikminState _state = PikminState.Idle;
    Rigidbody _rb;
    float _stopDis;
    public bool IsFirstCarry = false;
    public bool IsChild = false;
    public enum PikminState
    {
        Idle,
        Follow,
        Carry,
        Jump
    }

    private void Start()
    {
        _stopDis = _agent.stoppingDistance;
    }
    private void Update()
    {
        if (_state == PikminState.Jump)
            return;
        if(_state == PikminState.Follow)
        {
            //_destination = _playerGathPos.position;
            //_agent.SetDestination(_playerGathPos.position);
            _agent.enabled = true;
            _agent.stoppingDistance = _stopDis;
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
            _agent.enabled = true;
            _agent.isStopped = true;
        }
        else if(_state == PikminState.Carry)
        {
            if(_agent.stoppingDistance > Vector3.Distance(_target.transform.position, this.transform.position) && !IsFirstCarry)
            {
                _agent.isStopped = false;
                _agent.enabled = false;
                this.gameObject.transform.parent = _parentObj.transform;
                var parent = _parentObj.GetComponent<ObjectController>();
                IsFirstCarry = true;
                IsChild = true;
                parent.AddPikmin();
            }
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

    ///<summary>状態を取得するメソッド</summary>
    public void PikminJump()
    {
        _state = PikminState.Jump;
    }

    public void SetCarry(GameObject target, GameObject parent)
    {
        _state = PikminState.Carry;
        _agent.enabled = true;
        _agent.isStopped = false;
        SetDestination(target.transform.position);
        _agent.SetDestination(GetDestination());
        _target = target;
        _parentObj = parent;
        _agent.stoppingDistance = 1f;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(_state == PikminState.Jump)
        {
            _agent.enabled = true;
            _state = PikminState.Idle;
            if(collision.gameObject.tag == "Enemy")
            {
                
            }
        }
    }

}
