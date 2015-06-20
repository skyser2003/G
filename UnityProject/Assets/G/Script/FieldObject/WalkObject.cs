using UnityEngine;
using System;

class WalkObject : MonoBehaviour
{
    private float speed;
    private int direction;

    private GravityObject gvObject;

    public float Velocity {
        get
        {
            return direction * speed;
        }
        set
        {
            direction = Math.Sign(value);
            speed = Math.Abs(value);
        }
    }

    public float Speed { get { return speed; } }
    public int Direction { get { return direction; } }

    private void Start()
    {
        gvObject = GetComponent<GravityObject>();
    }

    private void Update()
    {
        var dx = Velocity * Time.deltaTime;
        transform.localPosition += new Vector3(dx, 0, 0);
    }
}