using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target;

    public Vector3 diference;

    public float speed = 10;


    // Start is called before the first frame update
    void Start()
    {
        diference = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + diference, Time.deltaTime * speed);
    }
}
