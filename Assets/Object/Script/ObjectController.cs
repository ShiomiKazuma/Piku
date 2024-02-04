using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObjectController : MonoBehaviour
{
    [SerializeField] int _pikminCount = 1;
    [SerializeField] float _objBasicSpeed = 1.0f;
    [SerializeField, Header("オブジェクトのサイズ")] float _objSize;
    [SerializeField, Header("目的地")] GameObject _destination;
    int _currentPikmin = 0;
    NavMeshAgent _agent;
    ObjState _state;
    float _objSpeed;
    public enum ObjState
    {
        Idle,
        Carry,
    }

    private void Awake()
    {
        _state = ObjState.Idle;
        _agent = GetComponent<NavMeshAgent>();
        _agent.destination = _destination.transform.position;
    }
    void Update()
    {
        //目的地についていたら処理を変える
        if(Vector3.Distance(transform.position, _destination.transform.position) <= 0.25f)
        {
            Finish();
            return;
        }
        if(_currentPikmin >= _pikminCount)
        {
            _state = ObjState.Carry;
        }
        else
        {
            _state = ObjState.Idle;
        }

        if(_state == ObjState.Idle)
        {
            _agent.isStopped = true;
        }
        else if(_state == ObjState.Carry)
        {
            _agent.isStopped = false;
            if(_currentPikmin >= _pikminCount)
            {
                _agent.speed = _objSpeed * 1.5f;
            }
            else
            {
                _agent.speed = _objSpeed;
            }
        }
    }


    private void Finish()
    {
        Debug.Log("到着しました");
    }

    public void AddPikmin()
    {
        _currentPikmin++;
    }

    public void DisPikmin()
    {
        _currentPikmin--;
    }
}
