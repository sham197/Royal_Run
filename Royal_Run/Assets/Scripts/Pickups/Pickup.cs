using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;
    const string PlayerTag = "Player";

    private void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(PlayerTag)) return;
        OnPickup();
        Destroy(gameObject);
    }

    protected abstract void OnPickup();
}