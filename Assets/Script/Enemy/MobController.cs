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

    public int _tactic = 1; //èÛãµîªï 
    public MobState _mobState;
    Transform _targetTransform;
    NavMeshAgent _navAgent;
    Vector3 _destination;
    [SerializeField] Animator _animator;
    [SerializeField] float _freezeTime = 3f;
    float _freezeTimer = 0;
     
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
            _animator.SetBool("IsWalk", true);
        }
        else if(_mobState == MobState.Slashattack)
        {
            _animator.SetBool("IsWalk", false);
            _animator.SetBool("IsAttack", true);
            Invoke(nameof(AttackStop), 1.0f);
        }
        else if (_mobState == MobState.Freeze)
        {
            _freezeTimer += Time.deltaTime;
            if(_freezeTimer > _freezeTime)
            {
                _freezeTimer = 0;
                _mobState = MobState.Idle;
            }
        }
        Debug.Log(_freezeTimer);
    }
    public void SetState(MobState tempState, Transform targetObject = null)
    {
        if (_mobState == MobState.Freeze)
            return;
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

    public bool _attack = false;
    public int _atCount = 1;
    public void AttackStop()
    {
        _animator.SetBool("IsAttack", false);
        _mobState = MobState.Freeze;
        //_attack = false;
        //if (GetState() == MobState.Freeze) 
        //    SetState(MobState.Chase);
        //else
        //{
        //    float percent = 0.0f;

        //    switch(_tactic)//òAë±çUåÇÇÃóêêî
        //    {
        //        case 1:
        //            percent = 100.0f;
        //            break;
        //        case 2:
        //            if (_atCount == 1)
        //                percent = 50.0f;
        //            else if (_atCount == 2)
        //                percent = 100.0f;
        //            break;
        //        case 3:
        //            if (_atCount == 1)
        //                percent = 30.0f;
        //            else if (_atCount == 2)
        //                percent = 70.0f;
        //            else if (_atCount == 3)
        //                percent = 100.0f;
        //            break;
        //    }

        //    //òAë±çUåÇÇÃï™äÚ
        //    if(Probability(percent))
        //    {
        //        _animator.SetBool("IsAttack", false);
        //        SetState(MobState.Freeze);
        //        _atCount = 1;
        //    }
        //    else
        //    {
        //        SetState(MobState.Chase);
        //        _atCount++;
        //    }
        //}
    }

    public bool Probability(float percent)
    {
        float ProbabilityRate = Random.value * 100f;
        if (percent == 100.0f && ProbabilityRate == percent)
            return true;
        else if (ProbabilityRate < percent)
            return true;
        else
            return false;
    }

}
