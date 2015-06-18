using System;
using UnityEngine;

class WalkObject : MonoBehaviour
{
    public float Acceleration { get; set; }
    public float Velocity { get; set; }
    public float Speed { get { return Math.Abs(Velocity); } }
    public int Direction { get { return Velocity < 0 ? -1 : 1; } }
    public float MaxSpeed { get; set; }

    private void Upate()
    {
        var ds = Acceleration * Time.deltaTime;
        Velocity = Math.Max(MaxSpeed, Velocity + ds);

        var dx = Velocity * Time.deltaTime;
        transform.localPosition += new Vector3(dx, 0, 0);
    }
}