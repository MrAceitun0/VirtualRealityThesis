using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRespawning : MonoBehaviour
{
    public Transform playerPosition;
    public Vector3 originalPosition;
    public Vector3 originalRotation;

    bool first = true;
    public bool respawn;

    // Update is called once per frame
    void Update()
    {
        if(respawn 
            && first  
            && (Vector3.Distance(transform.position, playerPosition.position) > 2f))
        {
            StartCoroutine(resetPosition());
        }
    }

    IEnumerator resetPosition()
    {
        //On Function Call
        first = false;

        //yield on a new YieldInstruction that waits for 2 seconds.
        yield return new WaitForSeconds(2);

        //After we have waited 2 seconds print the time again.
        transform.position = originalPosition;
        transform.rotation = Quaternion.Euler(originalRotation);

        first = true;
    }
}
