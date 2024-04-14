using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDrop : MonoBehaviour
{
    public GameObject item;

    public void DropLoot()
    {
        Instantiate(item, transform.position, transform.rotation);
    }
}
