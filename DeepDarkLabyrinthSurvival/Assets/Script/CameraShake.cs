using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public GameObject subCamera; // Inspector에서 설정할 서브 카메라

    private float targetZ = 0.3f; // 목표로 하는 Z 좌표
    private float duration = 3.0f; // 이동을 완료하는 데 걸리는 시간
    private float elapsedTime = 0.0f; // 경과 시간

    void Update()
    {
        if (Monster_Aitest.instance.CamSke)
        {
            Debug.Log("카메라 무빙 시작");
            if (elapsedTime < duration)
            {
                // 현재 시간에 따라 증가해야 할 Z 좌표 계산
                float currentZ = Mathf.Lerp(0.0f, targetZ, elapsedTime / duration);

                // 서브 카메라의 위치 설정
                Vector3 newPosition = subCamera.transform.position;
                newPosition.z = currentZ;
                subCamera.transform.position = newPosition;

                // 경과 시간 업데이트
                elapsedTime += Time.deltaTime;
            }
            else
            {
                // 이동이 완료되면 업데이트 중지
                enabled = false;
                Debug.Log("서브 카메라의 Z 좌표가 0.3f가 되었습니다.");
            }
        }
    }
}
