using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraChange : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera[] _vcam;
    int _cameraIndex;
    private void Start()
    {
        _cameraIndex = 0;
        for (int i = 0; i < _vcam.Length; i++)
        {
            if (i == _cameraIndex)
                _vcam[i].Priority = 1;
            else
                _vcam[i].Priority = 0;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            _cameraIndex++;
            if (_cameraIndex > _vcam.Length - 1)
                _cameraIndex = 0;
            for(int i = 0; i < _vcam.Length; i++)
            {
                if (i == _cameraIndex)
                    _vcam[i].Priority = 1;
                else
                    _vcam[i].Priority = 0;
            }
        }
    }
}
