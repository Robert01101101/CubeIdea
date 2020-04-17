using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private bool objectInTrigger = false;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        objectInTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        objectInTrigger = false;
    }

    public bool IsObjectInTrigger()
    {
        return objectInTrigger;
    }
}
