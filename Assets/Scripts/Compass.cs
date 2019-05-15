using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{

    public Vector3 north;
    public Transform player;
    public Quaternion missionDirection;

    public RectTransform northLayer;
    public RectTransform missionLayer;

    public Transform missionPlace;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangeNorthDirection();
        ChangeMissionDirection();
    }

    void ChangeNorthDirection() {
        north.z = player.eulerAngles.y;
        northLayer.localEulerAngles = north;

    }

    void ChangeMissionDirection() {
        Vector3 dir = player.position - missionPlace.position;
        missionDirection = Quaternion.LookRotation(-dir);

        missionDirection.z = -missionDirection.y;
        missionDirection.x = 0;
        missionDirection.y = 0;

        missionLayer.localRotation = missionDirection * Quaternion.Euler(north);
    }
}
