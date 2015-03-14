using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class BossCharacter : MonoBehaviour
{
    private double hp = 1000000;

    private void Start()
    {

    }

    public void ReceiveDamage(double damage)
    {
        hp -= damage;
    }
}