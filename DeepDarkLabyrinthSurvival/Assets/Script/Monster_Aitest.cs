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

    private Animation ani;
    private float stoptime = 5f;
    private float timer = 0f;

    private GameObject player;
    private MonsterManager monsterManager;


    private void Awake()
    {
        monsterManager = GetComponent<MonsterManager>();
        ani = GetComponent<Animation>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //�÷��̾�� ����� ��
        if (collision.gameObject.CompareTag("Player"))
        {
            isAttacking = true;
        }


        //�ѿ� �¾��� ��
        //if (collision.gameObject.CompareTag("bullet"))
        //{
            //�Ѿ� ������ �Ѿ� �ı�
            //Destroy(collision.gameObject);

            //die();

        //}

    }

    private void stopmove()
    {
        playscary = true;

        if (playscary)
        {
        //����Ƣ ���� ������, ���ߴ� ���� ���� �� ���Ӥ���
            moveSpeed = 0;
            timer += Time.deltaTime;
        }
            //���ߴ� �ð� �� �Ǹ�
            if (timer >= stoptime)
            {   //���ߴ°� ������ �ð� �ʱ�ȭ
                isAttacking = false;
                timer = 0f;
                moveSpeed = 1f;

                playscary = false;
            }
        }
    

    private void die()
    {
        //�״� ���ϸ��̼� �ֱ�


            //������
            Destroy(gameObject);
        
    }


    private void FixedUpdate()
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
            }


            if (isAttacking)
            {
                stopmove();
            }
        }
    }
}
