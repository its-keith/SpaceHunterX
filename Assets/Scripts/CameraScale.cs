using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//testcomment
public class CameraScale : MonoBehaviour
{

    public static float pixelsToUnits = 1f;
    public static float scale = 1f;

    //The x values of the edge of the game area
    public static float rightEdge = 900;
    public static float leftEdge = -900;

    public Vector2 nativeResolution = new Vector2(1920, 1080);
    
    void Awake()
    {
        var camera = GetComponent<Camera>();

        if (camera.orthographic)
        {
            scale = Screen.height / nativeResolution.y;
            pixelsToUnits *= scale;
            //camera.orthographicSize = (Screen.height / 2.0f) / pixelsToUnits;
        }
    }
}
