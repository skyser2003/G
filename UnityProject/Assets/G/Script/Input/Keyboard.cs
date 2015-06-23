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

        // Key down
        if (Input.GetKeyDown("space"))
        {
            pc.Jump();
        }

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

        if(Input.GetKeyDown("z"))
        {
            pc.Attack();
        }
    }
}