using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobController : MonoBehaviour
{
    public enum MobState
    {
        Idle,
        Walk,
        Chase,
        Leave,
        Alert,
        Freeze,
        Slashattack,
        Laserbeam,
        Laserbullet
    }

    public MobState _mobState;
    Transform _targetTransform;
    NavMeshAgent _navAgent;
    Vector3 _destination;

    private void Start()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        SetState(MobState.Idle);
    }

    private void Update()
    {
        if(_mobState == MobState.Chase)
        {
            if(_targetTransform == null)
            {
                SetState(MobState.Idle);
            }
            else
            {
                SetDestination(_targetTransform.position);
                _navAgent.SetDestination(GetDestination());
            }
            var dir = (GetDestination() - transform.position).normalized;
            dir.y = 0;
            Quaternion setRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, setRotation, _navAgent.angularSpeed * 0.1f * Time.deltaTime);
        }
    }
    public void SetState(MobState tempState, Transform targetObject = null)
    {
        _mobState = tempState;
        if(tempState == MobState.Idle)
        {
            _navAgent.isStopped = true;
        }
        else if(tempState == MobState.Chase)
        {
            _targetTransform = targetObject;
            _navAgent.isStopped = false;
        }
    }

    public MobState GetState()
    {
        return _mobState;
    }

    public void SetDestination(Vector3 transform)
    {
        _destination = transform;
    }

    public Vector3 GetDestination()
    {
        return _destination;
    }
}
