using UnityEngine;

public class Collector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        ICollectible collectible = (ICollectible)collider.GetComponentInChildren(typeof(ICollectible));

        collectible?.Collect();
    }
}
