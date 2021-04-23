using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using Firebase;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager instance;

    DatabaseReference mDatabaseRef;

    public string playerId;

    public float gameplayTime = 0f;
    public int totalSnap;
    public int totalTeleport;
    public float totalFree;
    public float totalWalk;

    public XRNode rightHandNode;
    public InputDevice rightHand;

    public bool timeEnabled = true;
    public bool realUser = false;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        if (playerId == null) { 
            playerId = CreateRandomString();
        }

        rightHand = InputDevices.GetDeviceAtXRNode(rightHandNode);

        mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeEnabled)
        {
            gameplayTime += Time.deltaTime;
        }
    }

    public void teleportingPlayer()
    {
        totalTeleport++;
        LocomotionAction action = new LocomotionAction("teleport", gameplayTime);
        actions.Add(action);
    }

    public void movingPlayer()
    {
        totalFree++; 
        LocomotionAction action = new LocomotionAction("free", gameplayTime);
        actions.Add(action);
    }

    public void walkingPlayer()
    {
        totalWalk++;
        LocomotionAction action = new LocomotionAction("walk", gameplayTime);
        actions.Add(action);
    }

    public void snappingPlayer()
    {
        totalSnap++;
        LocomotionAction action = new LocomotionAction("snap", gameplayTime);
        actions.Add(action);
    }

    public IEnumerator PushToDatabase(string type)
    {
        DBItem item = new DBItem(System.DateTime.UtcNow, type, totalSnap, totalTeleport, totalFree, percentageFree, totalWalk, percentageWalk, gameplayTime);
        string json = JsonUtility.ToJson(item);

        var DBTask = mDatabaseRef.Child("gameplays").Child(playerId).SetValueAsync(3);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            Debug.Log("Pushed");
        }
    }

    private string CreateRandomString(int stringLength = 20)
    {
        int _stringLength = stringLength - 1;
        string randomString = "";
        string[] characters = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        for (int i = 0; i <= _stringLength; i++)
        {
            randomString = randomString + characters[Random.Range(0, characters.Length)];
        }
        return randomString;
    }
}
