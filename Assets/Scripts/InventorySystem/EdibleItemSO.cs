using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EdibleItem", menuName = "InventorySystem/Create EdibleItem", order = 1)]
public class EdibleItemSO : ItemSO, IDestroyableInventoryItem, IItemAction
{
    [SerializeField] private List<ModifierData> modifiersData = new List<ModifierData>();
    //[SerializeField] private List<InGameMissionEvent> inGameMissionEvents = new List<InGameMissionEvent>();

    public string ActionName => "Consume";

    public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
    {
        foreach (ModifierData data in modifiersData)
        {
            data.statModifier.AffectCharacter(character, data.value);
        }
        /*foreach (InGameMissionEvent data in inGameMissionEvents)
        {
            data.MissionEventInvoke();
        }   */
        return true;
    }
}

public interface IDestroyableInventoryItem
{

}

public interface IItemAction
{
    public string ActionName { get; }

    bool PerformAction(GameObject character, List<ItemParameter> itemState);
}

[Serializable]
public class ModifierData
{
    public CharacterStatModifierSO statModifier;
    public float value;
}
