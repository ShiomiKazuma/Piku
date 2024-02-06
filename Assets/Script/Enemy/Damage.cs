using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] Vector3 _range = new Vector3(1, 0.75f, 0.5f);
    [SerializeField] GameObject _parent;
    [SerializeField] ParticleSystem _particleSystem;
    [SerializeField] Collider _collider;
    bool IsAttack = false;

    private void Start()
    {
        //_collider.enabled = false;
    }
    public void Attack()
    {
        SoundManager._instance.PlaySE(SESoundData.SE.SlimeAttack);
        IsAttack = true;
       // _collider.enabled = true;
        //var range = Physics.OverlapBox(new Vector3(_parent.transform.position.x, _parent.transform.position.y + 0.5f, _parent.transform.position.z + 1.5f), _range, Quaternion.identity);
        //var range = Physics.OverlapBox(transform.TransformDirection(new Vector3(_parent.transform.position.x, _parent.transform.position.y + 0.5f, _parent.transform.position.z + 1.5f)), _range, Quaternion.identity);
        //foreach (var c in range)
        //{
        //    if(c.gameObject.tag == "Player")
        //    {
        //        var player = c.gameObject.GetComponent<PlayerMoveTest>();
        //        player.PlayerDamage(1);
        //    }
        //    else if(c.gameObject.tag == "Pikmin")
        //    {
        //        var pik = c.gameObject.GetComponent<Pikmin>();
        //        pik.Death();
        //    }
        //}

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        //Gizmos.DrawWireCube(new Vector3(_parent.transform.position.x, _parent.transform.position.y + 0.5f, _parent.transform.position.z + 1.5f), _range);
        //Gizmos.DrawWireCube(transform.TransformDirection(new Vector3(_parent.transform.position.x, _parent.transform.position.y + 0.5f, _parent.transform.position.z + 1.5f)), _range);
    }

    public void Death()
    {
        SoundManager._instance.PlaySE(SESoundData.SE.SlimeDeath);
        Instantiate(_particleSystem, this.transform.position, Quaternion.identity);
        Destroy(_parent, 3f);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!IsAttack) return;
        if (other.gameObject.tag == "Player")
        {
            IsAttack = false;
            var player = other.gameObject.GetComponent<PlayerMoveTest>();
            player.PlayerDamage(1);
        }
        else if (other.gameObject.tag == "Pikmin")
        {
            IsAttack = false;
            var pik = other.gameObject.GetComponent<Pikmin>();
            pik.Death();
        }
    }

    public void Finish()
    {
        //_collider.enabled = false;
        IsAttack = false;
    }
}
