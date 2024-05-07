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

<<<<<<< HEAD
    private int die_stop_time = 2;
    private int die_start = 0;

    private GameObject player;
=======
    private GameObject player;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isAttacking = true;
        }
    }
>>>>>>> parent of 01c64a1c (Merge pull request #19 from leesh0829/hyunjae)

    private void stopmove()
    {
        Debug.Log("�ð� �����ϰ� ���߰� ����");
        playscary = true;

        if (playscary)
        {
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
        }
    }

    private void die()
    {
        //�״� ���ϸ��̼� �ֱ�


        //��ü �� �� ����
        die_start++;

        if (die_start == 2)
        {
            //������
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //�÷��̾�� ����� ��
        if (collision.gameObject.CompareTag("Player"))
        {
            isAttacking = true;
        }

        //�ѿ� �¾��� ��
        if(collision.gameObject.CompareTag("bullet"))
        {
            //�Ѿ� ������ �Ѿ� �ı�
            Destroy(collision.gameObject);

            die();
            die_start++;

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
