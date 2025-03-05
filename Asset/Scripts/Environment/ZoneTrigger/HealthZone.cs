using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealthZone : StatusZone
{
    [SerializeField] protected HealthDataFlyweight m_HealthDataFlyweight;

    public override object CreateEffectData()
    {
        return m_HealthDataFlyweight.HealthData;
    }

    protected override void ThingHappen(Collider2D other)
    {
        if (other.GetComponentInParent<ObjectT>() is ObjectT objectT)
        {
            // Debug.Log("ObjecTThingHappen in Parent");
            m_ThingHappenTriggerSO.ObjecTThingHappen(
                objectT,
                " " + Mathf.Abs(m_HealthDataFlyweight.HealthData.DataNumVars.FirstOrDefault(v => v.Name.Equals("Health", StringComparison.OrdinalIgnoreCase)).AddNumVariable)
            );
        }
        else
        {
            Debug.LogWarning("objectT can't be found!");
        }
    }
}
