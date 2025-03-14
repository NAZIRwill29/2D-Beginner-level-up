using UnityEngine;

[CreateAssetMenu(fileName = "MeleeAbility", menuName = "Abilities/Melee")]
public class Melee : Ability
{
    // Each Strategy can use custom logic. Implement the Use method in the subclasses
    public override void Use(ObjectT objectT, ExecuteActionCommandData data = null)
    {
        // Use method logs name, plays sound, and particle effect
        Debug.Log($"Using melee: {m_AbilityName}");
        objectT.ObjectMelee.Melee(m_AbilityName);
        ThingHappen(objectT);
    }
}
