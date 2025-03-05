using System;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    [SerializeField] protected string m_AbilityName;
    public string AbilityName => m_AbilityName;
    [Tooltip("Image texture for UI button")]
    [SerializeField] protected Sprite m_ButtonIcon;
    [Header("Visuals")]
    [SerializeField] protected NameDataFlyweight m_FXNameDataFlyweight;
    [SerializeField] protected NameDataFlyweight m_SoundNameDataFlyweight;
    public bool IsPassiveAbility;
    public float Cooldown = 0.65f;
    public float Delay = 0f;
    public Sprite ButtonIcon => m_ButtonIcon;
    //public float TimeToNextPlay { get; set; }
    [Tooltip("for effect ObjectT only"), SerializeField] private NatureElement m_NatureElement;
    [Tooltip("for effect ObjectT only"), SerializeField] private NatureElementEffectObjectSO m_NatureElementEffectObjectSO;

    // Each Strategy can use custom logic. Implement the Use method in the subclasses
    public virtual void Use(ObjectT objectT, ExecuteActionCommandData data = null)
    {
        // Use method logs name, plays sound, and particle effect
        Debug.Log($"Using ability: {m_AbilityName}");
        ThingHappen(objectT);
    }

    protected virtual void ThingHappen(ObjectT objectT)
    {
        string soundName = m_SoundNameDataFlyweight != null ? m_SoundNameDataFlyweight.Name : "";
        string fxName = m_FXNameDataFlyweight != null ? m_FXNameDataFlyweight.Name : "";
        objectT.ThingHappen(new()
        {
            SoundName = soundName,
            FXName = fxName
        });
        if (m_NatureElement) objectT.NatureElement = m_NatureElement;
        if (m_NatureElementEffectObjectSO) objectT.NatureElementControllerObject.NatureElementEffectObjectSO = m_NatureElementEffectObjectSO;
    }

    // public virtual void OnBenefit(){}
    // public virtual void OnSupport(){}
    // public virtual void OnStrength(){}
    // public virtual void OnWeakness(){}
}