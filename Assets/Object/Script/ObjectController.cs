using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    [SerializeField] int _pikminCount = 1;
    [SerializeField] float _objBasicSpeed = 1.0f;
    [SerializeField, Header("オブジェクトのサイズ")] float _objSize;
    [SerializeField, Header("目的地")] GameObject _destination;
    ObjState _state;
    float _objSpeed;
    public enum ObjState
    {
        Idle,
        Carry,
    }

    private void Awake()
    {
        _state = ObjState.Idle;
    }
    void Update()
    {
        //目的地についていたら処理を変える
        if(Vector3.Distance(transform.position, _destination.transform.position) <= 0.25f)
        {
            Finish();
            return;
        }
        //自身の子オブジェクトを数える
        int childCount = this.gameObject.transform.childCount;
        if(childCount > _pikminCount)
        {
            _state = ObjState.Carry;
        }
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Pikmin")
        {
            //ピクミンが子オブジェクトになる
        }
    }

    private void Finish()
    {
        Debug.Log("到着しました");
    }
}
