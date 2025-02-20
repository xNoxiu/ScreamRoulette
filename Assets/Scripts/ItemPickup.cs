using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Transform holdPosition; 
    public float pickupRange = 2.5f; 
    public LayerMask pickupLayer; 

    private Rigidbody pickedUpObject;

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
