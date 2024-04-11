using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_move : MonoBehaviour
{
    Transform target = null;
    public float speed;

    // Update is called once per frame
    void Update()
    {
       if(target != null)
        {
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            target = col.gameObject.transform;
            Debug.Log("found");
        }
    }
    private void OnTriggerExit(Collider col)
    {
        target = null;
        Debug.Log("Not found");
    }
}
