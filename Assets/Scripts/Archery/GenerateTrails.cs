using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTrails : MonoBehaviour {
    private GameObject trailObject, trailParentContainer, trailParent;
    // Use this for initialization
    private void Awake()
    {
    }

    void Start() {
        trailObject = Resources.Load<GameObject>("Prefabs/Sphere");
        trailParentContainer = GameObject.Find("TrailParentContainer");
        DontDestroyOnLoad(trailParentContainer);
        trailParent = trailParentContainer.transform.Find("TrailParent").gameObject;
        trailParent.SetActive(true);
        StartCoroutine(genTrail());
	}
	
	// Update is called once per frame
	void Update () {
	}

    private IEnumerator genTrail()
    {
        if (GetComponent<Motion>().InMotion)
        {
            GameObject go = Instantiate(trailObject, transform.position, Quaternion.identity);
            go.transform.parent = trailParent.transform;
        }
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(genTrail());
        
    }
}
