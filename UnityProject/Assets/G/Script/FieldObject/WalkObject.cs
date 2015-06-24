using UnityEngine;
using System;

class WalkObject : MonoBehaviour
{
    private int direction;

    private GravityObject gvObject;
    private Unit unit;

    private float speed;

    public float Velocity
    {
        get
        {
            return direction * unit.Physics.maxMoveSpeed;
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
                speed = unit.Physics.maxMoveSpeed;
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
        unit = GetComponent<Unit>();
        speed = 0.0f;
    }

    private void Update()
    {
        var dx = Velocity * Time.deltaTime;
        transform.localPosition += new Vector3(dx, 0, 0);
    }
}