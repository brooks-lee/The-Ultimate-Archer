using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonUIController : UIController {
    private GameObject scoreText;
    private CanvasController canvasController;
    // Use this for initialization
    BalloonUIController() {
    }
    private void Awake()
    {
        scoreText = GameObject.Find("Score Text");
        canvasController = GameObject.Find("Canvas").GetComponent<CanvasController>();
    }

    void afterStart()
    {
        scoreBackground.transform.position = Camera.main.transform.position +  new Vector3(0f,0f,2f);
        scoreForeground.transform.localPosition = Vector3.zero;
    }

    private void DisplayScore(string text, int t = 1)
    {
        scoreText.SetActive(true);
        scoreText.GetComponent<TextMesh>().text = text;
        if (scoreText.GetComponent<scoreScript>())
            scoreText.GetComponent<scoreScript>().enabled = true;
        else
            scoreText.AddComponent<scoreScript>();

        scoreText.GetComponent<TextMesh>().characterSize = 0.05f;
        scoreText.GetComponent<TextMesh>().offsetZ = 0.0f;
        if (t == 0)
        {
            scoreText.GetComponent<TextMesh>().characterSize = 0.025f;
            scoreText.GetComponent<TextMesh>().offsetZ = 0.4f;

        }
        
    }

    public GameObject getScoreText()
    {
        return scoreText;
    }
    private void ResetScore()
    {
        ResetScoreBgFg();
        //scoreText.transform.position = new Vector3(Camera.main.transform.position.x, Screen.height/2, Camera.main.transform.position.z);
        scoreText.transform.position = Camera.main.transform.position;
        scoreText.SetActive(false);
    }

    public override IEnumerator Timer(string text, int t =1)
    {
        if (!canvasController.gameOver)
        {
            SetScoreBgFg();
            //yield return new WaitForSeconds(0.1f);
            DisplayScore(text, t);
        }
        yield return new WaitForSeconds(2f);
        
        ResetScore();
        //sets camera
        MainCamera.SetActive(true);
        arrowCameraTransform.gameObject.SetActive(false);
        arrowCameraTransform.parent = arrowView.transform;
        GameBehavior.getInstance().inputActive = true;
        //CollisionHolder.gameObject.SetActive(false);
        yield return null;

    }

}
