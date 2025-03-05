using UnityEngine;

//TODO() - make collectible more complex + inventory
public class Collectible : MonoBehaviour
{
    [SerializeField] protected LayerMask m_InteractLayers;
    [SerializeField] protected NameDataFlyweight NameDataFlyweight;
    public string SoundName
    {
        get => NameDataFlyweight != null ? NameDataFlyweight.Name : string.Empty;
        set
        {
            if (NameDataFlyweight != null)
            {
                NameDataFlyweight.Name = value;
            }
        }
    }
    [SerializeField] private CollectibleRuntimeSetSO RuntimeSet;

    protected void OnEnable()
    {
        if (RuntimeSet) RuntimeSet.Add(this);
    }
    protected void OnDisable()
    {
        if (RuntimeSet) RuntimeSet.Remove(this);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (!LayerInteraction.IsLayerInteractable(other, m_InteractLayers)) return;

        ObjectT objectT = other.GetComponent<ObjectT>();
        if (objectT)
            objectT.ThingHappen(new() { SoundName = SoundName });

        // Notify any listeners through the event channel
        GameEvents.CollectibleCollected();
        //characterAudioMulti.PlayRandomClip(m_SoundName);
        Destroy(gameObject);
    }
}
