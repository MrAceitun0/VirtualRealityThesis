using System.Collections;
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

    private void OnTriggerStay(Collider other)
    {
        if (objRenderer.material.name.Contains(opaqueName))
        {
            return;
        }

        if (other.tag == "Carretilla" && !this.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable>().isSelected)
        {
            StressScenePacer.Instance.stage = 5;
            src.PlayOneShot(coinSound);
            objRenderer.material = opaqueMat;
            this.gameObject.SetActive(false);
        }
    }
}
