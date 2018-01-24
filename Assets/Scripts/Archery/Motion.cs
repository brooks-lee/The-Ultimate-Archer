using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motion : MonoBehaviour {
    // Use this for initialization
    public static Motion INSTANCE;
    public bool InMotion;
    private Vector3 startPosition, targetPosition;
    private GameObject start, target, crossSceneObj, levelmap, Cubes;
    private Shader shader;
    void Start () {
        INSTANCE = this;
        crossSceneObj = GameObject.Find("CrossSceneObject");
        levelmap = GameObject.Find("LevelMap");
        start = levelmap.transform.Find(crossSceneObj.GetComponent<CrossSceneController>().startBoardName).gameObject;
        startPosition = start.transform.position;
        transform.position = start.transform.Find("Position" + start.name.Replace("Board", "")).transform.position;
        target = levelmap.transform.Find("Board" + (Convert.ToInt32(start.name.Replace("Board", "0")) + 1).ToString()).gameObject;
        targetPosition = target.transform.position;
        Cubes = levelmap.transform.Find("Cubes").gameObject;

        InMotion = true;
        shader = Shader.Find("MK/Glow/Selective/Self-Illumin/Diffuse");
    }
	
	// Update is called once per frame
	void Update () {
        if (InMotion)
        {
            transform.LookAt(target.transform);
             transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Board"))
        {
            if (Convert.ToInt32(other.name.Replace("Board", "0")) < 5)
            {
                string startName;
                if (!start)
                    startName = "Board";
                else
                    startName = start.name;
                if (other.name.Contains("Board") && !other.name.Equals(startName))
                {
                    InMotion = !InMotion;
                    //crossSceneObj.GetComponent<CrossSceneController>().startBoardName = other.name;
                    StartCoroutine(Timer(other.name));
                }
            }
        }
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
        if (!boardName.Equals(crossSceneObj.GetComponent<CrossSceneController>().startBoardName))
            GetComponent<fadeScreen>().fadeIn();
        yield return new WaitForSeconds(2f);
        if (!boardName.Equals(crossSceneObj.GetComponent<CrossSceneController>().startBoardName))
        {
            //crossSceneObj.GetComponent<CrossSceneController>().startBoardName = target.name;
            Application.LoadLevel("balloon2");// + (Convert.ToInt32(boardName.Replace("Board", "")) - 1).ToString());
        }
        
        InMotion = !InMotion;
    }

    public static Motion getInstance()
    {
        return INSTANCE;
    }
}
