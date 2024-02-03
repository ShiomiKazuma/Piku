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
    GameObject _treasure; //お宝
    [SerializeField, Header("宝のタグ名")] string _treasureTagName;
    [SerializeField] float _maxDis = 20; //レーダーが反応する最大距離
    [SerializeField] float _mostCloseDis = 2; //レーダーが反応する最も近い距離
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

    /// <summary>メータの針の角度を決めるメソッド</summary>
    /// <returns>Z軸rotationの数値</returns>
    float GetNeedleRotation()
    {
        float totalAngleSize = _zeroMeterAngle - _maxMeterAngle;
        NearObjectWithTag(_treasureTagName);
        //タカラがないなら
        if(!_treasure)
        {
            return _zeroMeterAngle;
        }
        _currentDistance = Vector3.Distance(_player.transform.position, _treasure.transform.position);
        if (_currentDistance > _maxDis)
            return _zeroMeterAngle;
        else if (_currentDistance < _mostCloseDis)
            return _maxMeterAngle;

        //距離を正規化する
        float distanceNormalized = 1 - _currentDistance / _maxDis;
        //float distanceNormalized = _currentDis / _treasureDis;
        return _zeroMeterAngle - distanceNormalized * totalAngleSize;
    }

    /// <summary>指定したタグのオブジェクトの中から一番近いゲームオブジェクトを設定する </summary>
    /// <param name="tagName">調べたいタグ</param>
    void NearObjectWithTag(string tagName)
    {
        //該当オブジェクトが一つしかない場合それを返す
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
        //一番近くにあるオブジェクトを設定する
        _treasure = result;
    }
}
