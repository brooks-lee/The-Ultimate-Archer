using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonScript : MonoBehaviour {
    List<List<GameObject>> pool;
    private BalloonUIController uiControlScript;
    private CanvasController canvasController;
    // Use this for initialization
    void Start () {
        pool = GameBehavior.getInstance().objectPool.pool;
        uiControlScript = GameObject.Find("Game Controller").GetComponent<BalloonUIController>();
        canvasController = GameObject.Find("Canvas").GetComponent<CanvasController>();
    }
	
	// Update is called once per frame
	void Update () {
        }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Arrow"))
        {
            GameBehavior.getInstance().audioController.playBalloonSound();
            int totalScore = GameBehavior.getInstance().maxScore;
            canvasController.ScaleUpScore(50, totalScore);
            int index = (int)(poolItems)System.Enum.Parse(typeof(poolItems), "BalloonBurstEffect" + transform.parent.name.Replace("Balloon", ""));
            collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            GameBehavior.getInstance().IncrementScore(50);
            foreach (GameObject item in pool[index])
            {
                if (!item.activeInHierarchy)
                {
                    item.SetActive(true);
                    item.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)) + new Vector3(0,0,1f); //transform.parent.localPosition  + transform.forward*(-3f) + Vector3.up * 1f;
                    break;
                }
            }
            transform.GetComponent<Collider>().enabled = false;
            transform.GetComponentInChildren<MeshRenderer>().enabled = false;
            collision.transform.GetComponent<Collider>().enabled = false;
            collision.transform.GetComponentInChildren<MeshRenderer>().enabled = false;
            StartCoroutine(uiControlScript.Timer("50"));
        }

    }

    



}
