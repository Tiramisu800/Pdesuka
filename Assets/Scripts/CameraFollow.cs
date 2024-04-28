using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float _followSpeed = 2f;
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        Follow();   
    }

    virtual protected void Follow()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, _followSpeed * Time.deltaTime);
    }
}
