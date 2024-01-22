using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pikmin : MonoBehaviour
{
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] bool IsFollow;
    public PikminPlayer _controller;
    public Transform _playerGathPos;
    [Header("追跡するもの")] public Transform _targetTransform;
    public Transform _homPos;
    public ObjController _targetObject;
    public bool _gohome;
    public bool IsIdle;
    Vector3 _destination;

    public enum PikminState
    {
        Idle,
        Follow,

    }
    private void Update()
    {
        if(IsFollow)
        {
            //_destination = _playerGathPos.position;
            //_agent.SetDestination(_playerGathPos.position);
            SetDestination(_targetTransform.position);
            _agent.SetDestination(GetDestination());
            var dir = (GetDestination() - transform.position).normalized;
            dir.y = 0;
            Quaternion setRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, setRotation, 120.0f * 0.1f * Time.deltaTime);
        }

        if(_gohome)
        {
            _agent.SetDestination(_homPos.transform.position);
            if(Vector3.Distance(transform.position, _homPos.transform.position) <= 0.85f)
            {
                IsFollow = false;
                _gohome = false;
                IsIdle = false;
            }
        }

        if(_targetObject != null)
        {
            _agent.SetDestination(_targetObject.transform.position);
            if (Vector3.Distance(transform.position, _targetObject.transform.position) <= 0.75f)
            {
                _targetObject.transform.position = transform.position + Vector3.forward * 0.9f;
                _targetObject.transform.SetParent(transform);
                Destroy(_targetObject.GetComponent<GameObject>());
                _gohome = true;
            }
        }

        if(IsIdle)
        {
            _agent.Stop();
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
}
