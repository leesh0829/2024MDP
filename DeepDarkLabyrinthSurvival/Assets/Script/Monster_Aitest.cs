using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Aitest : MonoBehaviour
{
    private GameObject player;
    public float moveSpeed = 1f;
    public float detectionRange = 10f;

    private bool isAttacking = false;
    private float stoptime = 3f;
    private float timer = 0;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("플레이어랑 닿음");
            isAttacking = true;

        }
    }


    private void Update()
    {
        // 플레이어를 감지함 플레이어와 적의 사이의 거리
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // 플레이어가 일정 범위 내에 있으면
        if (distanceToPlayer <= detectionRange)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;

            // 플레이어를 향해 이동
            if (isAttacking == false)
            {
                transform.LookAt(player.transform);
                transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
            }

            //플레이어랑 닿았을 때
            if (isAttacking)
            {
                Debug.Log("시간 증가중");
                timer += Time.deltaTime;

                //멈추는 시간 다 되면
                if (timer >= stoptime)
                {   //멈추는거 끝내고 시간 초기화
                    isAttacking = false;
                    timer = 0f;
                    moveSpeed = 1f;
                    Debug.Log("끝남");
                }
                //멈추고 있음
                else
                {
                    Debug.Log("멈추고 있음");
                    moveSpeed = 0f;
                }
            }
        }
    }
}
