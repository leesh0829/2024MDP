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

    private Vector3 oripos;
    private GameObject player;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isAttacking = true;
            oripos = transform.position;
        }
    }

    private void stopmove()
    {
        Debug.Log("시간 증가하고 멈추고 있음");
        playscary = true;

        if (playscary)
        {
            //갑툭튀 여따 넣으셈, 멈추는 동안 실행 될 거임ㅇㅇ
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
            transform.position = oripos;
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
