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
    [SerializeField] GameObject _treasure; //����
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

    float GetNeedleRotation()
    {
        float totalAngleSize = _zeroMeterAngle - _maxMeterAngle;
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
}
