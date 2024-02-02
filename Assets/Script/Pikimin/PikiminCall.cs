using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PikiminCall : MonoBehaviour
{
    [SerializeField, Header("“J‚Ì”¼Œa")] float _radius = 5.0f;
    [SerializeField] FormationControl _formationController;
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("‚æ‚ñ‚Å‚¢‚é");
            CallPikmin();
        }
    }
    public void CallPikmin()
    {
        //RaycastHit[] hits = Physics.SphereCastAll(this.transform.position, _radius);
        var pikmin = Physics.OverlapSphere(this.transform.position, _radius);
        foreach(var p in pikmin)
        {
            var pikg = p.gameObject;
            //ƒsƒNƒ~ƒ“‚È‚ç‘à—ñ‚É‰Á‚í‚é
            Pikmin pik = pikg.GetComponent<Pikmin>();
            if (pik != null)
            {
                _formationController.CallPikmin(pik.gameObject);
            }
            
        }
    }
}
