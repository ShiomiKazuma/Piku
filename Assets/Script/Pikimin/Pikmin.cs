using System.Collections;
using System.Collections.Generic;
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
    public enum PikminState
    {
        Idle,
        Follow,

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

        //if(_gohome)
        //{
        //    _agent.SetDestination(_homPos.transform.position);
        //    if(Vector3.Distance(transform.position, _homPos.transform.position) <= 0.85f)
        //    {
        //        IsFollow = false;
        //        _gohome = false;
        //        IsIdle = false;
        //    }
        //}

        //if(_targetObject != null)
        //{
        //    _agent.SetDestination(_targetObject.transform.position);
        //    if (Vector3.Distance(transform.position, _targetObject.transform.position) <= 0.75f)
        //    {
        //        _targetObject.transform.position = transform.position + Vector3.forward * 0.9f;
        //        _targetObject.transform.SetParent(transform);
        //        Destroy(_targetObject.GetComponent<GameObject>());
        //        _gohome = true;
        //    }
        //}

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
    //ピクミンが呼ばれた時
    //public void CallPikmin()
    //{
    //    //隊列に入れる
    //    this.transform.SetParent(_formationParent.transform);
    //    _pikminList.Add(this.gameObject);
    //    if (_pikminList.IndexOf(this.gameObject) == 0)
    //    {
    //        _targetTransform = _playerObject.transform;
    //    }
    //    else
    //    {
    //        _targetTransform = _pikminList[_pikminList.IndexOf(this.gameObject) - 1].transform;
    //    }
    //}
}
