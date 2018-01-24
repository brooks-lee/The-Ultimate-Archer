using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameBehavior : MonoBehaviour {

    public ObjectPool objectPool {
        get;
        private set;
    }

    public AudioController audioController
    {
        get;
        private set;
    }
    private static GameBehavior INSTANCE;
    private ScoreController scoreController;
    private CanvasController canvasController;
    public GameObject MainCamera;
    private Image timerScript;
    private int flag = 0, currScore = 0;
    public bool inputActive;
    public int maxScore;
    private GameObject crossSceneObj;
    private string activeSceneName;
    public bool takeScore;
    private void Awake()
    {
        INSTANCE = this;
        objectPool = new ObjectPool();
        audioController = new AudioController();
        GameObject trailParentContainer = GameObject.Find("TrailParentContainer");
        if (trailParentContainer)
        {
            trailParentContainer.transform.Find("TrailParent").gameObject.SetActive(false);
            DontDestroyOnLoad(trailParentContainer);
        }


        crossSceneObj = GameObject.Find("CrossSceneObject");
        maxScore = crossSceneObj.GetComponent<CrossSceneController>().maxScore;
        string[] balloonObjects = { "Balloons0", "Balloons1", "Balloons2", "Score Target" };
        string targetName;
        string levelNum = crossSceneObj.GetComponent<CrossSceneController>().startBoardName.Replace("Board","");// = "Board" + (Convert.ToInt32(activeSceneName.Replace("Balloons", "")) + 1).ToString();
        if (levelNum.Equals(""))
        {
            targetName = "Score Target";
        }
        else
        {
            targetName = "Balloons" + (Convert.ToInt32(levelNum)-1).ToString();
        }
        if (targetName.Equals("Balloons0"))
        {
            foreach (string balloonName in balloonObjects)
            {
                if (!balloonName.Equals("Balloons0"))
                {
                    GameObject.Find(balloonName).SetActive(false);
                }
            }
        }
        else if (targetName.Equals("Balloons1"))
        {
            foreach (string balloonName in balloonObjects)
            {
                if (!balloonName.Equals("Balloons1"))
                {
                    GameObject.Find(balloonName).SetActive(false);
                }
            }
        }
        else if (targetName.Equals("Balloons2"))
        {
            foreach (string balloonName in balloonObjects)
            {
                if (!balloonName.Equals("Balloons2"))
                {
                    GameObject.Find(balloonName).SetActive(false);
                }
            }
        }
        else
        {
            foreach (string balloonName in balloonObjects)
            {
                if (!balloonName.Equals("Score Target"))
                {
                    GameObject.Find(balloonName).SetActive(false);
                }
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        inputActive = false; takeScore = false;
        MainCamera = GameObject.Find("Main Camera");
        DontDestroyOnLoad(crossSceneObj);
        timerScript = GameObject.Find("Canvas").transform.Find("TimerBg").transform.Find("Timer").GetComponent<Image>();
        canvasController = GameObject.Find("Canvas").GetComponent<CanvasController>();
        GameBehavior.getInstance().objectPool.addToPool(new PoolObject(Resources.Load<GameObject>("Prefabs/BalloonBurstEffect0"), 10, true));
        GameBehavior.getInstance().objectPool.addToPool(new PoolObject(Resources.Load<GameObject>("Prefabs/BalloonBurstEffect1"), 10, true));
        GameBehavior.getInstance().objectPool.addToPool(new PoolObject(Resources.Load<GameObject>("Prefabs/BalloonBurstEffect2"), 10, true));
        GameBehavior.getInstance().objectPool.addToPool(new PoolObject(Resources.Load<GameObject>("Prefabs/BalloonBurstEffect3"), 10, true));
        GameBehavior.getInstance().objectPool.addToPool(new PoolObject(Resources.Load<GameObject>("Prefabs/Sphere"), 10, true));

        StartCoroutine(LateStart());      

    }

    private IEnumerator LateStart()
    {
        yield return new WaitForEndOfFrame();
        UIController bUIScript = GetComponent<BalloonUIController>();
        StartCoroutine(bUIScript.Timer("Score as high as you can before time \n gets over.",flag));
        yield return new WaitForSeconds(2f);
        flag = 1;
        inputActive = true;
    }
	// Update is called once per frame
	void Update () {
        //flag is used so that the timer starts after the instructions
        if(flag==1)
        timerScript.fillAmount -= Time.deltaTime * 0.05f;
        if (timerScript.fillAmount == 0)
        {
            canvasController.gameOver = true;
            if (currScore >= maxScore )
            {
                canvasController.targetReached = true;
            }

            else 
            {
                canvasController.targetReached = false;
            }
        }
        
	}

    public static GameBehavior getInstance()
    {
        return INSTANCE;
    }

    private IEnumerator SceneChange(string sceneName)
    {
        yield return new WaitForSeconds(2f);
        Application.LoadLevel(sceneName);
    }
    public void IncrementScore(int score)
    {
        currScore += score;
        takeScore = false;
    }

    public void continueGame()
    {
        GetComponent<fadeScreen>().fadeIn();
        string startBoardName = crossSceneObj.GetComponent<CrossSceneController>().startBoardName;
        int boardNum = Convert.ToInt32(startBoardName.Replace("Board", "0")) + 1;
        crossSceneObj.GetComponent<CrossSceneController>().startBoardName = "Board" + boardNum.ToString();// "Board" + (Convert.ToInt32(activeSceneName.Replace("balloon", "")) + 1).ToString();
        if ((Convert.ToInt32(startBoardName.Replace("Board", "0"))) < 3)
        {
            crossSceneObj.GetComponent<CrossSceneController>().maxScore = crossSceneObj.GetComponent<CrossSceneController>().maxScores[boardNum];
        }
        StartCoroutine(SceneChange("levelMap"));
    }

    public void quitGame()
    {
        GetComponent<fadeScreen>().fadeIn();
    }

    public int getScore()
    {
        return currScore;
    }

    public void resetScore()
    {
        currScore = 0;
    }
}
