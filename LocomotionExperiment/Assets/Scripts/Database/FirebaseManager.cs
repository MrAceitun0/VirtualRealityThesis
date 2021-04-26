using Firebase.Database;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class FirebaseManager : MonoBehaviour
{
    public bool realPlayer = false;
    public static string playerId;

    public bool timerEnabled = false;
    public float gameTime = 0f;

    public int totalTeleport;
    public float totalWalk;
    public float totalFree;
    public int totalSnap;

    private static FirebaseManager _instance;
    private DatabaseReference reference;

    public XRNode inputSource;
    InputDevice device;
    float snapTime = 0.5f;

    private const string projectId = "vrthesisdb"; // You can find this in your Firebase project settings
    private static readonly string databaseURL = $"https://vrthesisdb-default-rtdb.europe-west1.firebasedatabase.app/";

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
    }

    public class LocomotionAction
    {
        public float gameTimestamp;
        public string type;
        public float duration;
    }

    public class Scene
    {
        public float totalTime;

        public int totalTeleport;
        public float totalWalk;
        public float totalFree;
        public int totalSnap;

        public float ratioTeleport;
        public float ratioWalk;
        public float ratioFree;
        public float ratioSnap;
    }

    void Start()
    {
        totalTeleport = 0;
        totalWalk = 0f;
        totalFree = 0f;
        totalSnap = 0; 

        reference = FirebaseDatabase.DefaultInstance.RootReference;

        gameTime = 0f;

        device = InputDevices.GetDeviceAtXRNode(inputSource);
    }

    private void Update()
    {
        if (playerId == null || playerId == "")
        {
            playerId = generatePlayerId(realPlayer);
            pushInitalData();
        }

        if (timerEnabled)
        {
            gameTime += Time.deltaTime;

            snapTime -= Time.deltaTime;
            Vector2 inputAxis;
            device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
            if(inputAxis.x != 0 && snapTime < 0f)
            {
                addSnapAction();
                snapTime = 0.5f;
            }
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

    public static void PostBasicData(BasicData user, string userId)
    {
        Proyecto26.RestClient.Put<BasicData>($"{databaseURL}players/{userId}.json", user);
    }

    public void pushInitalData()
    {
        BasicData basicData = new BasicData();
        basicData.realPlayer = realPlayer;
        basicData.timestamp = System.DateTime.Now.ToString();
        PostBasicData(basicData, playerId);
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

        totalTeleport++;
    }

    public void addSnapAction()
    {
        totalSnap++;
    }

    public void addFreeAction(float duration)
    {
        totalFree += duration;
    }

    public void addWalkAction(float duration)
    {
        totalWalk += duration;
    }

    public void pushSceneInformation()
    {
        Scene scene = new Scene();

        scene.totalTime = gameTime;

        scene.totalTeleport = totalTeleport;
        scene.ratioTeleport = (totalTeleport * 60) / gameTime;

        scene.totalSnap = totalSnap;
        scene.ratioSnap = (totalSnap * 60) / gameTime;

        scene.totalWalk = totalWalk;
        scene.ratioWalk = (totalWalk * 60) / gameTime;

        scene.totalFree = totalFree;
        scene.ratioFree = (totalFree * 60) / gameTime;

        PostSceneData(scene, playerId);
    }

    public static void PostSceneData(Scene scene, string userId)
    {
        Proyecto26.RestClient.Put<BasicData>($"{databaseURL}players/{userId}/Scenes/{SceneManager.GetActiveScene().name}/totals.json", scene);
    }
}
