using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Transform holdPosition; // Miejsce trzymania przedmiotu przed kamer¹
    public float pickupRange = 3f; // Maksymalna odleg³oœæ podnoszenia
    public LayerMask pickupLayer; // Warstwa przedmiotów do podnoszenia

    private Rigidbody pickedUpObject; // Przechowywany przedmiot

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (pickedUpObject == null)
            {
                TryPickUp();
            }
            else
            {
                DropObject();
            }
        }
    }

    void TryPickUp()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, pickupRange, pickupLayer))
        {
            if (hit.collider.attachedRigidbody != null)
            {
                pickedUpObject = hit.collider.attachedRigidbody;
                pickedUpObject.useGravity = false;
                pickedUpObject.isKinematic = true;
                pickedUpObject.transform.SetParent(holdPosition);
                pickedUpObject.transform.localPosition = Vector3.zero;
            }
        }
    }

    void DropObject()
    {
        if (pickedUpObject != null)
        {
            pickedUpObject.useGravity = true;
            pickedUpObject.isKinematic = false;
            pickedUpObject.transform.SetParent(null);
            pickedUpObject = null;
        }
    }

}
