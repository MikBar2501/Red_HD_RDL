using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Radioactive/Stats")]
public class Stats : ScriptableObject
{
    [Space(25)]
    public float movingSpeed = 12.0f;
    public float rotationSpeed = 9.0f;
    public float sprintMultiplayer = 1.5f;


    [Space(25)]
    public float cameraDampTime = 0.2f;                 // Approximate time for the camera to refocus.
    public float cameraScreenEdgeBuffer = 4f;           // Space between the top/bottom most target and the screen edge.
    public float cameraMinSize = 6.5f;                  // The smallest orthographic size the camera can be.

}
