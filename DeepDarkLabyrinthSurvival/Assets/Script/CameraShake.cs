using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public GameObject subCamera; // Inspector���� ������ ���� ī�޶�

    private float targetZ = 0.3f; // ��ǥ�� �ϴ� Z ��ǥ
    private float duration = 3.0f; // �̵��� �Ϸ��ϴ� �� �ɸ��� �ð�
    private float elapsedTime = 0.0f; // ��� �ð�

    void Update()
    {
        if (Monster_Aitest.instance.CamSke)
        {
            Debug.Log("ī�޶� ���� ����");
            if (elapsedTime < duration)
            {
                // ���� �ð��� ���� �����ؾ� �� Z ��ǥ ���
                float currentZ = Mathf.Lerp(0.0f, targetZ, elapsedTime / duration);

                // ���� ī�޶��� ��ġ ����
                Vector3 newPosition = subCamera.transform.position;
                newPosition.z = currentZ;
                subCamera.transform.position = newPosition;

                // ��� �ð� ������Ʈ
                elapsedTime += Time.deltaTime;
            }
            else
            {
                // �̵��� �Ϸ�Ǹ� ������Ʈ ����
                enabled = false;
                Debug.Log("���� ī�޶��� Z ��ǥ�� 0.3f�� �Ǿ����ϴ�.");
            }
        }
    }
}
