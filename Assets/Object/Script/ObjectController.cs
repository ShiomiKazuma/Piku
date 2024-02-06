using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObjectController : MonoBehaviour
{
    [SerializeField] int _pikminCount = 4;
    [SerializeField, Header("運べる場所")]
    GameObject[] _objects;
    [SerializeField] float _objBasicSpeed = 1.0f;
    [SerializeField, Header("オブジェクトのサイズ")] float _objSize;
    [SerializeField, Header("目的地")] GameObject _destination;
    [SerializeField] int _currentPikmin = 0;
    [SerializeField] ParticleSystem _particleSystem;
    [SerializeField] int _score = 1000;
    NavMeshAgent _agent;
    ObjState _state;
    float _objSpeed;
    bool IsFinish = true;
    [SerializeField] bool IsScore = true;
    [SerializeField] GameObject _pikmin;
    public enum ObjState
    {
        Idle,
        Carry,
    }

    private void Awake()
    {
        _state = ObjState.Idle;
        _agent = GetComponent<NavMeshAgent>();
        _agent.destination = _destination.transform.position;
    }
    void Update()
    {
        //目的地についていたら処理を変える
        if(Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(_destination.transform.position.x, _destination.transform.position.z)) <= 0.2f && IsFinish)
        {
            Finish();
            return;
        }
        if(_currentPikmin >= _pikminCount)
        {
            _state = ObjState.Carry;
        }
        else
        {
            _state = ObjState.Idle;
        }

        if(_state == ObjState.Idle)
        {
            _agent.isStopped = true;
        }
        else if(_state == ObjState.Carry)
        {
            _agent.isStopped = false;
            _agent.speed = _objBasicSpeed;
        }
    }

    /// <summary>
    /// ピクミンが射程にいたとき
    /// </summary>
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Pikmin")
        {
            var pik = other.gameObject.GetComponent<Pikmin>();
            if(pik._state == Pikmin.PikminState.Idle)
            {
                if(_currentPikmin < _pikminCount)
                {
                    pik.SetCarry(_objects[_currentPikmin], this.gameObject);
                }
            }
        }
    }
    private void Finish()
    {
        _agent.isStopped = true;
        IsFinish = false;
        Component[] components = GetComponentsInChildren<Pikmin>();
        foreach(var component in components)
        {
            component.gameObject.transform.parent = null;
            var pik = component.gameObject.GetComponent<Pikmin>();
            pik._state = Pikmin.PikminState.Idle;
        }
        if(IsScore)
        {
            GameManager._instance.AddScore(_score);
        }
        else
        {
            Born();
        }
        SoundManager._instance.PlaySE(SESoundData.SE.CarryFinish);
        
        Instantiate(_particleSystem, new Vector3(this.transform.position.x, this.transform.position.y + 2.5f, this.transform.position.z), Quaternion.identity);
        Destroy(gameObject, 1f);
    }

    public void AddPikmin()
    {
        _currentPikmin++;
    }

    public void DisPikmin()
    {
        _currentPikmin--;
    }

    void Born()
    {
        Instantiate(_pikmin, new Vector3(this.transform.position.x + 5f, this.transform.position.y + 3f, this.transform.position.z), Quaternion.identity);
    }
}
