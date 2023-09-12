using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    [SerializeField] public float throwForce = 10f;
    [SerializeField] public bool isLargeTrash = false;
    
    private string trashType;
    
    private int barrierLayer;
    private int trashCanLayer;
    private int dumpsterLayer;

    private MoveTrash moveTrash;
    private GameObject playerHand;
    private GameManager gameManager;

    private void Start()
    {
        // Get trash type
        trashType = gameObject.tag;

        // Get move trash script
        moveTrash = GameObject.FindGameObjectWithTag("Player").GetComponent<MoveTrash>();

        // Get player hand
        playerHand = GameObject.FindGameObjectWithTag("PlayerHand");

        // Get barrier layer
        barrierLayer = LayerMask.NameToLayer("Barrier");

        // Get trash can layer
        trashCanLayer = LayerMask.NameToLayer("TrashCan");

        // Get dumpster layer
        dumpsterLayer = LayerMask.NameToLayer("Dumpster");

        // Get game manager
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        // Check if trash touches a barrier
        if (collision.gameObject.layer == barrierLayer)
        {
            // Check if trash is in player hand
            if (transform.parent == playerHand.transform)
            {
                // Drop trash
                moveTrash.DropTrash();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if trash touches a trash can and is small or it touched a dumpster
        if ((collision.gameObject.layer == trashCanLayer && !isLargeTrash) || collision.gameObject.layer == dumpsterLayer)
        {
            // Check if trash is in player hand
            if (transform.parent == playerHand.transform)
            {
                // Drop trash
                moveTrash.DropTrash();
            }

            // Collect trash
            gameManager.CollectTrash(trashType);

            // Destroy trash
            Destroy(gameObject);
        }
    }
}
