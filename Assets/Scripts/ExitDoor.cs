using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private GameObject escapedCanvas;

    public void EnableCollider()
    {
        boxCollider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            escapedCanvas.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
