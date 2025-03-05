using UnityEngine;

public class EnvironmentObject : MonoBehaviour
{
    [SerializeField] private EnvironmentObjectRuntimeSetSO RuntimeSet;

    protected void OnEnable()
    {
        if (RuntimeSet) RuntimeSet.Add(this);
    }
    protected void OnDisable()
    {
        if (RuntimeSet) RuntimeSet.Remove(this);
    }
}
