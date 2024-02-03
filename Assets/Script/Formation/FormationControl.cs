using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationControl : MonoBehaviour
{
    [SerializeField] GameObject _parent; //�܂Ƃߗp�I�u�W�F�N�g
    [SerializeField, Header("�擪�̐l���ǂ�����")] Transform _targetTransform;
    public List<GameObject> _tokenList; //���X�g

    /// <summary> �s�N�~�����Ă΂ꂽ���̏��� </summary>
    public void CallPikmin(GameObject pikmin)
    {
        pikmin.transform.SetParent(_parent.transform);
        _tokenList.Add(pikmin);
        pikmin.GetComponent<Pikmin>().SetPikminState(Pikmin.PikminState.Follow);
        //_token.GetComponent<ChaseTokenController>()._targetTransform = this.gameObject.transform;
        //����ɂ���
        if (_tokenList.IndexOf(pikmin) == 0)
        {
            pikmin.GetComponent<Pikmin>()._targetTransform = this._targetTransform;
        }
        else
        {
            pikmin.GetComponent<Pikmin>()._targetTransform = _tokenList[_tokenList.IndexOf(pikmin) - 1].transform;
        }
    }

    /// <summary> �ǔ��L�������폜���邽�߂̃��\�b�h </summary>
    public void OnRemove()
    {
        _tokenList.Clear();
        for (int i = 0; i < _parent.transform.childCount; i++)
        {
            GameObject child = _parent.transform.GetChild(i).gameObject;
            Destroy(child);
        }
    }
}
