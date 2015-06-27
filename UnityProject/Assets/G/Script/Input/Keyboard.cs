using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Keyboard : MonoBehaviour
{
    private float strongAttackDelay = 0.0f;
    private float strongAttackChargeTime = 0.0f;

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
            strongAttackDelay = pc.strongAttackDelay;
            strongAttackChargeTime = 0.0f;
        }

        if(Input.GetKey("z"))
        {
            strongAttackDelay -= Time.deltaTime;
            strongAttackChargeTime += Time.deltaTime;

            if(strongAttackDelay <= 0.0f)
            {
                pc.GetComponent<Animator>().SetTrigger("StartStrongAttack");
            }
        }

        if(Input.GetKeyUp("z"))
        {
            if (strongAttackDelay > 0.0f)
            {
                pc.Attack();
                strongAttackChargeTime = 0.0f;
            }
            else
            {
                pc.StrongAttack(strongAttackChargeTime);
                strongAttackDelay = 0.0f;
            }
        }
    }
}