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
    protected float curHealth; //현재 체력
    public float maxHealth = 3;  //최대 체력
    public Slider HpBarSlider;

    private void Awake()
    {
        animator = GetComponent<Animator>(); // Animator 컴포넌트 초기화

        if(Monster_Aitest.instance == null )
        {
            Monster_Aitest.instance = this;
        }

    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        subcamera = GameObject.FindWithTag("SubCamera");

        // 무적상태 코루틴 실행
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

        // 플레이어랑 닿았을 때
        if (collision.gameObject.CompareTag("Player") && !undying)
        {
            isAttacking = true;
            StartCoroutine(jumpScare());
        }

        // 총에 맞았을 때
        if (collision.gameObject.CompareTag("bullet") && !undying)
        {
            // 총알 닿으면 총알 파괴
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
        // 멈추는 시간 다 되면
        if (timer >= stoptime)
        {
            // 멈추는거 끝내고 시간 초기화
            isAttacking = false;
            timer = 0f;
            moveSpeed = 1f;
            playscary = false;
        }
    }

    private void die()
    {
        // 죽는 애니메이션 트리거
        animator.SetTrigger("death");
        // 죽는 애니메이션 재생 후 오브젝트 파괴
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
        // 현재 애니메이션 상태의 길이를 가져와 대기
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject); // 오브젝트 파괴
    }

    IEnumerator undying_coroutine()
    {
        // 일정 시간 동안 무적 상태
        undying = true;


        yield return new WaitForSeconds(undying_time);
        undying = false;
    }

    // 잠시 무적시간
    IEnumerator little_undying_coroutine()
    {
        // 일정 시간 동안 무적 상태
        undying = true;

        yield return new WaitForSeconds(little_undying_time);
        undying = false;
    }

    private void FixedUpdate()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        // 플레이어가 일정 범위 내에 있고 무적 시간이 끝난 경우
        if (distanceToPlayer <= detectionRange && !undying)
        {
            // 거리 측정
            Vector3 direction = (player.transform.position - transform.position).normalized;

            // 플레이어를 향해 이동
            if (!isAttacking && !undying)
            {
                transform.LookAt(player.transform);
                transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
            }

            // 공격 당할 시
            if (isAttacking && !undying)
            {
                Gamemanager.instance.GameOver();
                stopmove();
                StartCoroutine(little_undying_coroutine());
            }
        }
    }
}
