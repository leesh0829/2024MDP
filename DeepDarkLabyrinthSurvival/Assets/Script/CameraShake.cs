using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Monster_Aitest MA = new Monster_Aitest();

    public float shakeTime = 1.0f;
    public float shakeSpeed = 2.0f;
    public float shakeAmount = 1.0f;

    private Transform cam;

    // Start is called before the first frame update
    void Start()
    {
            cam = GameObject.FindGameObjectWithTag("SubCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (MA.CamSke)
        {
            StartCoroutine(Shake());
            MA.CamSke = false;
        }
    }

    IEnumerator Shake()
    {
        Vector3 originPosition = cam.localPosition;
        float elapsedTime = 0.0f;

        while (elapsedTime < shakeTime)
        {
            Vector3 randomPoint = originPosition + Random.insideUnitSphere * shakeAmount;
            cam.localPosition = Vector3.Lerp(cam.localPosition, randomPoint, Time.deltaTime * shakeSpeed);

            yield return null;

            elapsedTime += Time.deltaTime;
        }

        cam.localPosition = originPosition;
    }
}