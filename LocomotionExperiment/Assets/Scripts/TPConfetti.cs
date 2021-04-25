using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPConfetti : MonoBehaviour
{
    public ParticleSystem particlesA;
    public ParticleSystem particlesB;
    bool first = true;

    // Update is called once per frame
    void Update()
    {
        if(first && transform.position.z > 30.5f && transform.position.x > 3.5f && transform.position.x < 8f){
            particlesA.Play();
            particlesA.gameObject.GetComponent<AudioSource>().Play();
            Destroy(particlesA.gameObject, 2f);
            particlesB.Play();
            particlesB.gameObject.GetComponent<AudioSource>().Play();
            Destroy(particlesB.gameObject, 2f);
            FirebaseManager.Instance.disableGameTime();
            FirebaseManager.Instance.pushSceneInformation();
            first = false;
        }
    }
}
