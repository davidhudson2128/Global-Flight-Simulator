using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

using System.Net;

public class PlaneMovementScript : MonoBehaviour
{
    public GameObject earth;
    public GameObject plane;

    public GameObject camera;
    
    public float planeSpeed = .1f;
    int planeScale = 290;
    float translationDistanceEarthToPlane = 26.25f;
    float translationDistanceEarthToCamera = -200f;
    float accuracyThreshold = .7f; 

    Vector3[] originEarthVertices;
    Vector3[] originPlaneVertices;

    float planeOrbitSpeedDegreesPerFrameLon;
    float planeOrbitSpeedDegreesPerFrameLat;

    float rotationAngleEarthToPlaneLon;
    float rotationAngleEarthToPlaneLat;

    Matrix4x4 rotatePlaneMatrixLon;
    Matrix4x4 rotatePlaneMatrixLat;
    Matrix4x4 rotateCameraMatrix;

    Matrix4x4 translatePlaneMatrix;
    Matrix4x4 translateCameraMatrix;

    public Transform PlaneTransform;

    float startingLon;
    float startingLat;

    float destinationLon;
    float destinationLat;

    // (Lon, Lat)
    List<(float, float)> locationsList = new List<(float, float)>();


    // Start is called before the first frame update
    void Start()
    {                     
        //                        W+ E-         N+ S-
        //                      Longitude     Latitude
        locationsList.Add(((float)71.06, (float)42.36)); // Boston
        locationsList.Add(((float).13, (float)51.51)); // London
        // locationsList.Add(((float)71.06, (float)42.36)); //Boston
        // locationsList.Add(((float)-55.27, (float)25.2)); // Dubai
        // locationsList.Add(((float)-103.82, (float)1.35)); // Singapore
        // locationsList.Add(((float)-101.69, (float)3.14)); // Kuala Lumpur
        // locationsList.Add(((float)-55.27, (float)25.2)); // Dubai
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston



        startingLon = locationsList[0].Item1;
        startingLat = locationsList[0].Item2;
        locationsList.RemoveAt(0);
        destinationLon = locationsList[0].Item1;
        destinationLat = locationsList[0].Item2;
        locationsList.RemoveAt(0);


        earth = GameObject.Find("Earth");
        plane = GameObject.Find("Boeing");
        camera = GameObject.Find("Main Camera");

        SetDepartingVariables();


        // Plane Translation Matrix
        translatePlaneMatrix = Matrix4x4.Translate(new Vector3(translationDistanceEarthToPlane,0,0));
        
        
    }

    void SetDepartingVariables(){
        rotationAngleEarthToPlaneLon = startingLon;
        rotationAngleEarthToPlaneLat = startingLat;
        float changeLon = Math.Abs(destinationLon - startingLon);
        float changeLat = Math.Abs(destinationLat - startingLat);


        if (startingLon>0 & destinationLon < 0){
            if ((180-startingLon)+(180+destinationLon) < changeLon){
                changeLon = (180-startingLon)+(180+destinationLon);
                changeLon = changeLon * -1;
            }
        }
        if (startingLon<0 & destinationLon > 0){   
            if ((180+startingLon)+(180-destinationLon) < changeLon){
                changeLon = (180+startingLon)+(180-destinationLon);
                changeLon = changeLon * -1;
                
            }
        }
        

        planeOrbitSpeedDegreesPerFrameLon = (float)(changeLon/(Math.Sqrt((changeLat*changeLat)+(changeLon*changeLon)))) * planeSpeed;
        planeOrbitSpeedDegreesPerFrameLat = (float)(changeLat/(Math.Sqrt((changeLat*changeLat)+(changeLon*changeLon)))) * planeSpeed;
        
        SetPlaneOrbitSpeedAngles();
        
    }

    void SetPlaneOrbitSpeedAngles()
    {
        

        if((startingLat>=destinationLat)&(startingLon>=destinationLon)){
            planeOrbitSpeedDegreesPerFrameLat = planeOrbitSpeedDegreesPerFrameLat * -1;
            planeOrbitSpeedDegreesPerFrameLon = planeOrbitSpeedDegreesPerFrameLon * -1;
        }
        if((startingLat>=destinationLat)&(startingLon<destinationLon)){
            planeOrbitSpeedDegreesPerFrameLat = planeOrbitSpeedDegreesPerFrameLat * -1;
        }
        if((startingLat<destinationLat)&(startingLon>=destinationLon)){
            planeOrbitSpeedDegreesPerFrameLon = planeOrbitSpeedDegreesPerFrameLon * -1;
        }
        if((startingLat<destinationLat)&(startingLon<destinationLon)){
        }

    }


    float getLongitudeFromPoint(Vector3 point){

        float longitude;

        double x = Convert.ToDouble(point.x);
        double y = Convert.ToDouble(point.y);
        double z = Convert.ToDouble(point.z);

        longitude = (float)((Math.Atan(z/x)) * 180/Math.PI);

        if(x <= 0){
            return 180-longitude;
        }
        return -longitude;
    }

    float getLatitudeFromPoint(Vector3 point){

        float latitude;

        double x = Convert.ToDouble(point.x);
        double y = Convert.ToDouble(point.y);
        double z = Convert.ToDouble(point.z);
        

        double hypotenuse = Math.Sqrt((x*x) + (y*y) + (z*z));
 
        latitude = (float)((Math.Asin(y/hypotenuse))*(180/Math.PI));

        return latitude;

    }

