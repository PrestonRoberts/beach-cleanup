using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrash : MonoBehaviour
{
    [SerializeField] private LayerMask pickupLayerMask;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform playerHand;
    [SerializeField] private GameObject allTrash;

    private bool holdingTrash = false;
    private GameObject trashObject;
    private Trash trash;

    [SerializeField] private float grabDistance = 2f;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (holdingTrash)
            {
                // Throw trash
                ThrowTrash();
            }
            else
            {
                // Grab trash
                GrabTrash();
            }
        }

        if (holdingTrash)
        {
            // Move trash to player hand
            trashObject.transform.position = playerHand.position;
        }
    }

    private void GrabTrash()
    {
        // Raycast to see if we hit any trash
        Ray cameraRay = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if(Physics.Raycast(cameraRay, out RaycastHit hit, grabDistance, pickupLayerMask))
        {
            // Get trash object and trash script
            trashObject = hit.collider.gameObject;
            trash = trashObject.GetComponent<Trash>();
            holdingTrash = true;

            // Make the trash a child of the player hand
            trashObject.transform.SetParent(playerHand);

            // Disable trash rigidbody
            trashObject.GetComponent<Rigidbody>().isKinematic = true;
        }
        
    }

    private void ThrowTrash()
    {
        holdingTrash = false;

        // Make the trash a child of the all trash object
        trashObject.transform.SetParent(allTrash.transform);

        // Enable trash rigidbody
        trashObject.GetComponent<Rigidbody>().isKinematic = false;

        // Throw trash
        trashObject.GetComponent<Rigidbody>().AddForce(playerCamera.transform.forward * trash.throwForce, ForceMode.Impulse);
    }

    public void DropTrash()
    {
        holdingTrash = false;

        // Make the trash a child of the all trash object
        trashObject.transform.SetParent(allTrash.transform);

        // Enable trash rigidbody
        trashObject.GetComponent<Rigidbody>().isKinematic = false;
    }
}
