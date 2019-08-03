using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformer_8 : MonoBehaviour
{

    public float speed;
    Vector3 pointA = new Vector3(9.9f, 16.8f, 0.6f);
    Vector3 pointB = new Vector3(14.22f, 16.8f, 0.6f);

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);
        transform.position = Vector3.Lerp(pointA, pointB, Mathf.PingPong(Time.time, 1));
    }
}
