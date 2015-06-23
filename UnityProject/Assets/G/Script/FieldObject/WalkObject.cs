using UnityEngine;
using System;

class WalkObject : MonoBehaviour
{
    private int direction;

    private GravityObject gvObject;
    private Unit unitObject;

    public float Velocity
    {
        get
        {
            return direction * unitObject.Physics.maxMoveSpeed;
        }
    }

    public float Speed { get { return unitObject.Physics.maxMoveSpeed; } }
    public int Direction
    {
        get
        {
            return direction;
        }
        set
        {
            direction = value;
        }
    }

    private void Start()
    {
        gvObject = GetComponent<GravityObject>();
        unitObject = GetComponent<Unit>();
    }

    private void Update()
    {
        var dx = Velocity * Time.deltaTime;
        transform.localPosition += new Vector3(dx, 0, 0);
    }
}