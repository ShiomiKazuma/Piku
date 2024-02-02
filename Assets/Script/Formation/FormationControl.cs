using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationControl : MonoBehaviour
{
    [SerializeField] GameObject _parent; //まとめ用オブジェクト
    [SerializeField, Header("先頭の人が追うもの")] Transform _targetTransform;
    public List<GameObject> _tokenList; //リスト

    /// <summary> ピクミンが呼ばれた時の処理 </summary>
    public void CallPikmin(GameObject pikmin)
    {
        pikmin.transform.SetParent(_parent.transform);
        _tokenList.Add(pikmin);
        pikmin.GetComponent<Pikmin>().SetPikminState(Pikmin.PikminState.Follow);
        //_token.GetComponent<ChaseTokenController>()._targetTransform = this.gameObject.transform;
        //隊列にする
        if (_tokenList.IndexOf(pikmin) == 0)
        {
            pikmin.GetComponent<Pikmin>()._targetTransform = this._targetTransform;
        }
        else
        {
            pikmin.GetComponent<Pikmin>()._targetTransform = _tokenList[_tokenList.IndexOf(pikmin) - 1].transform;
        }
    }

    /// <summary> 追尾キャラを削除するためのメソッド </summary>
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
