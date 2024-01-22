using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationControl : MonoBehaviour
{
    [SerializeField] GameObject _parent; //まとめ用オブジェクト
    [SerializeField] GameObject _model; //追尾キャラ
    [SerializeField, Header("先頭の人が追うもの")] Transform _targetTransform;
    GameObject _token; //複製キャラ
    public List<GameObject> _tokenList; //リスト

    /// <summary> 追尾キャラを生成するためのメソッド </summary>
    public void OnGenerate()
    {
        _token = Instantiate(_model, _targetTransform.position, transform.rotation);
        _token.SetActive(true);
        _token.transform.SetParent(_parent.transform);
        _tokenList.Add(_token);
        //_token.GetComponent<ChaseTokenController>()._targetTransform = this.gameObject.transform;
        //隊列にする
        if(_tokenList.IndexOf(_token) == 0)
        {
            _token.GetComponent<Pikmin>()._targetTransform = this._targetTransform;
        }
        else
        {
            _token.GetComponent<Pikmin>()._targetTransform = _tokenList[_tokenList.IndexOf(_token) - 1].transform;
        }
    }

    /// <summary> 追尾キャラを削除するためのメソッド </summary>
    public void OnRemove()
    {
        _tokenList.Clear();
        for(int i = 0; i < _parent.transform.childCount; i++)
        {
            GameObject child = _parent.transform.GetChild(i).gameObject;
            Destroy(child);
        }
    }
}
