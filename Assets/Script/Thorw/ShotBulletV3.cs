using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBulletV3 : MonoBehaviour
{
    /// <summary> 弾のPrefab </summary>
    [SerializeField, Tooltip("弾のPrefab")] GameObject _bulletPrefab;
    /// <summary> 発射位置のオブジェクト </summary>
    [SerializeField, Tooltip("発射位置のオブジェクト")] GameObject _barrelObject;
    /// <summary> 弾を生成する位置情報 </summary>
    Vector3 _instantiatePosition;
    /// <summary> 発射位置のオブジェクト </summary>
    [SerializeField, Tooltip("弾が着地する距離をきめる")]
    float _bulletDistance = 10f;
    /// <summary> 発射角度 </summary>
    float _radius;
    /// <summary> 到達高度 </summary>
    float _hight = 80;
    /// <summary> 弾の生成座標の読み取り </summary>
    public Vector3 InstantiatePosition
    {
        get { return _instantiatePosition; }
    }

    /// <summary> 弾の速さ </summary>
    [SerializeField, Range(1.0f, 20.0f), Tooltip("弾の射出する速度")]
    float _speed = 1.0f;

    /// <summary> 弾の初速度 </summary>
    Vector3 _shootVelocity;
    /// <summary> 弾の初速度の読み取り </summary>
    public Vector3 ShootVelocity
    {
        get { return _shootVelocity; }
    }

    void Update()
    {
        //角度を更新
        CheckRadiusAndSpeed();
        //弾の初速度を更新
        float vz = _speed * Mathf.Cos(_radius * Mathf.Deg2Rad);
        float vy = _speed * Mathf.Sin(_radius * Mathf.Deg2Rad);
        _shootVelocity = new Vector3(0, vy, vz);
       // _shootVelocity = _barrelObject.transform.up * _speed;

        //弾の生成座標を更新
        _instantiatePosition = _barrelObject.transform.position;

        // 発射
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 弾を生成して飛ばす
            GameObject obj = Instantiate(_bulletPrefab, _instantiatePosition, Quaternion.identity);
            Rigidbody rid = obj.GetComponent<Rigidbody>();
            rid.AddForce(_shootVelocity * rid.mass, ForceMode.Impulse);
            
            // 5秒後に消える
            Destroy(obj, 5.0F);
        }
    }

    void CheckRadiusAndSpeed()
    {
        _radius = Mathf.Atan(_hight * 4 / _bulletDistance); //
        _speed = Mathf.Sqrt(2 * 9.8f * _hight) / Mathf.Sin(_radius);
        
    }
}
