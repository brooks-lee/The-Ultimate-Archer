using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour {
    private GameObject arrowView, arrowCamObj, wallObj;
    private Camera MainCamera;
    private BalloonUIController uiControlScript;
    private UIController uiCntrlScript;
    private CanvasController canvasController;

    private void Awake()
    {
        MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        arrowView = GameObject.Find("Arrow View");
        uiCntrlScript = GameObject.Find("Game Controller").GetComponent<BalloonUIController>();
        arrowCamObj = arrowView.transform.Find("Arrow Camera").gameObject;
        wallObj = GameObject.Find("Wall");
        canvasController = GameObject.Find("Canvas").GetComponent<CanvasController>();
        if (arrowCamObj)
        {
            arrowCamObj.SetActive(true);
            MainCamera.gameObject.SetActive(false);
            arrowCamObj.transform.parent = transform;
            arrowCamObj.transform.localPosition = new Vector3(-4.2f, 1.2f, -14f);
        }

    }
    // Use this for initialization
    void Start () {
        uiControlScript = GameObject.Find("Game Controller").GetComponent<BalloonUIController>();
    }
	
	// Update is called once per frame
	void Update () {
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.Equals("Border"))
        {
            StartCoroutine(uiControlScript.Timer("Missed"));
            GameBehavior.getInstance().IncrementScore(0);
            GameBehavior.getInstance().inputActive = true;
            /*arrowCamObj.GetComponent<Camera>().gameObject.SetActive(false);
            arrowCamObj.transform.parent = arrowView.transform;
            MainCamera.gameObject.SetActive(true);
            gameObject.SetActive(false);*/
            transform.GetComponent<Collider>().enabled = false;
            gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
            
        }

        if (collision.transform.tag.Equals("Score") )//&& GameBehavior.getInstance().takeScore)
        {
            GameBehavior.getInstance().takeScore = false;
            transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.transform.parent = transform;

            GameBehavior.getInstance().audioController.playScoreBoardSound();
            GameBehavior.getInstance().IncrementScore(Convert.ToInt32(collision.gameObject.name));
            transform.GetComponent<Collider>().enabled = false;
            transform.GetChild(0).transform.GetComponent<MeshRenderer>().enabled = false;
            canvasController.ScaleUpScore(Convert.ToInt32(collision.gameObject.name), GameBehavior.getInstance().maxScore);
            StartCoroutine(uiCntrlScript.Timer(collision.gameObject.name));
        }
    }
}
