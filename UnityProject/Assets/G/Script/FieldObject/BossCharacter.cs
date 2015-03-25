using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class BossCharacter : MonoBehaviour
{
    public class Phase
    {
        public double hp;
    }

    private List<Phase> phaseList = new List<Phase>();
    private int currentPhase;

    private void Start()
    {
        currentPhase = 0;
    }

    public void ReceiveDamage(double damage)
    {
        phaseList[currentPhase].hp -= damage;
        if(phaseList[currentPhase].hp <= 0.0)
        {
            currentPhase += 1;
            if(phaseList.Count <= currentPhase)
            {
                // End!
            }
        }
    }
}