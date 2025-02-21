using UnityEngine;

[CreateAssetMenu(fileName = "New CharacterStatModifier", menuName = "InventorySystem/Create CharacterStatModifier", order = 0)]

public class HealthModifier : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float val)
    {
        Debug.Log(val);
    }
}
