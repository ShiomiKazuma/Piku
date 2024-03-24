using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGenerator : MonoBehaviour
{
    [SerializeField, Tooltip("ノーツのPrefab")] GameObject _notePrefab;
    [SerializeField, Tooltip("生成場所")] Vector3 _generatoPos;

    public void SpawnNote()
    {
        Instantiate(_notePrefab, _generatoPos, Quaternion.identity);
    }
}