    void DetectClicks(){

        if(Input.GetMouseButtonDown(0)){

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit))
            {
                float longitude = getLongitudeFromPoint(hit.point);
                

                float latitude = getLatitudeFromPoint(hit.point);

                locationsList.Add((longitude, latitude));

            }
        }
    }

    void Update()
    {
        if(rotationAngleEarthToPlaneLon < -90){
            rotationAngleEarthToPlaneLon = rotationAngleEarthToPlaneLon + 360;
        }
        if(rotationAngleEarthToPlaneLon >= 270){
            rotationAngleEarthToPlaneLon = rotationAngleEarthToPlaneLon - 360;
        }

        TranslatePlaneToOrigin();

        DetectClicks();

        // Create Matrices
        rotatePlaneMatrixLon = Matrix4x4.Rotate(Quaternion.Euler(0, rotationAngleEarthToPlaneLon, rotationAngleEarthToPlaneLat));
        rotatePlaneMatrixLat = Matrix4x4.Rotate(Quaternion.Euler(0, 0, rotationAngleEarthToPlaneLat));
      
        Quaternion myRotation = Quaternion.identity;
        myRotation.eulerAngles = new Vector3(0, rotationAngleEarthToPlaneLon, rotationAngleEarthToPlaneLat);

        if((Math.Abs(rotationAngleEarthToPlaneLat - destinationLat) > accuracyThreshold) || (Math.Abs(rotationAngleEarthToPlaneLon - destinationLon) > accuracyThreshold)){

            if(Math.Abs(rotationAngleEarthToPlaneLat - destinationLat) >= accuracyThreshold){
                rotationAngleEarthToPlaneLat += planeOrbitSpeedDegreesPerFrameLat * .1f;
            }
            if(Math.Abs(rotationAngleEarthToPlaneLon - destinationLon) >= accuracyThreshold){
                rotationAngleEarthToPlaneLon += planeOrbitSpeedDegreesPerFrameLon * .1f;
            }
            
        }else
        {
            if (locationsList.Count > 0)
            {   
                startingLon = destinationLon;
                startingLat = destinationLat;
                destinationLon = locationsList[0].Item1;
                destinationLat = locationsList[0].Item2;
                locationsList.RemoveAt(0);
                SetDepartingVariables();

            }
        }

        TranslatePlane();
        TranslateCameraToOrigin();
        MoveCamera();
        
    }

    void MoveCamera(){
        
        rotateCameraMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, rotationAngleEarthToPlaneLon, rotationAngleEarthToPlaneLat/2));
        translateCameraMatrix = Matrix4x4.Translate(new Vector3(-(translationDistanceEarthToCamera-200),0,0));
        camera.transform.position = (rotateCameraMatrix*translateCameraMatrix).MultiplyPoint3x4(camera.transform.position);
        camera.transform.LookAt(earth.transform);

    }

    void TranslateCameraToOrigin(){
        camera.transform.eulerAngles = new Vector3(0,0,0);
        camera.transform.position = new Vector3(0,0,0);
    }

    void TranslatePlaneToOrigin()
    {
        plane.transform.position = new Vector3(0,0,0);
    }

    void TranslatePlane()
    {
        Matrix4x4 scalePlaneMatrix = Matrix4x4.Scale(new Vector3(planeScale,planeScale,planeScale));

        Matrix4x4 planeSelfRotationMatrix = Matrix4x4.Rotate(Quaternion.Euler(rotationAngleEarthToPlaneLon, rotationAngleEarthToPlaneLon, rotationAngleEarthToPlaneLon));

        plane.transform.position = ((((rotatePlaneMatrixLon) * translatePlaneMatrix)*scalePlaneMatrix)*planeSelfRotationMatrix).MultiplyPoint3x4(plane.transform.position);
        plane.transform.rotation = Quaternion.Euler(0, rotationAngleEarthToPlaneLon, rotationAngleEarthToPlaneLat);
        Vector3 tempPosition = plane.transform.position;
        plane.transform.position = new Vector3(0,0,0);

        double planeHeadingAngle;
        if(planeOrbitSpeedDegreesPerFrameLon <= 0)
        {
            if(planeOrbitSpeedDegreesPerFrameLat <= 0)
            {

                float lonProportion = (float)planeOrbitSpeedDegreesPerFrameLon/(planeOrbitSpeedDegreesPerFrameLon + planeOrbitSpeedDegreesPerFrameLat);
                planeHeadingAngle = 360 - lonProportion * 90;

            }else
            {
                float lonProportion = (float)Math.Abs(planeOrbitSpeedDegreesPerFrameLon)/(Math.Abs(planeOrbitSpeedDegreesPerFrameLon) + Math.Abs(planeOrbitSpeedDegreesPerFrameLat));
                planeHeadingAngle = 180 + lonProportion * 90;
            }

        }
        else{
            if(planeOrbitSpeedDegreesPerFrameLat <= 0)
            {
                float lonProportion = (float)Math.Abs(planeOrbitSpeedDegreesPerFrameLon)/(Math.Abs(planeOrbitSpeedDegreesPerFrameLon) + Math.Abs(planeOrbitSpeedDegreesPerFrameLat));
                planeHeadingAngle = 0 + lonProportion * 90;

            }else
            {
                float lonProportion = (float)Math.Abs(planeOrbitSpeedDegreesPerFrameLon)/(Math.Abs(planeOrbitSpeedDegreesPerFrameLon) + Math.Abs(planeOrbitSpeedDegreesPerFrameLat));
                planeHeadingAngle = 180 - lonProportion * 90;
            }
            
        }

        // Plane Heading Angle
        //
        //       N(180°)
        //  W(90°)     E(270°)
        //        S(0°)

        plane.transform.Rotate(new Vector3((float)planeHeadingAngle,0,270));
        plane.transform.position = tempPosition;

    }
}
