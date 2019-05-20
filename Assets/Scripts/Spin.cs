using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float x = 0.1f;
    public float y = 0.2f;
    public float z = 0f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(x, y, z));
    }
}
