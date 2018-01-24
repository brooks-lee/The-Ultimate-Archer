using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MotionControl : MonoBehaviour {

    public static MotionControl INSTANCE;
    public bool InMotion;
    private string[] scene_name = { "scene1", "scene2" };
    private int flag = 0;
    private Shader shader;
    private GameObject levelmap, Cubes;
    private GameObject crossSceneObj;
	// Use this for initialization
	void Start () {
        INSTANCE = this;
        InMotion = true;
        levelmap = GameObject.Find("LevelMap");
        crossSceneObj = GameObject.Find("CrossSceneObject");
        GameObject board = levelmap.transform.Find(crossSceneObj.GetComponent<CrossSceneController>().startBoardName).gameObject;
        Debug.Log(board);
        Debug.Log("board name: *" +  " " + crossSceneObj.GetComponent<CrossSceneController>().startBoardName + " inMotion " + InMotion);
        Cubes = levelmap.transform.Find("Cubes").gameObject;
        transform.position = board.transform.Find("Position" + board.name.Replace("Board", "")).transform.position;
        shader = Shader.Find("MK/Glow/Selective/Self-Illumin/Diffuse");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Board") && !other.name.Equals(crossSceneObj.GetComponent<CrossSceneController>().startBoardName))
        {
            InMotion = !InMotion;
            StartCoroutine(Timer(other.name));
        }
    }

    public static MotionControl getInstance()
    {
        return INSTANCE;
    }


    private IEnumerator Timer(string boardName)
    {
        yield return new WaitForSeconds(2f);
        GameObject board = levelmap.transform.Find(boardName).gameObject;
        GameObject cube = Cubes.transform.Find("Cube" + boardName.Replace("Board", "")).gameObject;
        Material material = board.GetComponent<MeshRenderer>().material;
        Color color = new Color();
        ColorUtility.TryParseHtmlString("#D8B24BFF", out color);
        material.color = color;
        material.shader = shader;
        material = cube.GetComponent<MeshRenderer>().material;
        material.color = color;
        material.shader = shader;
        if(!boardName.Equals(crossSceneObj.GetComponent<CrossSceneController>().startBoardName))
            GetComponent<fadeScreen>().fadeIn();
        yield return new WaitForSeconds(2f);
        if (!boardName.Equals(crossSceneObj.GetComponent<CrossSceneController>().startBoardName))
            Application.LoadLevel("balloon"+ (Convert.ToInt32(boardName.Replace("Board",""))-1).ToString());

        flag++;
        InMotion = !InMotion;
    }
}
