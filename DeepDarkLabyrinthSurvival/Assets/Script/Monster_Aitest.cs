using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Aitest : MonoBehaviour
{
    private GameObject player;
    public float moveSpeed = 1f;
    public float detectionRange = 10f;

    private bool isAttacking = false;
    private float stoptime = 3f;
    private float timer = 0;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("�÷��̾�� ����");
            isAttacking = true;

        }
    }


    private void Update()
    {
        // �÷��̾ ������ �÷��̾�� ���� ������ �Ÿ�
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // �÷��̾ ���� ���� ���� ������
        if (distanceToPlayer <= detectionRange)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;

            // �÷��̾ ���� �̵�
            if (isAttacking == false)
            {
                transform.LookAt(player.transform);
                transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
            }

            //�÷��̾�� ����� ��
            if (isAttacking)
            {
                Debug.Log("�ð� ������");
                timer += Time.deltaTime;

                //���ߴ� �ð� �� �Ǹ�
                if (timer >= stoptime)
                {   //���ߴ°� ������ �ð� �ʱ�ȭ
                    isAttacking = false;
                    timer = 0f;
                    moveSpeed = 1f;
                    Debug.Log("����");
                }
                //���߰� ����
                else
                {
                    Debug.Log("���߰� ����");
                    moveSpeed = 0f;
                }
            }
        }
    }
}
