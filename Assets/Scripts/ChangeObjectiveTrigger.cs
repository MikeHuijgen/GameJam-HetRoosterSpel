using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeObjectiveTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ObjectiveLog.Instance.StartPickUpObective();
            Destroy(gameObject);
        }
    }
}
