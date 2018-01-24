using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeScreen : MonoBehaviour {
    private Texture2D blackTexture;
    bool fadein = false, fadeout = false;
    int direction; float alpha;
     SpriteRenderer screenRenderer;
    // Use this for initialization
    void Start () {
        blackTexture = Resources.Load<Texture2D>("Images/Black Square");
        alpha = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            direction = 1;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            direction = -1;
        }
        if (fadein)
        {
            
        }

        if(fadeout)
        {
            
        }
    }

    public void fadeIn()
    {
        direction = -1;
    }

    public void fadeOut()
    {
        direction = 1;
    }

    private void OnGUI()
    {
        alpha -= Time.deltaTime * 0.2f * direction;
        alpha = Mathf.Clamp01(alpha);

        GUI.color = new Color(GUI.color.r, GUI.color.b, GUI.color.g, alpha);
        GUI.depth = -1000;
        GUI.DrawTexture(new Rect(0f,0f,Screen.width, Screen.height), blackTexture);
    }
}
