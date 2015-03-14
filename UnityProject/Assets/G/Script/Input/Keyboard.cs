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

        if (Input.GetKeyDown("left"))
        {
            pc.MoveLeft();
        }
        else if (Input.GetKeyDown("right"))
        {
            pc.MoveRight();
        }

        if(Input.GetKeyDown("z"))
        {
            pc.Attack();
        }
        
        // Key up
        if (Input.GetKeyUp("left") || Input.GetKeyUp("right"))
        {
            pc.Stop();
        }
    }
}