using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class CameraOrientation : MonoBehaviour
{
    public GameObject cameraObject;
    private Camera cameraInstance;
    private ScreenOrientation currentScreenOrientation;
 
    void Start()
    {
        cameraObject = GameObject.Find("Main Camera");
        cameraInstance = cameraObject.GetComponent<Camera>();
        currentScreenOrientation = Screen.orientation;

        if (Screen.orientation == ScreenOrientation.Landscape || Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight)
            {
                cameraInstance.fieldOfView = 8;
            }
        if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown)
            {
                cameraInstance.fieldOfView = 17;
            }
    }
 
    void Update()
    {
        
        if (Screen.orientation != currentScreenOrientation)
        {
            
            currentScreenOrientation = Screen.orientation;
            Debug.Log("Orientation changed!");
 
            if (Screen.orientation == ScreenOrientation.Landscape || Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight)
            {
                Debug.Log("Landscape");
                cameraInstance.fieldOfView = 8;
            }
            if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown)
            {
                Debug.Log("Portrait");
                cameraInstance.fieldOfView = 17;
            }
        }
    }
}