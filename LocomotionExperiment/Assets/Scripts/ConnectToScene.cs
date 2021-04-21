using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectToScene : MonoBehaviour
{
    public void connectTo(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
