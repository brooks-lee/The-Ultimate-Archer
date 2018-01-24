using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//make it a singleton class
public class UIController : MonoBehaviour {
    public GameObject scoreBackground, scoreForeground, MainCamera, arrowView;
    public Transform arrowCameraTransform;
    public bool expand;
	// Use this for initialization
	void Start () {
        expand = false;
        scoreBackground = new GameObject("Score Background");
        scoreBackground.transform.localScale = Vector3.one;
        SpriteRenderer scoreRenderer = scoreBackground.AddComponent<SpriteRenderer>();
        scoreRenderer.sprite = Resources.Load<Sprite>("Images/black");
        Vector3 b_extents = scoreRenderer.bounds.extents;
        float worldScreenHeight = 2 * Mathf.Tan(0.5f * Camera.main.fieldOfView) * 1f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
        scoreBackground.transform.localScale = new Vector3(worldScreenWidth / b_extents.x, 0.02f*worldScreenHeight / b_extents.y, 1f);
        if (GameObject.Find("Score Target"))
            scoreBackground.transform.position = GameObject.Find("Score Target").transform.position + new Vector3(0f, 0f, -3f);

        scoreForeground = new GameObject("Score Foreground");
        scoreRenderer = scoreForeground.AddComponent<SpriteRenderer>();
        scoreRenderer.sprite = Resources.Load<Sprite>("Images/Black Pattern");
        Vector3 f_extents = scoreRenderer.bounds.extents;
        scoreForeground.transform.parent = scoreBackground.transform;
        scoreForeground.transform.localScale = Vector3.one;
        scoreForeground.transform.localScale = new Vector3(worldScreenWidth / b_extents.x, 0.25f * worldScreenHeight / b_extents.y, 1f);
        scoreForeground.transform.localPosition = Vector3.zero;


        MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>().gameObject;
        arrowView = GameObject.Find("Arrow View");
        arrowCameraTransform = arrowView.transform.Find("Arrow Camera");

        scoreBackground.SetActive(false);
        afterStart();
    }

    void afterStart()
    {

    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) || expand)
        {
            //Debug.Log("expand set to true");
            float var = scoreForeground.transform.localScale.x;
            float stretch = Mathf.Clamp(var + Time.deltaTime * 8f, var, 6.5f);
            scoreForeground.transform.localScale = new Vector3(stretch, scoreForeground.transform.localScale.y, scoreForeground.transform.localScale.z);
        }
	}

    public void SetScoreBgFg()
    {
        scoreBackground.SetActive(true);
        scoreBackground.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0.5f, 0f)) + new Vector3(0f, 0f, 1f);
        scoreForeground.transform.parent = null;
        expand = true;
    }

    public void ResetScoreBgFg()
    {
        scoreForeground.transform.parent = scoreBackground.transform;
        expand = false;
        scoreForeground.transform.localScale = Vector3.one;
        scoreBackground.SetActive(false);
    }

    public virtual IEnumerator Timer(string text, int t = 1) {
        yield return null;
    }
}
