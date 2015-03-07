using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class PlayerCharacter : MonoBehaviour
{
    private Vector3 velocity = new Vector3(1, 0, 0);

    void Update()
    {
        transform.localPosition += velocity * Time.deltaTime;
    }
}