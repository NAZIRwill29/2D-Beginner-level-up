using UnityEngine;

public class PowerUpCollectible : Collectible
{
    [SerializeField] private int m_PowerUpID;
    [SerializeField] private string m_PowerUpName;
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }
}
