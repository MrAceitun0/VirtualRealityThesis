using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    public InputDeviceCharacteristics inputDeviceCharacteristics;

    private InputDevice targetDevice;
    public GameObject handModelPrefab;
    private GameObject spawnedHandModel;

    private Animator animator;

    void Start()
    {
        List<InputDevice> inputDevices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(inputDeviceCharacteristics, inputDevices);

        foreach(var device in inputDevices) 
        {
            Debug.Log(device.name + " " + device.characteristics);
        }

        if (inputDevices.Count > 0) 
        {
            spawnedHandModel = Instantiate(handModelPrefab, transform);
            targetDevice = inputDevices[0];
        }

        animator = spawnedHandModel.GetComponent<Animator>();
    }

    void Update()
    {
        updateHandAnimation();
    }

    private void updateHandAnimation()
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerValue) && triggerValue)
        {
            animator.SetFloat("Trigger", 1.0f);
        }
        else
        {
            animator.SetFloat("Trigger", 0f);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.gripButton, out bool gripValue) && gripValue)
        {
            animator.SetFloat("Grip", 1.0f);
        }
        else
        {
            animator.SetFloat("Grip", 0f);
        }
    }
}
