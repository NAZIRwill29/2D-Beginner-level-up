using UnityEngine;

public class ItemCollectible : Collectible
{
    [SerializeField] private int m_ItemID;
    [SerializeField] private string m_ItemName;
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }
}
