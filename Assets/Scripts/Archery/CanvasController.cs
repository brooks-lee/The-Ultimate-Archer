using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour{
    private GameObject score, quitObj, continueObj, arrowObj, scoreObj, timerObj, totalScore;
    private RectTransform scoreTransform;public RectTransform goldCupTransform;
    private Text arrowText;
    private BowScript bowScript;
    public bool gameOver, targetReached;
    private void Awake()
    {
    }

    void Start()
    {
        scoreTransform = null;goldCupTransform = null;
        gameOver = false; targetReached = false;
        quitObj = GameObject.Find("Canvas").transform.Find("Quit Image").gameObject;
        continueObj = GameObject.Find("Canvas").transform.Find("Continue Image").gameObject;
        arrowObj = GameObject.Find("Canvas").transform.Find("Arrow Image").gameObject;
        scoreObj = GameObject.Find("Canvas").transform.Find("Score Image").gameObject;
        timerObj = GameObject.Find("Canvas").transform.Find("TimerBg").gameObject;
        totalScore = GameObject.Find("Canvas").transform.Find("TotalScore").gameObject;
        bowScript = GameObject.Find("Bow").GetComponent<BowScript>();
        RectTransform[] rectTransforms = GetComponentsInChildren<RectTransform>();
        foreach (RectTransform rectTransform in rectTransforms)
        {
            if (rectTransform.gameObject.name.Equals("Score"))
            {
                scoreTransform = rectTransform;
            }
            if (rectTransform.gameObject.name.Equals("Arrow Text"))
            {
                arrowText = rectTransform.GetComponent<Text>();
            }
            if (rectTransform.gameObject.name.Equals("Gold Cup"))
            {
                goldCupTransform = rectTransform;
                goldCupTransform.gameObject.SetActive(false);
            }
        }
    }

    public void ScaleUpScore(float score, float totalScore)
    {
        float scoreToIncrease = score / totalScore;
        scoreTransform.localScale += new Vector3(scoreToIncrease * 0.1f, 0f, 0f);
    }

    public void ReduceArrow()
    {
        string arrowsLeft = arrowText.text.Replace("\t", "");
        arrowsLeft = arrowsLeft.Replace("/5","");
        bowScript.totalArrowCount = Int32.Parse(arrowsLeft) - 1;
        arrowText.text = "\t"+bowScript.totalArrowCount.ToString()+"/5";
    }

    private void Update()
    {
        if (gameOver)
        {
            GameBehavior.getInstance().inputActive = false;//take no input
            
            if (!Camera.main.transform.GetComponent<Blur>())
            {
                Blur blurScript = Camera.main.gameObject.AddComponent<Blur>();
                if (blurScript)
                {
                    blurScript.blurShader = Resources.Load<Shader>("Shaders/BlurEffectConeTaps");
                    blurScript.enabled = true;
                }
            }

            if (targetReached)
            {
                goldCupTransform.gameObject.SetActive(true);
                Vector3 targetPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
                Vector3 currPosition = goldCupTransform.localPosition;
                goldCupTransform.localPosition = Vector3.MoveTowards(currPosition, targetPosition, 150f * Time.deltaTime);
                if (goldCupTransform.localScale.x <= 2f)
                    goldCupTransform.localScale += new Vector3(5f * Time.deltaTime, 5f * Time.deltaTime, 0f);
                totalScore.transform.position = goldCupTransform.localPosition + goldCupTransform.up * (-2f);
            }

            if (!GameBehavior.getInstance().takeScore)
            {
                totalScore.GetComponent<Text>().text += GameBehavior.getInstance().getScore();
                GameBehavior.getInstance().takeScore = true;
                GameBehavior.getInstance().resetScore();
            }

            totalScore.SetActive(true);
            quitObj.SetActive(true);
            continueObj.SetActive(true);
            arrowObj.SetActive(false);
            timerObj.SetActive(false);
            scoreObj.SetActive(false);
        }
    }
}
