using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirebaseManager : MonoBehaviour
{
    public bool realPlayer = false;
    public string playerId;

    public bool timerEnabled = false;
    public float gameTime = 0f;

    Scene teleportScene;
    public int totalTeleport;
    public float totalWalk;
    public float totalFree;
    public int totalSnap;

    private static FirebaseManager _instance;
    private DatabaseReference reference;

    public static FirebaseManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<FirebaseManager>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public class BasicData
    {
        public bool realPlayer;
        public string timestamp;
        public Scene[] scenes;
    }

    public class LocomotionAction
    {
        public float gameTimestamp;
        public string type;
    }

    public class Scene
    {
        public string type;
        public float totalTime;

        public int totalTeleport;
        public float totalWalk;
        public float totalFree;
        public int totalSnap;

        public float ratioTeleport;
        public float ratioWalk;
        public float ratioFree;
        public float ratioSnap;

        public List<LocomotionAction> actions;
    }

    void Start()
    {
        totalTeleport = 0;
        totalWalk = 0f;
        totalFree = 0f;
        totalSnap = 0; 

        reference = FirebaseDatabase.DefaultInstance.RootReference;
        if (playerId == null)
        {
            playerId = generatePlayerId(realPlayer);
        }

        if (SceneManager.GetActiveScene().name == "")
        {
            pushInitalData();
        }
        else if(SceneManager.GetActiveScene().name == "TTP")
        {
            teleportScene = new Scene();
            teleportScene.actions = new List<LocomotionAction>();
        }

        gameTime = 0f;
    }

    private void Update()
    {
        if(timerEnabled)
        {
            gameTime += Time.deltaTime;
        }
    }

    string generatePlayerId(bool realPlayer)
    {
        string baseCode = "0123456789abcdefghijklmnopqrstuvwxABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string code = "";

        for (int i = 0; i < 20; i++) {
            int a = Random.Range(0, baseCode.Length);
            code = code + baseCode[a];
        }
        if (realPlayer)
        {
            code = "REAL-" + code;
        }
        else
        {
            code = "DEV-" + code;
        }

        return code;
    }

    public void pushInitalData()
    {
        BasicData basicData = new BasicData();
        basicData.realPlayer = realPlayer;
        basicData.timestamp = System.DateTime.Now.ToString();
        string json = JsonUtility.ToJson(basicData);

        reference.Child(generatePlayerId(realPlayer)).SetRawJsonValueAsync(json);

        Debug.Log(json);
    }

    public void enableGameTime()
    {
        timerEnabled = true;
    }

    public void disableGameTime()
    {
        timerEnabled = false;
    }

    public void addTeleportAction()
    {   if (!timerEnabled)
            return;

        LocomotionAction locomotionAction = new LocomotionAction();
        locomotionAction.type = "teleport";
        locomotionAction.gameTimestamp = gameTime;

        teleportScene.actions.Add(locomotionAction);

        totalTeleport++;
    }

    public void addSnapAction()
    {
        if (!timerEnabled)
            return;

        LocomotionAction locomotionAction = new LocomotionAction();
        locomotionAction.type = "snap";
        locomotionAction.gameTimestamp = gameTime;

        teleportScene.actions.Add(locomotionAction);

        totalSnap++;
    }

    public void pushTeleportInformation()
    {
        teleportScene.totalTime = gameTime;

        teleportScene.totalTeleport = totalTeleport;
        teleportScene.ratioTeleport = (totalTeleport * 60) / gameTime;

        teleportScene.totalSnap = totalSnap;
        teleportScene.ratioSnap = (totalSnap * 60) / gameTime;

        teleportScene.totalWalk = 0f;
        teleportScene.ratioWalk = 0f;

        teleportScene.totalFree = 0f;
        teleportScene.ratioFree = 0f;

        string json = JsonUtility.ToJson(teleportScene);

        reference.Child(playerId).Child("Scenes").Child("Teleport").SetRawJsonValueAsync(json);

        Debug.Log(json);
    }
}
