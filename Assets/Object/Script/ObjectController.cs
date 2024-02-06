using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObjectController : MonoBehaviour
{
    [SerializeField] int _pikminCount = 4;
    [SerializeField, Header("�^�ׂ�ꏊ")]
    GameObject[] _objects;
    [SerializeField] float _objBasicSpeed = 1.0f;
    [SerializeField, Header("�I�u�W�F�N�g�̃T�C�Y")] float _objSize;
    [SerializeField, Header("�ړI�n")] GameObject _destination;
    [SerializeField] int _currentPikmin = 0;
    [SerializeField] ParticleSystem _particleSystem;
    [SerializeField] int _score = 1000;
    NavMeshAgent _agent;
    ObjState _state;
    float _objSpeed;
    bool IsFinish = true;
    [SerializeField] bool IsScore = true;
    [SerializeField] GameObject _pikmin;
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
        //�ړI�n�ɂ��Ă����珈����ς���
        if(Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(_destination.transform.position.x, _destination.transform.position.z)) <= 0.2f && IsFinish)
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
            _agent.speed = _objBasicSpeed;
        }
    }

    /// <summary>
    /// �s�N�~�����˒��ɂ����Ƃ�
    /// </summary>
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Pikmin")
        {
            var pik = other.gameObject.GetComponent<Pikmin>();
            if(pik._state == Pikmin.PikminState.Idle)
            {
                if(_currentPikmin < _pikminCount)
                {
                    pik.SetCarry(_objects[_currentPikmin], this.gameObject);
                }
            }
        }
    }
    private void Finish()
    {
        _agent.isStopped = true;
        IsFinish = false;
        Component[] components = GetComponentsInChildren<Pikmin>();
        foreach(var component in components)
        {
            component.gameObject.transform.parent = null;
            var pik = component.gameObject.GetComponent<Pikmin>();
            pik._state = Pikmin.PikminState.Idle;
        }
        if(IsScore)
        {
            GameManager._instance.AddScore(_score);
        }
        else
        {
            Born();
        }
        SoundManager._instance.PlaySE(SESoundData.SE.CarryFinish);
        
        Instantiate(_particleSystem, new Vector3(this.transform.position.x, this.transform.position.y + 2.5f, this.transform.position.z), Quaternion.identity);
        Destroy(gameObject, 1f);
    }

    public void AddPikmin()
    {
        _currentPikmin++;
    }

    public void DisPikmin()
    {
        _currentPikmin--;
    }

    void Born()
    {
        Instantiate(_pikmin, new Vector3(this.transform.position.x + 5f, this.transform.position.y + 3f, this.transform.position.z), Quaternion.identity);
    }
}
