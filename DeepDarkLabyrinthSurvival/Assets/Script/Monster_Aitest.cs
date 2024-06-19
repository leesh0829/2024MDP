using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Aitest : MonoBehaviour
{
    //플레이어 감지 거리
    public float detectionRange = 5f;
    //이동속도
    public float moveSpeed = 3f;
   
    private bool playscary = false;
    private bool isAttacking = false;
    private bool undying = false;

    private Animator animator; 
    //시작 무적시간
    private float undying_time = 7f;
    //일시 무적시간
    private float little_undying_time = 5f;

    //멈추는 시간
    private float stoptime = 5f;
    private float timer = 0f;

    //몬스터 총 히트카운터
    private int hitcount = 0;

    private GameObject player;
    public GameObject subcamera;
    public bool CamSke = false;

    private void Awake()
    {
        animator = GetComponent<Animator>(); // Animator 컴포넌트 초기화
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        subcamera = GameObject.FindWithTag("SubCamera");

        // 무적상태 코루틴 실행
        StartCoroutine(undying_coroutine());
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
            
            if (hitcount == 3)
            {
                die();
                GameManager.instance.ClearGame();
            }
            else
            {
                hitcount++;
                Debug.Log("Monster hit count: " + hitcount); // Monster hitcount 증가 확인
                //총알 파괴
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
        Debug.Log("갑툭튀");

        subcamera.transform.GetChild(0).gameObject.SetActive(true);
        CamSke = true;
        yield return new WaitForSeconds(3);
        subcamera.transform.GetChild(0).gameObject.SetActive(false);
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
        Debug.Log("무적상태 활성화");

        yield return new WaitForSeconds(undying_time);
        undying = false;
        Debug.Log("무적상태 비활성화");
    }

    // 잠시 무적시간
    IEnumerator little_undying_coroutine()
    {
        // 일정 시간 동안 무적 상태
        undying = true;
        Debug.Log("무적상태 활성화");

        yield return new WaitForSeconds(little_undying_time);
        undying = false;
        Debug.Log("무적상태 비활성화");
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
