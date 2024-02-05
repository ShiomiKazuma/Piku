using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PikiminCall : MonoBehaviour
{
    [SerializeField, Header("笛の半径")] float _radius = 5.0f;
    [SerializeField] FormationControl _formationController;
    [SerializeField] ParticleSystem _particleSystem;

    private void Start()
    {
        _particleSystem.gameObject.SetActive(false);
        _particleSystem.Stop();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("よんでいる");
            CallPikmin();
            //パーティクルをよぶ
            _particleSystem.gameObject.SetActive(true);
            _particleSystem.Play();
        }
        else if(Input.GetKeyUp(KeyCode.E))
        {
            _particleSystem.Stop();
            _particleSystem.gameObject.SetActive(false);
        }
    }
    public void CallPikmin()
    {
        //RaycastHit[] hits = Physics.SphereCastAll(this.transform.position, _radius);
        var pikmin = Physics.OverlapSphere(this.transform.position, _radius);
        foreach(var p in pikmin)
        {
            var pikg = p.gameObject;
            //ピクミンなら隊列に加わる
            Pikmin pik = pikg.GetComponent<Pikmin>();
            if (pik != null && pik._state != Pikmin.PikminState.Jump)
            {
                if(pik._state == Pikmin.PikminState.Carry)
                {
                    
                    if(pik.IsChild)
                    {
                        var parent = pik.transform.parent.gameObject;
                        var parentObj = parent.GetComponent<ObjectController>();
                        parentObj.DisPikmin();
                        pik.gameObject.transform.parent = null;
                        pik.IsChild = false;
                    }
                    pik.IsFirstCarry = false;
                }
                _formationController.CallPikmin(pik.gameObject);
            }
            
        }
    }
}
