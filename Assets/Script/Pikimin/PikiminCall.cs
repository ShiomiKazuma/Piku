using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PikiminCall : MonoBehaviour
{
    [SerializeField, Header("�J�̔��a")] float _radius = 5.0f;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("���ł���");
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
            //�s�N�~���Ȃ����ɉ����
            Pikmin pik = pikg.GetComponent<Pikmin>();
            if (pik != null)
            {
                Debug.Log("�s�N�~�����Ă΂ꂽ");
            }
            
        }
    }
}
