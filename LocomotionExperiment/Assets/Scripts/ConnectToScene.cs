using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectToScene : MonoBehaviour
{
    public void connectTo(string scene)
    {
        FirebaseManager.Instance.gameTime = 0;
        FirebaseManager.Instance.totalFree = 0f;
        FirebaseManager.Instance.totalWalk = 0f;
        FirebaseManager.Instance.totalTeleport = 0;
        FirebaseManager.Instance.totalSnap = 0;
        SceneManager.LoadScene(scene);
    }
}
