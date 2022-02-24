using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance{get;private set;}
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float shakeTime;
     private void Awake() {
         Instance=this;
         cinemachineVirtualCamera=GetComponent<CinemachineVirtualCamera>();
        
    }

    public void ShakeCamera(){
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin=
        cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain=1.3f;
        shakeTime=0.5f;
    }
    
      void Update()
    {
        if(shakeTime>0){
            shakeTime-=Time.deltaTime;
            if(shakeTime<=0f){
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin=
                cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain=0f;
            }
        }
    }
}
