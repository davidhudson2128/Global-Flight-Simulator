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
    {   //                        W+ E-         N+ S-
        //                      Longitude     Latitude
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float).13, (float)51.51)); // London
        // locationsList.Add(((float)71.06, (float)42.36)); //Boston
        // locationsList.Add(((float)-55.27, (float)25.2)); // Dubai
        // locationsList.Add(((float)-103.82, (float)1.35)); // Singapore
        // locationsList.Add(((float)-101.69, (float)3.14)); // Kuala Lumpur
        // locationsList.Add(((float)-55.27, (float)25.2)); // Dubai
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston

        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float).13, (float)51.51)); // London
        // locationsList.Add(((float)71.06, (float)42.36)); //Boston
        // locationsList.Add(((float)-55.27, (float)25.2)); // Dubai
        // locationsList.Add(((float)-103.82, (float)1.35)); // Singapore
        // locationsList.Add(((float)-101.69, (float)3.14)); // Kuala Lumpur
        // locationsList.Add(((float)-55.27, (float)25.2)); // Dubai
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston

        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float).13, (float)51.51)); // London
        // locationsList.Add(((float)71.06, (float)42.36)); //Boston
        // locationsList.Add(((float)-55.27, (float)25.2)); // Dubai
        // locationsList.Add(((float)-103.82, (float)1.35)); // Singapore
        // locationsList.Add(((float)-101.69, (float)3.14)); // Kuala Lumpur
        // locationsList.Add(((float)-55.27, (float)25.2)); // Dubai
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston

        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float).13, (float)51.51)); // London
        // locationsList.Add(((float)71.06, (float)42.36)); //Boston
        // locationsList.Add(((float)-55.27, (float)25.2)); // Dubai
        // locationsList.Add(((float)-103.82, (float)1.35)); // Singapore
        // locationsList.Add(((float)-101.69, (float)3.14)); // Kuala Lumpur
        // locationsList.Add(((float)-55.27, (float)25.2)); // Dubai
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston

        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float).13, (float)51.51)); // London
        // locationsList.Add(((float)71.06, (float)42.36)); //Boston
        // locationsList.Add(((float)-55.27, (float)25.2)); // Dubai
        // locationsList.Add(((float)-103.82, (float)1.35)); // Singapore
        // locationsList.Add(((float)-101.69, (float)3.14)); // Kuala Lumpur
        // locationsList.Add(((float)-55.27, (float)25.2)); // Dubai
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston

        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float).13, (float)51.51)); // London
        // locationsList.Add(((float)71.06, (float)42.36)); //Boston
        // locationsList.Add(((float)-55.27, (float)25.2)); // Dubai
        // locationsList.Add(((float)-103.82, (float)1.35)); // Singapore
        // locationsList.Add(((float)-101.69, (float)3.14)); // Kuala Lumpur
        // locationsList.Add(((float)-55.27, (float)25.2)); // Dubai
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston

        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float).13, (float)51.51)); // London
        // locationsList.Add(((float)71.06, (float)42.36)); //Boston
        // locationsList.Add(((float)-55.27, (float)25.2)); // Dubai
        // locationsList.Add(((float)-103.82, (float)1.35)); // Singapore
        // locationsList.Add(((float)-101.69, (float)3.14)); // Kuala Lumpur
        // locationsList.Add(((float)-55.27, (float)25.2)); // Dubai
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston

        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float).13, (float)51.51)); // London
        // locationsList.Add(((float)71.06, (float)42.36)); //Boston
        // locationsList.Add(((float)-55.27, (float)25.2)); // Dubai
        // locationsList.Add(((float)-103.82, (float)1.35)); // Singapore
        // locationsList.Add(((float)-101.69, (float)3.14)); // Kuala Lumpur
        // locationsList.Add(((float)-55.27, (float)25.2)); // Dubai
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston



        // string strUrlTest = String.Format("https://jsonplaceholder.typicode.net");
        // WebRequest requestObjGet = WebRequest.Create(strUrlTest);
        // WebResponse responseObjGet = null;
        // responseObjGet = requestObjGet.GetResponse();


