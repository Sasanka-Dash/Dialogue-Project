using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public GameObject dialogueUI;
    private bool isNearNPC = false;

    void Update()
    {
        if (isNearNPC && Input.GetKey(KeyCode.E))
        {
            dialogueUI.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            isNearNPC = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            isNearNPC = false;
            dialogueUI.SetActive(false);
        }
    }
}
