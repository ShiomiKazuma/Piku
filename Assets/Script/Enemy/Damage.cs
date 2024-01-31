using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] Vector3 _range = new Vector3(1, 0.75f, 0.5f);
    [SerializeField] GameObject _parent;
    public void Attack()
    {
        var range = Physics.OverlapBox(new Vector3(_parent.transform.position.x, _parent.transform.position.y + 0.5f, _parent.transform.position.z + 1.5f), _range, Quaternion.identity);
        foreach(var c in range)
        {
            if(c.gameObject.tag == "Player")
            {
                Debug.Log("dead");
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector3(_parent.transform.position.x, _parent.transform.position.y + 0.5f, _parent.transform.position.z + 1.5f), _range);
    }
}
