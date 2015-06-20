using System.Collections.Generic;
using UnityEngine;

class MonsterManager : MonoBehaviour
{
    static public MonsterManager Inst { get; private set; }

    private List<Monster> monsterList = new List<Monster>();

    private MonsterManager()
    {
    }

    public void Add(Monster monster)
    {
        monsterList.Add(monster);
    }

    private void Awake()
    {
        Inst = this;
    }

    private void Update()
    {

    }
}