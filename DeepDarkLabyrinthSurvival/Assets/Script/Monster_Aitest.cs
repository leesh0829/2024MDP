using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Aitest : MonoBehaviour
{
    //�÷��̾� ���� �Ÿ�
    public float detectionRange = 5f;
    //�̵��ӵ�
    public float moveSpeed = 3f;
   
    private bool playscary = false;
    private bool isAttacking = false;
    private bool undying = false;

    private Animator animator; 
    //���� �����ð�
    private float undying_time = 7f;
    //�Ͻ� �����ð�
    private float little_undying_time = 5f;

    //���ߴ� �ð�
    private float stoptime = 5f;
    private float timer = 0f;

    //���� �� ��Ʈī����
    private int hitcount = 0;

    private GameObject player;
    public GameObject subcamera;
    public bool CamSke = false;

    private void Awake()
    {
        animator = GetComponent<Animator>(); // Animator ������Ʈ �ʱ�ȭ
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        subcamera = GameObject.FindWithTag("SubCamera");

        // �������� �ڷ�ƾ ����
        StartCoroutine(undying_coroutine());
    }

    private void OnCollisionEnter(Collision collision)
    {

        // �÷��̾�� ����� ��
        if (collision.gameObject.CompareTag("Player") && !undying)
        {
            isAttacking = true;
            StartCoroutine(jumpScare());


        }

        // �ѿ� �¾��� ��
        if (collision.gameObject.CompareTag("bullet") && !undying)
        {
            
            if (hitcount == 3)
            {
                die();
                GameManager.instance.ClearGame();
            }
            else
            {
                hitcount++;
                Debug.Log("Monster hit count: " + hitcount); // Monster hitcount ���� Ȯ��
                //�Ѿ� �ı�
                Destroy(collision.gameObject);

            }
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
        // ���ߴ� �ð� �� �Ǹ�
        if (timer >= stoptime)
        {
            // ���ߴ°� ������ �ð� �ʱ�ȭ
            isAttacking = false;
            timer = 0f;
            moveSpeed = 1f;
            playscary = false;
        }
    }

    private void die()
    {
        // �״� �ִϸ��̼� Ʈ����
        animator.SetTrigger("death");
        // �״� �ִϸ��̼� ��� �� ������Ʈ �ı�
        StartCoroutine(Destroy());
    }

    IEnumerator jumpScare()
    {
        Debug.Log("����Ƣ");

        subcamera.transform.GetChild(0).gameObject.SetActive(true);
        CamSke = true;
        yield return new WaitForSeconds(3);
        subcamera.transform.GetChild(0).gameObject.SetActive(false);
    }

    private IEnumerator Destroy()
    {
        // ���� �ִϸ��̼� ������ ���̸� ������ ���
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject); // ������Ʈ �ı�
    }

    IEnumerator undying_coroutine()
    {
        // ���� �ð� ���� ���� ����
        undying = true;
        Debug.Log("�������� Ȱ��ȭ");

        yield return new WaitForSeconds(undying_time);
        undying = false;
        Debug.Log("�������� ��Ȱ��ȭ");
    }

    // ��� �����ð�
    IEnumerator little_undying_coroutine()
    {
        // ���� �ð� ���� ���� ����
        undying = true;
        Debug.Log("�������� Ȱ��ȭ");

        yield return new WaitForSeconds(little_undying_time);
        undying = false;
        Debug.Log("�������� ��Ȱ��ȭ");
    }

    private void FixedUpdate()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        // �÷��̾ ���� ���� ���� �ְ� ���� �ð��� ���� ���
        if (distanceToPlayer <= detectionRange && !undying)
        {
            // �Ÿ� ����
            Vector3 direction = (player.transform.position - transform.position).normalized;

            // �÷��̾ ���� �̵�
            if (!isAttacking && !undying)
            {
                transform.LookAt(player.transform);
                transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
            }

            // ���� ���� ��
            if (isAttacking && !undying)
            {
                if (GameManager.instance != null)
                {
                    GameManager.instance.GameOver();
                }
                stopmove();
                StartCoroutine(little_undying_coroutine());
            }
        }
    }
}
