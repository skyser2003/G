using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class GravityObject : MonoBehaviour
{
    static private float gravity = -9.8f;
    static private float maxFallSpeed = -10.0f;
    static public float Gravity { get { return gravity; } }

    private bool applyGravity = true;
    private Unit unit;

    public Platform hitPlatform;

    private void Start()
    {
        unit = GetComponent<Unit>();
    }

    private void Update()
    {
        bool oldApplyGravity = applyGravity;
        applyGravity = true;

        var hitList = Physics2D.RaycastAll(transform.position, -Vector2.up, 0.1f);
        if (hitList != null)
        {
            foreach (var hit in hitList)
            {
                var platform = hit.collider.gameObject.GetComponent<Platform>();
                if (platform != null)
                {
                    if (unit.Velocity.y <= 0)
                    {
                        applyGravity = false;
                        hitPlatform = platform;
                        break;
                    }
                }
            }
        }

        if (applyGravity == true)
        {
            hitPlatform = null;
            unit.Velocity.y += gravity * Time.deltaTime;
            unit.Velocity.y = Math.Max(unit.Velocity.y, maxFallSpeed);
        }

        // Stop
        if (oldApplyGravity == true && applyGravity == false)
        {
            unit.IsJumpPossible = true;
            unit.Velocity.y = 0.0f;
        }
        // Begin
        else if (oldApplyGravity == false && applyGravity == true)
        {
            unit.IsJumpPossible = false;
        }
    }
}