// Andrew
//                              W+ E-         N+ S-
//                            Longitude     Latitude
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        locationsList.Add(((float).52, (float)51.51)); // England
        locationsList.Add(((float).52, (float)31.51)); // England
        // locationsList.Add(((float).52, (float)21.51)); // England
        // locationsList.Add(((float).52, (float)-51.51)); // England
        // locationsList.Add(((float).52, (float)51.51)); // England
        // locationsList.Add(((float)25.52, (float)51.51)); // England
        // locationsList.Add(((float).52, (float)51.51)); // England
        // locationsList.Add(((float).52, (float)51.51)); // England
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float).12, (float)51.51)); // England
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float).12, (float)51.51)); // England
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float).12, (float)51.51)); // England
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float).12, (float)51.51)); // England
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float)-4.84, (float)45.76)); // France
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float).12, (float)51.51)); // England
        // locationsList.Add(((float)-8.68, (float)50.11)); // Germany
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float)81.57, (float)28.38)); // Disney
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float).12, (float)51.51)); // England
        // locationsList.Add(((float).12, (float)51.51)); // Portugal
        // locationsList.Add(((float).12, (float)51.51)); // England
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float).12, (float)51.51)); // England
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float)81.57, (float)28.38)); // Disney
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float).12, (float)51.51)); // England
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float).12, (float)51.51)); // England
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float).12, (float)51.51)); // England
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float).12, (float)51.51)); // England
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float).12, (float)51.51)); // England
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float).12, (float)51.51)); // England
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float).12, (float)51.51)); // England
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float).12, (float)51.51)); // England
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float).12, (float)51.51)); // England
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float)-55.27, (float)25.2)); // Dubai
        // locationsList.Add(((float)-101.69, (float)3.14)); // Kuala Lumpur
        // locationsList.Add(((float)-103.82, (float)1.35)); // Singapore
        // locationsList.Add(((float)-106.85, (float)-6.21)); // Jakarta
        // locationsList.Add(((float)-55.27, (float)25.2)); // Dubai
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float).12, (float)51.51)); // England
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float)73.14, (float)40.79)); // Long Island
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float).12, (float)51.51)); // England
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float)-12.59, (float)55.68)); // Copenhagen
        // locationsList.Add(((float)-4.9, (float)52.37)); // Amsterdam
        // locationsList.Add(((float)-4.35, (float)50.85)); // Brussels
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float)77.04, (float)38.91)); // DC
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float)82.46, (float)27.95)); // Tampa
        // locationsList.Add(((float)75.16, (float)39.98)); // Philidelphia
        // locationsList.Add(((float)82.46, (float)27.95)); // Tampa
        // locationsList.Add(((float)79.02, (float)35.76)); // N Carolina
        // locationsList.Add(((float)82.46, (float)27.95)); // Tampa
        // locationsList.Add(((float)64.89, (float)18.34)); // St Thomas
        // locationsList.Add(((float)82.46, (float)27.95)); // Tampa
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float)83.05, (float)42.33)); // Detroit
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float)82.46, (float)27.95)); // Tampa
        // locationsList.Add(((float)75.16, (float)39.98)); // Philidelphia
        // locationsList.Add(((float)82.46, (float)27.95)); // Tampa
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float)82.46, (float)27.95)); // Tampa
        // locationsList.Add(((float)71.41, (float)41.82)); // Providence
        // locationsList.Add(((float)82.46, (float)27.95)); // Tampa
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float)76.61, (float)39.29)); // Baltimore
        // locationsList.Add(((float)79.38, (float)43.65)); // Toronto
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float)82.46, (float)27.95)); // Tampa
        // locationsList.Add(((float)88.79, (float)43.78)); // Wisconsin
        // locationsList.Add(((float)82.46, (float)27.95)); // Tampa
        // locationsList.Add(((float)90.04, (float)38.56)); // Norte Dame
        // locationsList.Add(((float)82.46, (float)27.95)); // Tampa
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float)82.46, (float)27.95)); // Tampa
        // locationsList.Add(((float)76.94, (float)38.99)); // U Maryland
        // locationsList.Add(((float)82.46, (float)27.95)); // Tampa
        // locationsList.Add(((float)82.46, (float)27.95)); // Virginia Tech
        // locationsList.Add(((float)82.46, (float)27.95)); // Tampa
        // locationsList.Add(((float).12, (float)51.51)); // England
        // locationsList.Add(((float)7.98, (float)31.63)); // Morocco
        // locationsList.Add(((float)-21.01, (float)52.23)); // Poland
        // locationsList.Add(((float).12, (float)51.51)); // England
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float)83.03, (float)40.01)); // Ohio State Univ
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float)89.41, (float)43.08)); // Wisconsin
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float)76.61, (float)39.29)); // Baltimore
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float)82.46, (float)27.95)); // Tampa
        // locationsList.Add(((float)87.63, (float)41.88)); // Chicago
        // locationsList.Add(((float)87.91, (float)43.04)); // Milwaukee
        // locationsList.Add(((float)91.53, (float)41.66)); // Iowa City
        // locationsList.Add(((float)82.46, (float)27.95)); // Tampa
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float)76.94, (float)38.99)); // Maryland
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float)82.46, (float)27.95)); // Tampa
        // locationsList.Add(((float)71.06, (float)42.36)); // Boston
        // locationsList.Add(((float)82.46, (float)27.95)); // Tampa
        // locationsList.Add(((float)76.94, (float)38.99)); // Maryland
        // locationsList.Add(((float)83.05, (float)42.33)); // Michigan
        // locationsList.Add(((float)82.46, (float)27.95)); // Tampa
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
        //originEarthVertices = earth.GetComponent<MeshFilter>().mesh.vertices;
        // originPlaneVertices = plane.GetComponent<MeshFilter>().mesh.vertices;

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
                // Debug.Log("click");

                float longitude = getLongitudeFromPoint(hit.point);
                

                float latitude = getLatitudeFromPoint(hit.point);

                locationsList.Add((longitude, latitude));

            }

        
        

        }
        


    }

    // Update is called once per frame
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
            // Debug.Log("Arrived");
            // Debug.Log(rotationAngleEarthToPlaneLon);
            
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


        // Translate Plane
        TranslatePlane();

        TranslateCameraToOrigin();
        MoveCamera();
        
    }

    void MoveCamera(){
        
        rotateCameraMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, rotationAngleEarthToPlaneLon, rotationAngleEarthToPlaneLat/2));
        // rotateCameraMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 0, 0));
        translateCameraMatrix = Matrix4x4.Translate(new Vector3(-(translationDistanceEarthToCamera-200),0,0));
        camera.transform.position = (rotateCameraMatrix*translateCameraMatrix).MultiplyPoint3x4(camera.transform.position);
        camera.transform.LookAt(earth.transform);

    }

    void TranslateCameraToOrigin(){

        // camera = GameObject.Find("Main Camera");
        camera.transform.eulerAngles = new Vector3(0,0,0);
        camera.transform.position = new Vector3(0,0,0);
    }

    void TranslatePlaneToOrigin()
    {

        plane.transform.position = new Vector3(0,0,0);

    }

    void TranslatePlane()
    {
        // Mesh planeMesh = plane.GetComponent<MeshFilter>().mesh;
        // Vector3[] originPlaneVertices = GameObject.Find("PlaneOrigin").GetComponent<MeshFilter>().mesh.vertices;

        // Vector3[] newPlaneVertices = new Vector3[originPlaneVertices.Length];

        Matrix4x4 scalePlaneMatrix = Matrix4x4.Scale(new Vector3(planeScale,planeScale,planeScale));

        Matrix4x4 planeSelfRotationMatrix = Matrix4x4.Rotate(Quaternion.Euler(rotationAngleEarthToPlaneLon, rotationAngleEarthToPlaneLon, rotationAngleEarthToPlaneLon));
        
        // for (var i = 0; i < originPlaneVertices.Length; i++)
        // {
        //     Vector3 newVert = (((rotatePlaneMatrixLon) * translatePlaneMatrix)*scalePlaneMatrix).MultiplyPoint3x4(originPlaneVertices[i]);
        //     newPlaneVertices[i] = newVert;
        // }
        // planeMesh.vertices = newPlaneVertices;
        // planeMesh.RecalculateNormals();

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


        // Debug.Log((Math.Sqrt(Math.Abs(planeOrbitSpeedDegreesPerFrameLat/planeSpeed)))*(180/Math.PI));

        // Plane Heading Angle
        //
        //       N(180°)
        //  W(90°)     E(270°)
        //        S(0°)
        //

        plane.transform.Rotate(new Vector3((float)planeHeadingAngle,0,270));
        plane.transform.position = tempPosition;

    }
}
