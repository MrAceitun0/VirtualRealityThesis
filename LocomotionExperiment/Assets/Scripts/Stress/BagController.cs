﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagController : MonoBehaviour
{
    public MeshRenderer objRenderer;

    public Material opaqueMat;

    string opaqueName;

    public AudioSource src;
    public AudioClip coinSound;

    private void Start()
    {
        opaqueName = opaqueMat.name;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (objRenderer.material.name.Contains(opaqueName))
        {
            return;
        }

        if (other.tag == "Carretilla" && !this.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable>().isSelected)
        {
            objRenderer.material = opaqueMat;
            Destroy(this.gameObject);
            //CalmScenePacer.Instance.stage++;
            src.PlayOneShot(coinSound);
        }
    }
}
