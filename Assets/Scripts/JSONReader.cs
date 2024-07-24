using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Conversation
{
    public int id;
    public string question;
    public List<string> options;
}

[System.Serializable]
public class BargainingResponse
{
    public string range;
    public string response;
}

[System.Serializable]
public class Bargaining
{
    public int min_price;
    public int max_price;
    public int attempts;
    public List<BargainingResponse> responses;
}

[System.Serializable]
public class DialogueData
{
    public List<Conversation> conversations;
    public Bargaining bargaining;
}

public class JSONReader : MonoBehaviour
{
    public DialogueData dialogueData;

    void Awake()
    {
        TextAsset jsonData = Resources.Load<TextAsset>("dialogues");
        dialogueData = JsonUtility.FromJson<DialogueData>(jsonData.text);
    }
}

