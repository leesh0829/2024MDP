using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Aitest : MonoBehaviour
{

    public float detectionRange = 5f;
    public float moveSpeed = 1f;
    private bool playscary = false;
    //공격여부
    private bool isAttacking = false;
    //무적여부
    private bool undiying = false;
    
    private Animation ani;
    private float undying_time = 10f;
    private float stoptime = 5f;
    private float timer = 0f;

    private GameObject player;


    //초기화
    private void Awake()
    {
        //아직 구현 안함 나중에 하기로 06/04
        ani = GetComponent<Animation>();
    }


    private void Start()
    {
        player = GameObject.FindWithTag("Player");

        //무적상태 코루틴 실행
        StartCoroutine(undiying_corutin());
    }

    private void OnCollisionEnter(Collision collision)
    {
        //플레이어랑 닿았을 때
        if (collision.gameObject.CompareTag("Player") && !undiying)
        {
            isAttacking = true;
        }


        //총에 맞았을 때
        if (collision.gameObject.CompareTag("bullet") && !undiying)
        {
            //총알 닿으면 총알 파괴
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
            //멈추는 시간 다 되면
            if (timer >= stoptime)
            {   //멈추는거 끝내고 시간 초기화
                isAttacking = false;
                timer = 0f;
                moveSpeed = 1f;

                playscary = false;
            }
        }
    

    private void die()
    {
        //죽는 에니메이션 넣기


        //삭제됨
        Destroy(gameObject);
        
    }

    IEnumerator undiying_corutin()
    {
        //일정 시간동안 멈춤
        undiying = true;
        Debug.Log("무적상태 활성화");

        yield return new WaitForSeconds(undying_time);
        undiying = false;
        Debug.Log("무적상태 비비비비활성화");
    }

    private void FixedUpdate()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        // 플레이어가 일정 범위 내에 있으면 && 무적시간이 끝나있으면


        if (distanceToPlayer <= detectionRange && !undiying)
        {
            //거리 측정
            Vector3 direction = (player.transform.position - transform.position).normalized;

            // 플레이어를 향해 이동
            if (!isAttacking && !undiying)
            {
                transform.LookAt(player.transform);
                transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
            }

            //공격 당할 시
            if (isAttacking && !undiying)
            {
                stopmove();
            }
        }
    }
}
