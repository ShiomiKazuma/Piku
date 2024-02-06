using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobEyeSensor : MonoBehaviour
{
    [SerializeField] SphereCollider _serchArea = default;
    [SerializeField] float _serchAngle = 45f;
    [SerializeField] GameObject _control;
    [SerializeField] bool _editor;
    MobController mob = default;
    bool Fight = false;
    bool Judge = false;

    private void Start()
    {
       // mob = transform.parent.GetComponent<MobController>();
       mob = GetComponent<MobController>();
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            var playerDirection = other.transform.position - transform.position;
            var angle = Vector3.Angle(transform.forward, playerDirection);
            //ãóó£ÇÃîªíË
            var dis = Vector3.Distance(other.gameObject.transform.position, transform.position);
            if(angle <= _serchAngle)
            {
                _control.transform.position = Vector3.Lerp(_control.transform.position, other.gameObject.transform.position, 0.1f);
                if(dis <= 2f && dis >= _serchArea.radius * 0f)
                {
                    mob.SetState(MobController.MobState.Slashattack);
                    if(!Fight)
                    {
                        SoundManager._instance.PlaySE(SESoundData.SE.Fight);
                    }
                    
                    Fight = true;
                    Judge = true;
                }
                else if(dis <= _serchArea.radius * 0.8f && dis >= 2f)
                {
                    mob.SetState(MobController.MobState.Chase, other.gameObject.transform);
                    if (!Judge && !Fight)
                    {
                        Judge = true;
                        SoundManager._instance.PlaySE(SESoundData.SE.Judge);
                    }
                    
                }
                else if(dis <= _serchArea.radius * 1f && dis >= _serchArea.radius * 0.8f)
                {
                    //èÛë‘ëJà⁄
                    mob.SetState(MobController.MobState.Idle);
                }
               
            }
            else
            {
                _control.transform.position = Vector3.Lerp(_control.transform.position, transform.position, 0.1f);
                mob.SetState(MobController.MobState.Idle);
            }
        }
        else if(other.gameObject.tag == "Pikmin")
        {
            var playerDirection = other.transform.position - transform.position;
            var angle = Vector3.Angle(transform.forward, playerDirection);
            //ãóó£ÇÃîªíË
            var dis = Vector3.Distance(other.gameObject.transform.position, transform.position);
            if (angle <= _serchAngle)
            {
                _control.transform.position = Vector3.Lerp(_control.transform.position, other.gameObject.transform.position, 0.1f);
                if (dis <= 2f && dis >= _serchArea.radius * 0f)
                {
                    mob.SetState(MobController.MobState.Slashattack);
                }
                else if (dis <= _serchArea.radius * 0.8f && dis >= 2f)
                {
                    mob.SetState(MobController.MobState.Chase, other.gameObject.transform);
                }
                else if (dis <= _serchArea.radius * 1f && dis >= _serchArea.radius * 0.8f)
                {
                    //èÛë‘ëJà⁄
                    mob.SetState(MobController.MobState.Idle);
                }

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
        Fight = false;
        Judge = false;
        mob.SetState(MobController.MobState.Idle);
    }

    //private void OnDrawGizmos()
    //{
    //    if(_editor)
    //    {
    //        Handles.color = Color.red;
    //        Handles.DrawSolidArc(transform.position, Vector3.up, Quaternion.Euler(0f, -_serchAngle, 0f) * transform.forward, _serchAngle * 2f, _serchArea.radius);
    //    }
    //}

}
