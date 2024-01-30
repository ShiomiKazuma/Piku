using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyEyeSensor : MonoBehaviour
{
    [SerializeField] SphereCollider _serchArea = default;
    [SerializeField] float _serchAngle = 45f;
    [SerializeField] GameObject _control;
    [SerializeField] bool _editor;
    MobController mob = default;

    private void Start()
    {
        mob = transform.parent.GetComponent<MobController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            var playerDirection = other.transform.position - transform.position;
            var angle = Vector3.Angle(transform.forward, playerDirection);
            //�����̔���
            var dis = Vector3.Distance(other.gameObject.transform.position, transform.position);
            if(angle <= _serchAngle)
            {
                _control.transform.position = Vector3.Lerp(_control.transform.position, other.gameObject.transform.position, 0.1f);
                if(dis <= _serchAngle * 0.15f && dis >= _serchAngle * 0f)
                {

                }
                else if(dis <= _serchAngle * 0.8f && dis >= _serchAngle * 0.15f)
                {

                }
                else if(dis <= _serchAngle * 1f && dis >= _serchAngle * 0.8f)
                {

                }
                //��ԑJ��
                mob.SetState(MobController.MobState.Chase, other.gameObject.transform);
            }
            else
            {
                _control.transform.position = Vector3.Lerp(_control.transform.position, transform.position, 0.1f);
                mob.SetState(MobController.MobState.Idle);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _control.transform.position = transform.position;
        mob.SetState(MobController.MobState.Idle);
    }

    private void OnDrawGizmos()
    {
        if(_editor)
        {
            Handles.color = Color.red;
            Handles.DrawSolidArc(transform.position, Vector3.up, Quaternion.Euler(0f, -_serchAngle, 0f) * transform.forward, _serchAngle * 2f, _serchArea.radius * 0.5f);
        }
    }

}