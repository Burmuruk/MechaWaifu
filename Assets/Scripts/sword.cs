using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : MonoBehaviour
{
    [SerializeField] string enemyTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == enemyTag)
        {
            other.GetComponent<Character>().Damage();
        }
    }
}
