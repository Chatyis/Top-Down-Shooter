using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupableBehaviour : MonoBehaviour
{
    public string type = "healthkit";
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == "Player")
        {
            collider.GetComponent<PlayerController>().PickUp(type);
            Destroy(this.gameObject);
        }

    }
}
