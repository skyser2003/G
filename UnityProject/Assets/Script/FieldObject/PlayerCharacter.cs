using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class PlayerCharacter : MonoBehaviour
{
    private float speed = 0.0f;
    private bool jumpPossible = true;
    private bool isJumping = false;
    private float jumpTime = 0.0f;

    private void Start()
    {
        Stop();
    }

    private void Update()
    {
        var velocity = new Vector3();
        velocity.x = speed;

        if(isJumping == true)
        {
            jumpTime -= Time.deltaTime;
            if(jumpTime <= 0.0f)
            {
                isJumping = false;
            }

            velocity.y = 1.0f;
        }

        var dx = velocity * Time.deltaTime;
        transform.localPosition += new Vector3(dx.x, dx.y, 0.0f);
    }

    private void Move(float speed)
    {
        var skeleton = GetComponent<SkeletonAnimation>();

        this.speed = speed;
        if(speed == 0)
        {
            skeleton.AnimationName = null;
        }
        else if(speed < 0)
        {
            GetComponent<Transform>().localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
            skeleton.AnimationName = "Walk";
        }
        else
        {
            GetComponent<Transform>().localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            skeleton.AnimationName = "Walk";
        }
    }

    public void MoveLeft()
    {
        Move(-1);
    }

    public void MoveRight()
    {
        Move(1);
    }

    public void Stop()
    {
        Move(0);
    }

    public void Jump()
    {
        if(jumpPossible == false)
        {
            return;
        }

        isJumping = true;
        jumpTime = 1.0f;
    }
   
    public float GetSpeed()
    {
        return speed;
    }

    public void SetJumpPossible(bool isPossible)
    {
        jumpPossible = isPossible;
    }
}