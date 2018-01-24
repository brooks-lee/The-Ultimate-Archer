using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScoreController : MonoBehaviour {
    
    private GameObject arrowView, scoreText, gameController;
    private Collision CollisionHolder;
    private Camera MainCamera;
    private UIController uiControlScript;
    private CanvasController canvasController;
    private UIController uiCntrlScript;
    public float totalScoreCount, maxScore;
    
	// Use this for initialization
	void Start () {
        arrowView = GameObject.Find("Arrow View");
        MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        scoreText = GameObject.Find("Score Text");
        gameController = GameObject.Find("Game Controller");
        uiControlScript = gameController.GetComponent<UIController>();
        canvasController = GameObject.Find("Canvas").GetComponent<CanvasController>();
        totalScoreCount = 0; maxScore = GameBehavior.getInstance().maxScore;
        uiCntrlScript = GameObject.Find("Game Controller").GetComponent<BalloonUIController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (GameBehavior.getInstance().getScore() == maxScore)
        {
            canvasController.gameOver = true;            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Arrow"))
        {
            CollisionHolder = collision;
            collision.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
            collision.gameObject.transform.parent = transform;
            GameBehavior.getInstance().audioController.playScoreBoardSound();
            GameBehavior.getInstance().IncrementScore(50);
            collision.transform.GetComponent<Collider>().enabled = false;
            collision.transform.GetChild(0).transform.GetComponent<MeshRenderer>().enabled = false;
            StartCoroutine(uiCntrlScript.Timer("50"));

        }
    }

    //handles Score Display
    private IEnumerator Timer()
    {

        totalScoreCount += 50f;
        //canvasController.ScaleUpScore(50f, GameBehavior.getInstance().maxScore);
        if (totalScoreCount != maxScore)
        {
            uiControlScript.SetScoreBgFg();
            yield return new WaitForSeconds(0.1f);
            DisplayScore();
            yield return new WaitForSeconds(2f);
            //score reset
            ResetScore();
        }
        //sets camera
        MainCamera.gameObject.SetActive(true);
        CollisionHolder.transform.Find("Arrow Camera").gameObject.SetActive(false);
        CollisionHolder.transform.Find("Arrow Camera").parent = arrowView.transform;
        GameBehavior.getInstance().inputActive = true;
        CollisionHolder.gameObject.SetActive(false);
        
    }

    private void DisplayScore()
    {
        scoreText.SetActive(true);
        scoreText.GetComponent<TextMesh>().text = "50";
        if (scoreText.GetComponent<scoreScript>())
            scoreText.GetComponent<scoreScript>().enabled = true; 
        else
            scoreText.AddComponent<scoreScript>(); 
        
    }

    private void ResetScore()
    {
        uiControlScript.ResetScoreBgFg();
        //scoreText.transform.position = new Vector3(Camera.main.transform.position.x, Screen.height/2, Camera.main.transform.position.z);
        scoreText.transform.position = Camera.main.transform.position;
        scoreText.SetActive(false);
    }
}
