using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster_Aitest : MonoBehaviour
{
    public float detectionRange = 5f;
    public float moveSpeed = 7f;
    private bool playscary = false;
    private bool isAttacking = false;
    private bool undying = false;

    private Animator animator; 
    private float undying_time = 25f;
    private float little_undying_time = 5f;

    private float stoptime = 5f;
    private float timer = 0f;
    private int hitcount = 0;

    private GameObject player;
    public GameObject subcamera;
    public bool CamSke = false;
    public bool JSAnime = false;
    public static Monster_Aitest instance;
    protected float curHealth; //���� ü��
    public float maxHealth = 3;  //�ִ� ü��
    public Slider HpBarSlider;

    private void Awake()
    {
        animator = GetComponent<Animator>(); // Animator ������Ʈ �ʱ�ȭ

        if(Monster_Aitest.instance == null )
        {
            Monster_Aitest.instance = this;
        }

    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        subcamera = GameObject.FindWithTag("SubCamera");

        // �������� �ڷ�ƾ ����
        StartCoroutine(undying_coroutine());
        SetHp();
    }

    public void SetHp()
    {
        curHealth = maxHealth;
        HpBarSlider.value = curHealth / maxHealth;
    }

    public void CheckHp()
    {
        if(HpBarSlider != null)
            HpBarSlider.value = curHealth / maxHealth;
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
            // �Ѿ� ������ �Ѿ� �ı�
            Destroy(collision.gameObject);
            if (maxHealth != 0 || curHealth >= 0)
            {
                curHealth--;
                CheckHp();
            }
            hitcount++;
            if (hitcount == 3 && curHealth <= 0)
            {
                Gamemanager.instance.ClearGame();
                die();
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
        subcamera.transform.GetChild(0).gameObject.SetActive(true);
        JSAnime = true;
        //CamSke = true;
        yield return new WaitForSeconds(3);
        JSAnime = false;
        //CamSke = false;
        subcamera.transform.GetChild(0).gameObject.SetActive(false);
        yield break;
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


        yield return new WaitForSeconds(undying_time);
        undying = false;
    }

    // ��� �����ð�
    IEnumerator little_undying_coroutine()
    {
        // ���� �ð� ���� ���� ����
        undying = true;

        yield return new WaitForSeconds(little_undying_time);
        undying = false;
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
                Gamemanager.instance.GameOver();
                stopmove();
                StartCoroutine(little_undying_coroutine());
            }
        }
    }
}
