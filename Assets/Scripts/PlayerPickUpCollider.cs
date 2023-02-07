using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpCollider : MonoBehaviour
{
    [SerializeField] GameObject _pickUpPanel;
    private bool _isInRange;
    private Transform _rooster;

    private void Update()
    {
        PickUpRooster();
    }

    private void PickUpRooster()
    {
        if (_isInRange && Input.GetKeyDown(KeyCode.E))
        {
            _isInRange = false;
            _pickUpPanel.SetActive(false);
            Destroy(_rooster.parent.gameObject);
            _rooster = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Roosters"))
        {
            _rooster = other.transform;
            _pickUpPanel.SetActive(true);
            _isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Roosters"))
        {
            _rooster = null;
            _pickUpPanel.SetActive(false);
            _isInRange = false;
        }
    }
}
