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
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = _player.transform.position;
        this.transform.position += new Vector3(0, 0, 10);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        _xPos += Input.GetAxis("Mouse X") * _userSensSpeed;
        _zPos += Input.GetAxis("Mouse Y") * _userSensSpeed;

        Vector3 movePos = this.transform.position + new Vector3(_xPos, 0, _zPos);
        movePos.y = _player.transform.position.y;
        float dis = Vector3.Distance(_player.transform.position, movePos);
        if (dis <= _maxDis && dis >= _minDis)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, movePos, _duration);
        }
    }
}
