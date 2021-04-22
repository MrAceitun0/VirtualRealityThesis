using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

public class Fish : MonoBehaviour
{
    public GameObject fishSpine;
    public GameObject cookedFish;

    public Camera cam;

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<XRGrabInteractable>().isSelected && Vector3.Distance(cam.transform.position, transform.position) < 0.3f)
        {
            fishSpine.SetActive(true);
            cookedFish.SetActive(false);
            this.gameObject.GetComponent<AudioSource>().Play();
            GetComponent<ObjectRespawning>().respawn = false;
        }
    }
}
