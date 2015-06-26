using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Keyboard : MonoBehaviour
{
    private void Update()
    {
        var pc = GetComponent<PlayerCharacter>();
        if (pc == null)
        {
            return;
        }

        var unit = GetComponent<Unit>();

        // Jump
        if (Input.GetKeyDown("space"))
        {
            unit.Jump();
        }

        if(Input.GetKey("space") && unit.IsJumping == true)
        {
            unit.ContinueJump(Time.deltaTime);
        }

        // Move
        if (Input.GetKey("left"))
        {
            pc.MoveLeft();
        }
        else if (Input.GetKey("right"))
        {
            pc.MoveRight();
        }
        else
        {
            pc.Stop();
        }

        // Attack
        if(Input.GetKeyDown("z"))
        {
            pc.Attack();
        }
    }
}