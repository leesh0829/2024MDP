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
        Debug.Log("시간 증가하고 멈추고 있음");
        playscary = true;

        if (playscary)
        {
            moveSpeed = 0;
            timer += Time.deltaTime;

            //멈추는 시간 다 되면
            if (timer >= stoptime)
            {   //멈추는거 끝내고 시간 초기화
                isAttacking = false;
                timer = 0f;
                moveSpeed = 1f;

                //보기 편하게 할려고 임시로 해둠
                Debug.Log("끝남----------------");
                playscary = false;
            }
        }
    }

    private void die()
    {
        //죽는 에니메이션 넣기


        //시체 몇 초 유지
        die_start++;

        if (die_start == 2)
        {
            //삭제됨
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //플레이어랑 닿았을 때
        if (collision.gameObject.CompareTag("Player"))
        {
            isAttacking = true;
        }

        //총에 맞았을 때
        if(collision.gameObject.CompareTag("bullet"))
        {
            //총알 닿으면 총알 파괴
            Destroy(collision.gameObject);

            die();
            die_start++;

        }
    }

    private void Update()
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
                Debug.Log("이동중---");
            }


            if (isAttacking)
            {
                stopmove();
            }
        }
    }
}
