using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMoveTest : MonoBehaviour
{
    Rigidbody _rb;
    float _h, _v;
    [SerializeField] float _moveSpeed = 10;
    Quaternion _targetRotation;
    [SerializeField] float _groundDrag = 10;
    [SerializeField, Header("�U���������")] float _rotateSpeed = 10f;
    [SerializeField, Header("�ڒn����p�̃I�u�W�F�N�g")] Transform _groundCheckTrn;
    [SerializeField, Header("�O���E���h�`�F�b�N�p���C���[")] LayerMask _groundLayer;
    bool IsGround = false;
    [SerializeField] float _groundCheckRayLength = 0.5f;

    [Header("Slope Handling")]
    [SerializeField] float _maxSlopeAngle = 45;
    float _slopeCheckRayLength = 0.5f;
    private RaycastHit _slopeHit;
    private bool _exitingSlope;
    Vector3 _dir;
    [SerializeField] Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        //�}�E�X�J�[�\�����\���ɂ���
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");
        CheckGround();
    }

    private void FixedUpdate()
    {
        Move();
        
    }

    private void Move()
    {
        _dir = new Vector3(_h, 0, _v);
        _dir = Camera.main.transform.TransformDirection(_dir);
        _dir.y = 0;
        _dir = _dir.normalized;
        //���炩�ɉ�]������
        if (_dir.magnitude > 0)
        {
            _targetRotation = Quaternion.LookRotation(_dir, Vector3.up);
            //transform.rotation = Quaternion.Lerp(quaternion, transform.rotation, _rotateSpeed);
        }
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, _rotateSpeed);
        if(OnSlope())
        {
            _rb.velocity = GetSlopeMoveDirection() * _moveSpeed;
        }
        else
        {
            _rb.velocity = _dir * _moveSpeed;
        }
        if(_rb.velocity.magnitude > 0)
        {
            _animator.SetBool("IsWalk", true);
        }
        else
        {
            _animator.SetBool("IsWalk", false);
        }
        
    }

    /// <summary>
    /// �ڒn����
    /// </summary>
    void CheckGround()
    {
        //�ڒn����
        IsGround = Physics.Raycast(_groundCheckTrn.position, Vector3.down, _groundCheckRayLength, _groundLayer);
        //IsGround = Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _groundLayer);
        if (IsGround)
        {
            _rb.drag = _groundDrag;
            //Debug.Log("�ڒn���Ă��܂�");
        }
        else
        {
            _rb.drag = 0;
        }
    }

    /// <summary>
    /// �X���[�v����
    /// </summary>
    /// <returns>�X���[�v��ɂ���̂�</returns>
    bool OnSlope()
    {
        if (Physics.Raycast(_groundCheckTrn.position, Vector3.down, out _slopeHit, _slopeCheckRayLength))
        {
            float angle = Vector3.Angle(Vector3.up, _slopeHit.normal);
            return angle < _maxSlopeAngle && angle != 0;
        }

        return false;
    }

    Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(_dir, _slopeHit.normal).normalized;
    }

    public void PlayerDamage(int damage)
    {
        GameManager._instance.Damage(damage);
        _animator.SetTrigger("Damage");
        SoundManager._instance.PlaySE(SESoundData.SE.Damage);
    }
}
