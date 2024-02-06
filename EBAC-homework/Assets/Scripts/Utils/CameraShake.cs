using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using Cinemachine;

public class CameraShake : Singleton<CameraShake>
{
    public CinemachineVirtualCamera virtualCamera;
    
    [Header("Shake Values")]
    public float amplitude = 3;
    public float frequency = 3;
    public float time = .2f;

    private CinemachineBasicMultiChannelPerlin _noise;
    private float _shakeTime;

    private void Start()
    {
        _noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    [NaughtyAttributes.Button]
    public void Shake()
    {
        ShakeCamera(amplitude, frequency, time);
    }

    public void ShakeCamera(float amplitude, float frequency, float time)
    {
        _noise.m_AmplitudeGain = amplitude;
        _noise.m_FrequencyGain = frequency;
        _shakeTime = time;
    }

    private void Update()
    {
        if(_shakeTime >0)
        {
            _shakeTime -= Time.deltaTime;
        }
        else
        {
        _noise.m_AmplitudeGain = 0f;
        _noise.m_FrequencyGain = 0f;
        }
    }




}
