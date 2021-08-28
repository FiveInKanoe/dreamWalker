using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item", order = 51)]
public class Item2 : ScriptableObject
{
    [SerializeField] private int id;
    public int Id { get; }



}
