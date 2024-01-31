using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObjectController : MonoBehaviour
{
    [SerializeField] int _pikminCount = 1;
    [SerializeField] float _objBasicSpeed = 1.0f;
    [SerializeField, Header("�I�u�W�F�N�g�̃T�C�Y")] float _objSize;
    [SerializeField, Header("�ړI�n")] GameObject _destination;
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
    }
    void Update()
    {
        //�ړI�n�ɂ��Ă����珈����ς���
        if(Vector3.Distance(transform.position, _destination.transform.position) <= 0.25f)
        {
            Finish();
            return;
        }
        //���g�̎q�I�u�W�F�N�g�𐔂���
        int childCount = this.gameObject.transform.childCount;
        if(childCount >= _pikminCount)
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
            if(childCount >= _pikminCount)
            {
                _agent.speed = _objSpeed * 1.5f;
            }
            else
            {
                _agent.speed = _objSpeed;
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Pikmin")
        {
            //�s�N�~�����q�I�u�W�F�N�g�ɂȂ�
        }
    }

    private void Finish()
    {
        Debug.Log("�������܂���");
    }
}
