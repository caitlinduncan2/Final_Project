using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformer : MonoBehaviour
{


    public float speed;
    Vector3 pointA = new Vector3(3.5f, -5.62f, 0.6f);
    Vector3 pointB = new Vector3(12.5f, -5.62f, 0.6f);



    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);
        transform.position = Vector3.Lerp(pointA, pointB, Mathf.PingPong(Time.time, 1));
    }
}
