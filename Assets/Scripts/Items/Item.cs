using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private int Id;

    public abstract void Usage(Player player); //А надо ли это?

}
