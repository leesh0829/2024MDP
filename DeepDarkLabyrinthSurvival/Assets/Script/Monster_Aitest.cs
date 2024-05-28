using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Aitest : MonoBehaviour
{
    public float detectionRange = 10f;
    public float moveSpeed = 1f;
    private bool playscary = false;
    //공격여부
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
        //플레이어랑 닿았을 때
        if (collision.gameObject.CompareTag("Player"))
        {
            isAttacking = true;
        }


        //총에 맞았을 때
        //if (collision.gameObject.CompareTag("bullet"))
        //{
            //총알 닿으면 총알 파괴
            //Destroy(collision.gameObject);

            //die();

        //}

    }

    private void stopmove()
    {
        playscary = true;

        if (playscary)
        {
        //갑툭튀 여따 넣으셈, 멈추는 동안 실행 될 거임ㅇㅇ
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


    private void FixedUpdate()
    {

        player = GameObject.FindWithTag("Player");
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        // 플레이어가 일정 범위 내에 있으면
        if (distanceToPlayer <= detectionRange)
        {
            //거리 측정
            Vector3 direction = (player.transform.position - transform.position).normalized;

            // 플레이어를 향해 이동
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
