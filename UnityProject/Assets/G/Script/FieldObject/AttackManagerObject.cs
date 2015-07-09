using System;
using System.Collections.Generic;
using UnityEngine;

class AttackManagerObject : MonoBehaviour
{
    private float currentTime;
    private GameObject owner;
    private float damageMultiplier;

    public AttackPatternDataRow info;
    public bool[] attacked;

    public void Init(GameObject owner, AttackPatternDataRow info, float damageMultiplier)
    {
        this.info = info;
        this.owner = owner;
        this.damageMultiplier = damageMultiplier;
        attacked = new bool[info.AttackObjectTimeList.Count];

        for (int i = 0; i < attacked.Length; ++i)
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

        for (int i = 0; i < info.AttackObjectTimeList.Count; ++i)
        {
            if (attacked[i] == true)
            {
                continue;
            }

            var atk = info.AttackObjectDataList[i];
            if (info.TotalTime * info.AttackObjectTimeList[i] <= currentTime)
            {
                CreateAttackObject(atk, (int)(owner.GetComponent<Unit>().Info.Attack * damageMultiplier));
                attacked[i] = true;
            }
        }

        // Destroy object if all frames have passed
        for (int i = 0; i < attacked.Length; ++i)
        {
            if (attacked[i] == false)
            {
                return;
            }
        }

        DestroyObject(gameObject);
    }

    private void CreateAttackObject(string atkObjectName, int baseDamage)
    {
        var obj = UnityEngine.Object.Instantiate(GameObject.Find("AttackSphere"));
        var info = DataManager.Inst.GetAttackObject(atkObjectName);

        var atkObj = obj.GetComponent<AttackObject>();
        atkObj.Init(baseDamage, info, owner.GetComponent<Transform>().position, owner.GetComponent<WalkObject>().Direction);
    }
}