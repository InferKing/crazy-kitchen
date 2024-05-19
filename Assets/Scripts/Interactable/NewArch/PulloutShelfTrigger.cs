using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulloutShelfTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Grabbable component) && !component.Rb.isKinematic)
        {
            component.transform.SetParent(transform, true);
        }
    }
}
