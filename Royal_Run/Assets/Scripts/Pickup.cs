using UnityEngine;

public class Pickup : MonoBehaviour
{
    const string PlayerTag = "Player";
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(PlayerTag))
            Debug.Log(other.gameObject.name);
    }
}