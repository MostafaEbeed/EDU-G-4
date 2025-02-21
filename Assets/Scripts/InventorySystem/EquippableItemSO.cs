using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EquippableItem", menuName = "InventorySystem/Create EquippableItem", order = 1)]
public class EquippableItemSO : ItemSO, IDestroyableInventoryItem, IItemAction
{
    [SerializeField] private List<ModifierData> modifiersData = new List<ModifierData>();
    //[SerializeField] private List<InGameMissionEvent> inGameMissionEvents = new List<InGameMissionEvent>();


    public string ActionName => "Equip";

    public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
    {
        foreach (ModifierData data in modifiersData)
        {
            data.statModifier.AffectCharacter(character, data.value);
        }
        /*foreach (InGameMissionEvent data in inGameMissionEvents)
        {
            data.MissionEventInvoke();
        }  */
        return true;
    }
}
