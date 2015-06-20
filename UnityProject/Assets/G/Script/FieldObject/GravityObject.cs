﻿using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class GravityObject : MonoBehaviour
{
    static private float gravity = 0.5f;
    static public float Gravity { get { return gravity; } }

    private bool applyGravity = true;
    public Platform hitPlatform;

    private void Update()
    {
        bool oldApplyGravity = applyGravity;
        applyGravity = true;

        var hitList = Physics2D.RaycastAll(transform.position, -Vector2.up, 0.1f);
        if(hitList != null)
        {
            foreach (var hit in hitList)
            {
                var platform = hit.collider.gameObject.GetComponent<Platform>();
                if (platform != null)
                {
                    applyGravity = false;
                    hitPlatform = platform;
                    break;
                }
            }
        }

        if(applyGravity == true)
        {
            hitPlatform = null;
            GetComponent<Transform>().localPosition += new Vector3(0.0f, -gravity * Time.deltaTime, 0.0f);
        }

        if(oldApplyGravity == true && applyGravity == false)
        {
            SetJumpPossible(true);
        }
        else if(oldApplyGravity == false && applyGravity == true)
        {
            SetJumpPossible(false);
        }
    }

    private void SetJumpPossible(bool isPossible)
    {
        var pc = GetComponent<PlayerCharacter>();
        if(pc != null)
        {
            pc.SetJumpPossible(isPossible);
        }
    }
}