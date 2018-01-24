using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowScript : MonoBehaviour {
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;

    public float minimumX = -90F;
    public float maximumX = 60F;

    public float minimumY = 5F;
    public float maximumY = 5F;

    float rotationY = 0F;
    public bool onHold;
    private GameObject bow, arrow;
    public float smoothing, sensitivity;
    public GameObject Arrow;
    public float arrowSpeed;
    public float totalArrowCount;
    private CanvasController canvasController;
    private Touch touch;
	// Use this for initialization
	void Start () {
        bow = GameObject.Find("Bow");
        arrow = GameObject.Find("Bow/Elven Long Bow Arrow");
        onHold = false;
        totalArrowCount = 5;
        canvasController = GameObject.Find("Canvas").GetComponent<CanvasController>();
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount>0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            if (Input.touchCount > 0)
            { touch = Input.GetTouch(0); }
                onHold = true;
                //bow.GetComponent<Renderer>().material.color = Color.yellow;
                if (GameBehavior.getInstance().inputActive)
                    Camera.main.transform.position += Camera.main.transform.forward * 0.1f;       
        }

        if (Input.GetMouseButtonUp(0) || touch.phase == TouchPhase.Ended)
        {
           if (GameBehavior.getInstance().inputActive)
            
            {
                onHold = false;
                GameBehavior.getInstance().takeScore = true;
                GameBehavior.getInstance().inputActive = false;
                //bow.GetComponent<Renderer>().material.color = Color.blue;
                Camera.main.transform.position -= Camera.main.transform.forward * 0.1f;
                shootArrow();
            }
        }

        if (onHold && GameBehavior.getInstance().inputActive)
        {
            GameBehavior.getInstance().audioController.playBowSound();
        }
        else
        {
            GameBehavior.getInstance().audioController.stopBowSound();
        }

	}

    private void LateUpdate()
    {
        if (GameBehavior.getInstance().inputActive && onHold)
            BowMotion();
    }

    void BowMotion()
    {
        
        Vector3 mouseMovement = new Vector3(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"), 0f);
        mouseMovement = Vector3.Scale(mouseMovement, new Vector3(smoothing * sensitivity, smoothing * sensitivity, 1f));

        mouseMovement.x = Mathf.Clamp(mouseMovement.x, -15f,15f);
        float mouseMovementY = Mathf.Clamp(mouseMovement.y, -2f, 5f);
        bow.transform.rotation *= Quaternion.AngleAxis(mouseMovement.x, Vector3.up);
        Camera.main.transform.rotation *= Quaternion.AngleAxis(mouseMovement.x, Vector3.up);
        
        bow.transform.rotation *= Quaternion.AngleAxis(mouseMovementY, Vector3.right);
        Camera.main.transform.rotation *= Quaternion.AngleAxis(mouseMovementY * 0.1f, Vector3.right);
        
        /*if (axes == RotationAxes.MouseXAndY)
        {
            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;

            rotationX = Mathf.Clamp(rotationX, minimumX, maximumX);
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
            Camera.main.transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
        else if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
        }
        else
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
            Camera.main.transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
        }*/
    }

    void shootArrow()
    {
        totalArrowCount -= 1;
        canvasController.ReduceArrow();
        Vector3 arrowPosition = arrow.transform.position;
        GameObject arrowObj = Instantiate(Arrow, arrowPosition, bow.transform.rotation);
        arrowObj.name = "Arrow";
        if(arrowObj.GetComponent<Rigidbody>())
        {
            arrowObj.GetComponent<Rigidbody>().velocity = arrowObj.transform.forward * arrowSpeed;
        }        
    }

   
}
