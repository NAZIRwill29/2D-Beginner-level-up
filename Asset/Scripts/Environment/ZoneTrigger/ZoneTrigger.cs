using System;
using UnityEngine;

public class ZoneTrigger : StatsTrigger
{
    protected float m_Cooldown;
    //[SerializeField] protected float m_TimeCoolDown = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        TriggerEffect(other);
    }
    void OnTriggerStay2D(Collider2D other)
    {
        TriggerEffect(other);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        TriggerExit(other);
    }

    protected override void ProcessTrigger(Collider2D other)
    {
        //(1) trigger when Checker enter -> call triggered in stats of footChecker
        //ex: DefenseZone - call triggered(DefenseEffect, DefenseEffectData) in CharacterStatsManager of footChecker
        m_Cooldown -= Time.deltaTime;
        if (m_Cooldown > 0)
            return;

        var checker = GetCheckerComponent(other);
        if (checker?.objectStatsManager == null)
        {
            Debug.LogWarning($"Checker of type {m_StatTriggerFlyweightData.Receiver} is null or missing ObjectStatsManager!");
            return;
        }
        //Debug.Log("health() - ProcessTrigger " + GetTime.GetCurrentTime("full-ms"));
        if (m_StatTriggerFlyweightData is ZoneTriggerFlyweight zoneTriggerFlyweight)
            m_Cooldown = zoneTriggerFlyweight.TimeCoolDown;
        var effectData = CreateEffectData();
        if (effectData != null)
        {
            checker.objectStatsManager.Triggered(effectData, true);

            ThingHappen(other);
        }
        else
        {
            Debug.LogWarning("Effect data is null!");
        }
    }

    protected override void ProcessExit(Collider2D other)
    {
        m_Cooldown -= Time.deltaTime;
        base.ProcessExit(other);
    }

    public override object CreateEffectData()
    {
        // Provide implementation specific to ZoneTrigger
        // Example:
        // return base.CreateEffectData(); // Or construct and return an effect data object
        return null;
    }
}
