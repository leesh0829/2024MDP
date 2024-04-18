using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRShoot : MonoBehaviour
{
    public SimpleShoot simpleShoot;
    publix OVRInput.Button shootButton;

    private OVRGrabbable grabbable;
    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        grabbable = GetCommponent<OVRGrabbable>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
