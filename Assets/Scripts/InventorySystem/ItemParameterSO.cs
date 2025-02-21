using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ItemParameter", menuName = "InventorySystem/Create ItemParameter", order = 1)]
public class ItemParameterSO : ScriptableObject
{
    [field: SerializeField] public string ParameterName { get; private set; } 
}
