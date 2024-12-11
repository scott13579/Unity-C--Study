using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct MonsterTest
{
    public string name;
    public int health;
}

public class LinqExample : MonoBehaviour
{
    public List<MonsterTest> monsters = new List<MonsterTest>()
    {
        new MonsterTest() { name = "A", health = 10 },
        new MonsterTest() { name = "B", health = 30 },
        new MonsterTest() { name = "C", health = 50 },
        new MonsterTest() { name = "A", health = 20 },
        new MonsterTest() { name = "A", health = 100 },
        new MonsterTest() { name = "C", health = 70 },
        new MonsterTest() { name = "A", health = 40 },
        new MonsterTest() { name = "A", health = 200 },
        new MonsterTest() { name = "A", health = 5 },
    };

    void Start()
    {
        // 몬스터 테스트 그룹에서 A 네임을 가진 hp 30 이상의 오브젝트들을 리스트화 해서 체력 높은 순으로 출력하기
        
        List<MonsterTest> list = new List<MonsterTest>();
        for (var i = 0; i < monsters.Count; i++)
        {
            if (monsters[i].name == "A" && monsters[i].health >= 30)
            {
                list.Add(monsters[i]);
            }
        }

        // 왼쪽에 있던게 크면 왼쪽을 앞으로 보낸다는 뜻
        // 왼쪽에 있던게 더 작으면 왼쪽에 있는 것을 뒤로 보냄
        list.Sort((l,r) => l.health>= r.health ? -1 : 1);

        foreach (var monster in list)
        {
            Debug.Log($"이름 : {monster.name}, 체력 : {monster.health}");
        }
        
        var linqFilter = monsters.Where(
            e => e is { name : "A", health: >= 30 }
            ).
            OrderByDescending(e => e.health
            ).ToList();
        
        for (var i = 0; i < linqFilter.Count; i++)
        {
            Debug.Log($"이름 : {linqFilter[i].name}, 체력 : {linqFilter[i].health}");
        }

        var linqFilter2= 
            (from e in monsters 
                where e is { health: >= 30, name: "A" }
                orderby e.health 
                descending
                select new{ e.name, e.health})
            .ToList();
        
        foreach (var t in linqFilter2)
        {
            Debug.Log($"이름 : {t.name}, 체력 : {t.health}");
        }
    }

    void Update()
    {
        
    }
}
