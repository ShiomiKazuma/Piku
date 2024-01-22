using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveTest : MonoBehaviour
{
    Rigidbody _rb;
    float _h, _v;
    [SerializeField] float _moveSpeed = 10;
    Quaternion _targetRotation;
    [SerializeField, Header("振り向く速さ")] float _rotateSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        Vector3 dir = new Vector3(_h, 0, _v);
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;
        dir = dir.normalized;
        //滑らかに回転させる
        if (dir.magnitude > 0)
        {
            _targetRotation = Quaternion.LookRotation(dir, Vector3.up);
            //transform.rotation = Quaternion.Lerp(quaternion, transform.rotation, _rotateSpeed);
        }
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, _rotateSpeed);
        _rb.velocity = dir * _moveSpeed;
    }
}
