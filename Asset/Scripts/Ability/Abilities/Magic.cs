using UnityEngine;

[CreateAssetMenu(fileName = "MagicAbility", menuName = "Abilities/Magic")]
public class Magic : Ability
{
    // Each Strategy can use custom logic. Implement the Use method in the subclasses
    public override void Use(ObjectT objectT, ExecuteActionCommandData data = null)
    {
        //TODO() - LIKE PROJECTILE
        // Use method logs name, plays sound, and particle effect
        Debug.Log($"Using magic: {m_AbilityName}");
        objectT.ObjectMagic.Magic(m_AbilityName);
        ThingHappen(objectT);
    }
}
