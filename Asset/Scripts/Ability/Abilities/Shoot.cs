using UnityEngine;

[CreateAssetMenu(fileName = "ShootAbility", menuName = "Abilities/Shoot")]
public class Shoot : Ability
{
    // Each Strategy can use custom logic. Implement the Use method in the subclasses
    public override void Use(ObjectT objectT, ExecuteActionCommandData data = null)
    {
        //8
        // Use method logs name, plays sound, and particle effect
        Debug.Log($"Using shoot: {m_AbilityName} {data.Poolable}");
        //Debug.Log("(8)Use " + GetTime.GetCurrentTime("full-ms"));

        if (data?.Poolable is Projectile projectile)
        {
            // Use reflection to find and call the method dynamically
            var method = typeof(Gun).GetMethod(m_AbilityName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            if (method != null)
            {
                objectT.ObjectShoot.Shoot(m_AbilityName, data);
                method.Invoke(objectT.ObjectShoot.Gun, new object[] { projectile });
                ThingHappen(objectT);
            }
            else
            {
                Debug.LogWarning($"Gun: Method '{m_AbilityName}' not found.");
            }
        }
    }
}
