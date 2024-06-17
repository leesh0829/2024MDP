using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Aitest : MonoBehaviour
{

    public float detectionRange = 5f;
    public float moveSpeed = 1f;
    private bool playscary = false;
    //���ݿ���
    private bool isAttacking = false;
    //��������
    private bool undiying = false;
    
    private Animation ani;
    private float undying_time = 10f;
    private float stoptime = 5f;
    private float timer = 0f;

    private GameObject player;


    //�ʱ�ȭ
    private void Awake()
    {
        //���� ���� ���� ���߿� �ϱ�� 06/04
        ani = GetComponent<Animation>();
    }


    private void Start()
    {
        player = GameObject.FindWithTag("Player");

        //�������� �ڷ�ƾ ����
        StartCoroutine(undiying_corutin());
    }

    private void OnCollisionEnter(Collision collision)
    {
        //�÷��̾�� ����� ��
        if (collision.gameObject.CompareTag("Player") && !undiying)
        {
            isAttacking = true;
        }


        //�ѿ� �¾��� ��
        if (collision.gameObject.CompareTag("bullet") && !undiying)
        {
            //�Ѿ� ������ �Ѿ� �ı�
            Destroy(collision.gameObject);

            die();

        }

    }

    private void stopmove()
    {
        playscary = true;

        if (playscary)
        {
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

    IEnumerator undiying_corutin()
    {
        //���� �ð����� ����
        undiying = true;
        Debug.Log("�������� Ȱ��ȭ");

        yield return new WaitForSeconds(undying_time);
        undiying = false;
        Debug.Log("�������� �����Ȱ��ȭ");
    }

    private void FixedUpdate()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        // �÷��̾ ���� ���� ���� ������ && �����ð��� ����������


        if (distanceToPlayer <= detectionRange && !undiying)
        {
            //�Ÿ� ����
            Vector3 direction = (player.transform.position - transform.position).normalized;

            // �÷��̾ ���� �̵�
            if (!isAttacking && !undiying)
            {
                transform.LookAt(player.transform);
                transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
            }

            //���� ���� ��
            if (isAttacking && !undiying)
            {
                stopmove();
            }
        }
    }
}
