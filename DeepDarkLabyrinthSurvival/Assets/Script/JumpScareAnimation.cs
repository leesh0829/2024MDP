using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScareAnimation : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>(); // Animator ������Ʈ �ʱ�ȭ
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Monster_Aitest.instance.JSAnime == true)
        {
            animator.SetBool("JSAnime", true);
            Debug.Log("����Ƣ �ִ� ����");
        }
        if (Monster_Aitest.instance.JSAnime == false)
        {
            animator.SetBool("JSAnime", false);
        }

    }
}
