using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureMeter : MonoBehaviour
{
    [SerializeField] const float _maxMeterAngle = -180;
    [SerializeField] const float _zeroMeterAngle = 0;
    [SerializeField] GameObject _needle;
    Transform _needleTransform;
    [SerializeField] GameObject _player;
    GameObject _treasure; //����
    [SerializeField, Header("��̃^�O��")] string _treasureTagName;
    [SerializeField] float _maxDis = 20; //���[�_�[����������ő勗��
    [SerializeField] float _mostCloseDis = 2; //���[�_�[����������ł��߂�����
    float _currentDistance;
    private void Awake()
    {
        _needleTransform = _needle.GetComponent<Transform>();
        _currentDistance = float.MaxValue;
    }

    private void Update()
    {
        _needleTransform.eulerAngles = new Vector3(0, 0, GetNeedleRotation());
    }

    /// <summary>���[�^�̐j�̊p�x�����߂郁�\�b�h</summary>
    /// <returns>Z��rotation�̐��l</returns>
    float GetNeedleRotation()
    {
        float totalAngleSize = _zeroMeterAngle - _maxMeterAngle;
        NearObjectWithTag(_treasureTagName);
        //�^�J�����Ȃ��Ȃ�
        if(!_treasure)
        {
            return _zeroMeterAngle;
        }
        _currentDistance = Vector3.Distance(_player.transform.position, _treasure.transform.position);
        if (_currentDistance > _maxDis)
            return _zeroMeterAngle;
        else if (_currentDistance < _mostCloseDis)
            return _maxMeterAngle;

        //�����𐳋K������
        float distanceNormalized = 1 - _currentDistance / _maxDis;
        //float distanceNormalized = _currentDis / _treasureDis;
        return _zeroMeterAngle - distanceNormalized * totalAngleSize;
    }

    /// <summary>�w�肵���^�O�̃I�u�W�F�N�g�̒������ԋ߂��Q�[���I�u�W�F�N�g��ݒ肷�� </summary>
    /// <param name="tagName">���ׂ����^�O</param>
    void NearObjectWithTag(string tagName)
    {
        //�Y���I�u�W�F�N�g��������Ȃ��ꍇ�����Ԃ�
        GameObject[] targets = GameObject.FindGameObjectsWithTag(tagName);
        if(targets.Length == 1)
        {
            _treasure = targets[0];
            return;
        }

        GameObject result = null;
        float minTargetDis = float.MaxValue;
        foreach(var target in targets)
        {
            float targetaDis = Vector3.Distance(_player.transform.position, target.transform.position);
            if(targetaDis < minTargetDis)
            {
                minTargetDis = targetaDis;
                result = target;
            }
        }
        //��ԋ߂��ɂ���I�u�W�F�N�g��ݒ肷��
        _treasure = result;
    }
}
