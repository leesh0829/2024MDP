using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Aitest : MonoBehaviour
{
    public float detectionRange = 5f;
    public float moveSpeed = 3f;
    private bool playscary = false;
    private bool isAttacking = false;
    private bool undying = false;

    private Animator animator; 
    private float undying_time = 35f;
    private float little_undying_time = 5f;

    private float stoptime = 5f;
    private float timer = 0f;
    private int hitcount = 0;

    private GameObject player;
    //public GameObject JumpScare;

    private void Awake()
    {
        animator = GetComponent<Animator>(); // Animator 컴포넌트 초기화
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");

        // 무적상태 코루틴 실행
        StartCoroutine(undying_coroutine());
    }

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(jumpScare());

        // 플레이어랑 닿았을 때
        if (collision.gameObject.CompareTag("Player") && !undying)
        {
            isAttacking = true;
        }

        // 총에 맞았을 때
        if (collision.gameObject.CompareTag("bullet") && !undying)
        {
            // 총알 닿으면 총알 파괴
            Destroy(collision.gameObject);
            hitcount++;
            if (hitcount == 3)
            {
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
        GameObject JumpScare = GameObject.Find("JumpScare");
        JumpScare.gameObject.SetActive(true);
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
                stopmove();
                StartCoroutine(little_undying_coroutine());
            }
        }
    }
}
