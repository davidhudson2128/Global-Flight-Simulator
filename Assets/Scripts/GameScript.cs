using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Net;

public class GameScript : MonoBehaviour
{

    public GameObject plane;

    // Start is called before the first frame update
    void Start()
    {
        
        plane = GameObject.Find("Boeing");
    }

    // Update is called once per frame
    void Update()
    {
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
}
