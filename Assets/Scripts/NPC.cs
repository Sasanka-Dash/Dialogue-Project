using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel; // Reference to the dialogue panel UI element
    public Text dialogueText; // Reference to the UI Text component for displaying dialogue
    public string[] dialogue; // Array to store dialogue strings
    private int index; // Index to keep track of the current dialogue line

    public GameObject contButton; // Reference to the continue button UI element
    public float wordSpeed = 0.1f; // Set a default word speed at which the dialogue text appears, character by character
    public bool playerIsClose; // Boolean to check if the player is near the NPC

    void Start()
    {
        // Ensure the dialogue panel is inactive at the start of the game
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false); // Ensure the dialogue panel is inactive at the start
        }
        else
        {
            Debug.LogError("Dialogue Panel is not assigned!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose) // Check if the 'E' key is pressed and the player is close to the NPC
        {
            if (dialoguePanel.activeInHierarchy) // If the dialogue panel is already active, reset the dialogue
            {
                zeroText();
            }
            else
            {
                dialoguePanel.SetActive(true); // Otherwise, activate the dialogue panel and start typing the dialogue
                StartCoroutine(Typing());
            }
        }
        
        if (dialogueText.text == dialogue[index]) // Enable the continue button if the current dialogue line is fully displayed
        {
            contButton.SetActive(true);
        }
    }

    public void zeroText() // Method to reset the dialogue text and deactivate the dialogue panel
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing() // Coroutine to display the dialogue text character by character
    {
        dialogueText.text = "";
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine() // Method to display the next line of dialogue or reset if at the end
    {
        contButton.SetActive(false);
        
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) // Trigger event when the player enters the NPC's collider
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
            Debug.Log("Player is close to the NPC");
        }
    }

    private void OnTriggerExit2D(Collider2D other) // Trigger event when the player exits the NPC's collider
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            zeroText();
            Debug.Log("Player left the NPC");
        }
    }
}