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
            return (float)(direction * speed);
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
            speed = (float)unit.Info.MaxMovementSpeed;
        }
    }

    public void Stop()
    {
        speed = 0.0f;
    }

    private void Start()
    {
        gvObject = GetComponent<GravityObject>();
        unit = GetComponent<Unit>();
        direction = Math.Abs(GetComponent<Transform>().rotation.eulerAngles.y - 90.0f) <= 0.001f ? 1 : -1;
        speed = 0.0f;
    }

    private void Update()
    {
        var dx = Velocity * Time.deltaTime;
        transform.localPosition += new Vector3(dx, 0, 0);

        var pc = GetComponent<PlayerCharacter>();
        if (pc != null)
        {
            if (pc.WalkVelocity < 0)
            {
                pc.GetComponent<Transform>().rotation = Quaternion.Euler(0, -90, 0);
            }
            else if (pc.WalkVelocity > 0)
            {
                pc.GetComponent<Transform>().rotation = Quaternion.Euler(0, 90, 0);
            }

            if (pc.WalkVelocity != 0)
            {
                var animator = pc.GetComponent<Animator>();
                animator.SetFloat("HorizontalSpeed", pc.Walk.Speed);
            }

            pc.GetComponent<Transform>().localPosition += new Vector3(pc.WalkVelocity * Time.deltaTime, 0.0f, 0.0f);
        }
    }
}