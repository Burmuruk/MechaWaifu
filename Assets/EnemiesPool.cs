using System;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Distance,
    Melee,
    Berserker
}

public class EnemiesPool : MonoBehaviour
{
    [SerializeField] EnemyData[] enemies;
    Dictionary<EnemyType, LinkedList<Enemies>> collection;
    Dictionary<EnemyType, LinkedList<Enemies>> availables;
    Dictionary<EnemyType, LinkedList<Enemies>> inScene;

    public int count { get; private set; } = 0;
    public int Availables { get => availables.Count; }

    [Serializable]
    public struct EnemyData
    {
        public EnemyType type;
        public Enemies Enemy;
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
            inScene[type].Last.Value.Health = 3;
            inScene[type].Last.Value.gameObject.SetActive(true);

            return inScene[type].Last.Value.gameObject;
        }

        var newEnemy = Instantiate(collection[type].First.Value.gameObject, transform);
        newEnemy.name = $"Enemy {Enum.GetNames(typeof(EnemyType))[(int)type]} {count++}";
        inScene[type].AddLast(newEnemy.GetComponent<Enemies>());

        return newEnemy;
    }

    public void Kill(Enemies enemy)
    {
        foreach (var key in inScene.Keys)
        {
            var cur = inScene[key].First;
            for (int i = 0; i < inScene[key].Count; i++)
            {
                if (cur.Value.GetInstanceID() == enemy.GetInstanceID())
                {
                    cur.Value.gameObject.SetActive(false);
                    inScene[key].Remove(cur);
                    availables[key].AddLast(cur);
                    break;
                }

                cur = cur.Next;
            }
        }
    }
}
