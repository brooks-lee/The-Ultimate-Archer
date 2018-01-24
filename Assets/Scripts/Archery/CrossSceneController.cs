using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossSceneController : MonoBehaviour {
    private Vector3 startposition;
    public int[] maxScores = {30, 400, 1200, 2400 };
    public int maxScore ;
    public string startBoardName { get; set; }
    // Use this for initialization
	void Awake () {
        maxScore = maxScores[0];
        startBoardName = "Board";
    }
	
	// Update is called once per frame
	void Update () {
		
	}

}
