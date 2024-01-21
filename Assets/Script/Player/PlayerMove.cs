using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ThirdPersonMove : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] Transform _playerTrn;
    [SerializeField] Transform _orientationTrn;
    [SerializeField] Transform _groundCheckTrn;
    [Header("Movement")]
    [SerializeField] float _moveSpeed = 10;
    [SerializeField] float rotationSpeed = 180;
    [SerializeField] float _groundDrag = 10;
    [SerializeField, Header("振り向く速さ")] float _rotateSpeed = 600f;
    
    
    float _h, _v;
    [Header("Ground Check")]
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] float _playerHeight = 1f;
    bool IsGround = true;
    [Header("Slope Handling")]
    [SerializeField] float _maxSlopeAngle = 45;
    float _slopeCheckRayLength = 0.5f;
    private RaycastHit _slopeHit;
    private bool _exitingSlope;

    Rigidbody _rb;
    [SerializeField, Header("カメラ")] Transform _cameraTrn;
    Vector3 _moveDirection;
    Quaternion _targetRotation;

    Vector2 moveInput; // 移動入力
    bool jumpInput;

    readonly float GROUND_DRAG = 5;
    readonly float GRAVITY = 9.81f;
    readonly Vector2 VECTOR2_ZERO = new Vector2(0, 0);
    float _airMultiple = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.drag = _groundDrag;
    }

    // Update is called once per frame
    void Update()
    {
        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");
        CheckGround();
        Rotate();
        
    }

    void FixedUpdate()
    {
        Move();
        //Vector3 dir =  new Vector3(_h, 0, _v);
        //dir = Camera.main.transform.TransformDirection(dir);
        //dir.y = 0;
        //dir = dir.normalized;
        ////滑らかに回転させる
        //if (dir.magnitude > 0)
        //{
        //    _targetRotation = Quaternion.LookRotation(dir, Vector3.up);
        //    //transform.rotation = Quaternion.Lerp(quaternion, transform.rotation, _rotateSpeed);
        //}
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, _rotateSpeed);
        //_rb.velocity = dir * _moveSpeed;
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    private void Move()
    {
        _moveDirection = _orientationTrn.forward * _v + _orientationTrn.right * _h;
        //スロープ上
        if(OnSlope() && !_exitingSlope)
        {
            _rb.AddForce(GetSlopeMoveDirection() * _moveSpeed * 20f, ForceMode.Force);
            if(_rb.velocity.y > 0)
            {
                _rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
        }
        //地上
        else if(IsGround)
        {
            _rb.AddForce(_moveDirection.normalized * _moveSpeed * 10f, ForceMode.Force);
        }
        //空中
        else if(!IsGround)
        {
            _rb.AddForce(_moveDirection.normalized * _moveSpeed * 10f * _airMultiple, ForceMode.Force);

        }

        //スロープにいるとき重力を無効か
        _rb.useGravity = !OnSlope();
    }

    /// <summary>
    /// 接地判定
    /// </summary>
    void CheckGround()
    {
        //接地判定
        //isGrounded = Physics.Raycast(_groundCheckTrn.position, Vector3.down, _groundCheckRayLength, _whatIsGround);
        IsGround = Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _groundLayer);
        if(IsGround)
        {
            _rb.drag = _groundDrag;
        }
        else
        {
            _rb.drag = 0;
        }
    }

    /// <summary>
    /// 回転処理
    /// </summary>
    void Rotate()
    {
        var dir = _playerTrn.position - new Vector3(_cameraTrn.position.x, _playerTrn.position.y, _cameraTrn.position.z);
        _orientationTrn.forward = dir.normalized;
        _moveDirection = _orientationTrn.forward * _v + _orientationTrn.right * _h;
        if(_moveDirection != Vector3.zero)
        {
            _playerTrn.forward = Vector3.Slerp(_playerTrn.forward, _moveDirection.normalized, _rotateSpeed * Time.deltaTime);

        }
    }

    /// <summary>
    /// 最高速度制限
    /// </summary>
    void SpeedControl()
    {
        if(OnSlope() && !_exitingSlope)
        {
            if(_rb.velocity.magnitude > _moveSpeed)
                _rb.velocity = _rb.velocity.normalized * _moveSpeed;
        }
        else
        {
            Vector3 flatVel = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);
            if(flatVel.magnitude > _moveSpeed)
            {
                Vector3 limitVel = flatVel.normalized * _moveSpeed;
                _rb.velocity = new Vector3(limitVel.x, _rb.velocity.y, limitVel.z);
            }
        }
    }

    /// <summary>
    /// スロープ判定
    /// </summary>
    /// <returns>スロープ上にいるのか</returns>
    bool OnSlope()
    {
        if(Physics.Raycast(_groundCheckTrn.position, Vector3.down, out _slopeHit, _slopeCheckRayLength))
        {
            float angle = Vector3.Angle(Vector3.up, _slopeHit.normal);
            return angle < _maxSlopeAngle && angle != 0;
        }

        return false;
    }

    Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(_moveDirection, _slopeHit.normal).normalized;
    }


}
