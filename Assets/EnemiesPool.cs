using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Distance,
    Melee,
    Misile
}

public class EnemiesPool : MonoBehaviour
{
    [SerializeField] EnemyData[] enemies;
    Dictionary<EnemyType, LinkedList<EnemyTest>> collection;
    Dictionary<EnemyType, LinkedList<EnemyTest>> availables;
    Dictionary<EnemyType, LinkedList<EnemyTest>> inScene;

    [Serializable]
    public struct EnemyData
    {
        public EnemyType type;
        public EnemyTest Enemy;
    }

    private void Awake()
    {
        availables = new();
        inScene = new();
        collection = new();

        foreach (var enemy in enemies)
        {
            if (!availables.ContainsKey(enemy.type))
            {
                availables.Add(enemy.type, new());
                inScene.Add(enemy.type, new());
                collection.Add(enemy.type, new());
            }

            collection[enemy.type].AddLast(enemy.Enemy);
        }
    }

    public GameObject GetEnemy(EnemyType type)
    {
        if (!availables.ContainsKey(type)) return null;

        if (availables[type].Count > 0)
        {
            inScene[type].AddLast(availables[type].First);
            availables[type].RemoveFirst();

            return inScene[type].Last.Value.gameObject;
        }

        return Instantiate(collection[type].First.Value.gameObject, transform);
    }

    public void Kill(EnemyTest enemy)
    {
        foreach (var key in availables.Keys)
        {
            var cur = availables[key].First;
            for (int i = 0; i < availables.Keys.Count; i++)
            {
                if (cur.Value.GetInstanceID() == enemy.GetInstanceID())
                {
                    cur.Value.gameObject.SetActive(false);
                    inScene[key].Remove(cur);
                    availables[key].AddLast(cur);
                }
            }

        }
    }
}
