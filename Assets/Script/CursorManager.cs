using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] GameObject _player;
    [SerializeField] float _minDis = 0.5f;
    [SerializeField] float _maxDis = 15f;
    [SerializeField] float _userSensSpeed = 40f;
    [SerializeField] float _duration = 0.1f;
    float _xPos;
    float _zPos;    
    // Update is called once per frame
    void Update()
    {
        //�J��������}�E�X������ꏊ�Ɍ�������Ray�𔭎�
        RaycastHit hit;
        //layer8��9��"Player"��"Attack"�ɂ͓�����Ȃ����߂̃}�X�N
        int layerMask = ~(1 << 8 | 1 << 9);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            //���������ӏ��̍��W
            //transform.position = hit.point;
            //�v���C���[�̍����ɍ��킹��
            Vector3 vec = new Vector3(hit.point.x, (_player.transform.position.y) - 0.5f, hit.point.z);
            float dis = Vector3.Distance(vec, _player.transform.position);
            if (dis <= _maxDis && dis >= _minDis)
            {
                this.transform.position = Vector3.Lerp(this.transform.position, vec, _duration);
            }
        }
    }

    //private void Move()
    //{
    //    _xPos += Input.GetAxis("Mouse X") * _userSensSpeed;
    //    _zPos += Input.GetAxis("Mouse Y") * _userSensSpeed;

    //    Vector3 movePos = this.transform.position + new Vector3(_xPos, 0, _zPos);
    //    movePos.y = _player.transform.position.y;
    //    float dis = Vector3.Distance(_player.transform.position, movePos);
    //    if (dis <= _maxDis && dis >= _minDis)
    //    {
    //        this.transform.position = Vector3.Lerp(this.transform.position, movePos, _duration);
    //    }
    //}
}
