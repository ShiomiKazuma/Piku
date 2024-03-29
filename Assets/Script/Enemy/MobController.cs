using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using DG.Tweening;

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

    public int _tactic = 1; //状況判別
    public MobState _mobState;
    Transform _targetTransform;
    NavMeshAgent _navAgent;
    Vector3 _destination;
    [SerializeField] Animator _animator;
    [SerializeField] float _freezeTime = 3f;
    float _freezeTimer = 0;
    [SerializeField] float _maxHp;
    [SerializeField] Image _image;
    float _currentHp;
    [SerializeField] GameObject _damageObj;
    bool IsDeath = true;
    private void Start()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        SetState(MobState.Idle);
        _currentHp = _maxHp;
    }

    private void Update()
    {
        if(_currentHp <= 0 && IsDeath)
        {
            Death();
            IsDeath = false;
            return;
        }
        Gaze();
        if (_mobState == MobState.Chase)
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
        //Debug.Log(_freezeTimer);
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
        _mobState = MobState.Freeze;
        _animator.SetBool("IsAttack", false);
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

    public void Damage(int damage)
    {
        _currentHp -= damage;
        _image.DOFillAmount(_currentHp / _maxHp, 1.0f);
    }

    void Gaze()
    {
        // Fill Amountによってゲージの色を変える
        if (_image.fillAmount > 0.5f)
        {

            _image.color = Color.green;
 
        }
        else if (_image.fillAmount > 0.2f)
        {

            _image.color = Color.yellow;
 
        }
        else 
        {
            _image.color = Color.red;
        }
    }

    public void Death()
    {
        var dag = _damageObj.GetComponent<Damage>();
        _animator.SetTrigger("Death");
        dag.Death();
    }
}
