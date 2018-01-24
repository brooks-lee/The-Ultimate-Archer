using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {
    public GameObject balloonsparent;
    private GameObject WindScript;
    bool flyStart;
    // Use this for initialization
    void Start () {
        flyStart = false;
        WindScript = GameObject.Find("WindScript");
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            flyStart = !flyStart;
            if (flyStart)
                StartCoroutine(BalloonFly());
        }
    }

    private IEnumerator BalloonFly()
    {
        int i = 0;
        if (flyStart)
        {
            int rnd = Random.Range(0, balloonsparent.transform.childCount);
            Debug.Log(rnd + " is the selected balloon");
            yield return new WaitForSeconds(5f);

            GameObject selectedObject = balloonsparent.transform.GetChild(rnd).gameObject;
            if (!selectedObject.transform.Find("balloon main").GetComponent<Rigidbody>())
            {
                selectedObject.transform.Find("balloon main").gameObject.AddComponent<Rigidbody>();
                selectedObject.transform.Find("balloon main").GetComponent<Rigidbody>().useGravity = false;
            }
            Material m = Resources.Load<Material>("Materials/m4");
            selectedObject.transform.Find("balloon main").GetComponent<Renderer>().material = m;
            selectedObject.transform.Find("balloon main").GetComponent<Rigidbody>().velocity = Vector3.up * 0.5f;
            //VertexWind testScript = WindScript.transform.GetComponent<VertexWind>();
            List<Component> components = new List<Component>();
            components = new List<Component>(WindScript.transform.GetComponents<Component>());

            foreach (Component comp in components)
                Debug.Log(comp);
            /*if (testScript != null)
            {
                Debug.Log("testScript not null and adding object to windObject Script");
                testScript.addWindObject(selectedObject.transform.Find("balloon main").gameObject);
            }
            else
            {
                Debug.Log("testScript is null");
                Debug.Log("WindScript found " + WindScript);
            }*/
        }
    }
}
