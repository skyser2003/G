using System;
using System.Collections.Generic;
using UnityEngine;

class AttackManagerObject : MonoBehaviour
{
    private float currentTime;
    private GameObject owner;

    public AttackInfo info;
    public bool[] attacked;

    public void Init(GameObject owner, AttackInfo info)
    {
        this.info = info.Clone();
        this.owner = owner;
        attacked = new bool[info.frames.Length];

        for(int i=0;i<attacked.Length;++i)
        {
            attacked[i] = false;
        }
    }

    private void Update()
    {
        if (info == null)
        {
            return;
        }

        if (owner != null)
        {
            GetComponent<Transform>().position = owner.GetComponent<Transform>().position;
        }

        currentTime += Time.deltaTime;

        for (int i = 0; i < info.frames.Length; ++i)
        {
            if(attacked[i] == true)
            {
                continue;
            }

            var atk = info.frames[i];
            if(atk.time <= currentTime)
            {
                CreateAttackObject(atk);
                attacked[i] = true;
            }
        }

        // Destroy object if all frames have passed
        for(int i=0;i<attacked.Length;++i)
        {
            if(attacked[i] == false)
            {
                return;
            }
        }

        DestroyObject(gameObject);
    }

    private void CreateAttackObject(AttackFrameInfo frame)
    {
        var obj = UnityEngine.Object.Instantiate(GameObject.Find("AttackSphere"));
        var info = DataManager.Inst.GetAttackInfo("Fireball");

        var atkObj = obj.GetComponent<AttackObject>();

        var atkObjInfo = new AttackObjectInfo();
        atkObjInfo.position.x = owner.GetComponent<Transform>().localPosition.x + frame.position.x;
        atkObjInfo.position.y = owner.GetComponent<Transform>().localPosition.y + frame.position.y;
        atkObjInfo.initialSpeed = info.initialSpeed;
        atkObjInfo.acceleration = info.acceleration;
        atkObjInfo.damage = frame.damage;

        atkObjInfo.targetGroup.Add(typeof(Monster));
        atkObjInfo.ownerID = owner.GetComponent<Unit>().UID;

        atkObj.Init(atkObjInfo);
    }
}