using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float speed;
    Vector3 initialPos;
    Vector3 direction;
    Vector3 finalPos;

    private void Awake()
    {
        initialPos = transform.position;
        direction = Vector3.zero - transform.position;
        direction.Normalize();
        finalPos = Vector3.zero + (2 * direction);
    }

    void FixedUpdate()
    {
        transform.Translate(direction * Time.deltaTime * speed);
        if (transform.position.magnitude < finalPos.magnitude || transform.position.magnitude > initialPos.magnitude) //Closer than the final pos, negate the direction
        {
            direction *= -1;
        }
        //transform.position = new Vector3(initialPos - (scale * Mathf.PingPong(Mathf.Sin(Time.time),dist)), transform.position.y, transform.position.z);
    }
    public Vector3 GetDirectionNormalized(Vector3 other)
    {
        Vector3 vec;
        vec = transform.position - other;
        vec.Normalize();
        return vec;
    }

    internal void SetDirection(Vector3 dir)
    {
        direction = dir;
    }
}
