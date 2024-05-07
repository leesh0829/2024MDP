using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Aitest : MonoBehaviour
{
    public float detectionRange = 10f;
    public float moveSpeed = 1f;
    private bool playscary = false;
    //���ݿ���
    private bool isAttacking = false;
    private float stoptime = 5f;
    private float timer = 0f;

    private Vector3 oripos;
    private GameObject player;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isAttacking = true;
            oripos = transform.position;
        }
    }

    private void stopmove()
    {
        Debug.Log("�ð� �����ϰ� ���߰� ����");
        playscary = true;

        if (playscary)
        {
            //����Ƣ ���� ������, ���ߴ� ���� ���� �� ���Ӥ���
            moveSpeed = 0;
            timer += Time.deltaTime;

            //���ߴ� �ð� �� �Ǹ�
            if (timer >= stoptime)
            {   //���ߴ°� ������ �ð� �ʱ�ȭ
                isAttacking = false;
                timer = 0f;
                moveSpeed = 1f;

                //���� ���ϰ� �ҷ��� �ӽ÷� �ص�
                Debug.Log("����----------------");
                playscary = false;
            }
            transform.position = oripos;
        }
    }

 

    private void Update()
    {

        player = GameObject.FindWithTag("Player");
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        // �÷��̾ ���� ���� ���� ������
        if (distanceToPlayer <= detectionRange)
        {
            //�Ÿ� ����
            Vector3 direction = (player.transform.position - transform.position).normalized;

            // �÷��̾ ���� �̵�
            if (!isAttacking)
            {
                transform.LookAt(player.transform);
                transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
                Debug.Log("�̵���---");
            }


            if (isAttacking)
            {
                stopmove();
            }
        }
    }
}
