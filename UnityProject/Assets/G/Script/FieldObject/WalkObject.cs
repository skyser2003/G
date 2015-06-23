using UnityEngine;
using System;

class WalkObject : MonoBehaviour
{
    private int direction;

    private GravityObject gvObject;
    private Unit unitObject;

    private float speed;

    public float Velocity
    {
        get
        {
            return direction * unitObject.Physics.maxMoveSpeed;
        }
    }

    public float Speed { get { return speed; } }
    public int Direction
    {
        get
        {
            return direction;
        }
        set
        {
            direction = value;
            if(direction != 0)
            {
                speed = unitObject.Physics.maxMoveSpeed;
            }
            else
            {
                speed = 0.0f;
            }
        }
    }

    private void Start()
    {
        gvObject = GetComponent<GravityObject>();
        unitObject = GetComponent<Unit>();
        speed = 0.0f;
    }

    private void Update()
    {
        var dx = Velocity * Time.deltaTime;
        transform.localPosition += new Vector3(dx, 0, 0);
    }
}