using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class PlayerCharacter : MonoBehaviour
{
    private float speed = 1.0f;
    private bool isJumping = false;
    private float jumpTime = 0.0f;

    private void Start()
    {

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
        this.speed = speed;
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
        isJumping = true;
        jumpTime = 1.0f;
    }
   
    public float GetSpeed()
    {
        return speed;
    }
}