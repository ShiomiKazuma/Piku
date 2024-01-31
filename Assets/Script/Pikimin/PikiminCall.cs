using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PikiminCall : MonoBehaviour
{
    [SerializeField, Header("“J‚Ì”¼Œa")] float _radius = 5.0f;
    public void CallPikmin()
    {
        var pikmin = Physics.OverlapSphere(this.transform.position, _radius);
        foreach(var p in pikmin)
        {
            //ƒsƒNƒ~ƒ“‚È‚ç‘à—ñ‚É‰Á‚í‚é
        }
    }
}
