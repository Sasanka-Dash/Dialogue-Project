using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationManager : MonoBehaviour
{
    public JSONReader jsonReader;
    public Text questionText;
    public List<Button> optionButtons;
    public Slider priceSlider;
    public Text npcResponseText;
    private int bargainingAttempts;

    void Start()
    {
        LoadConversation(1); // Example to load the first conversation
        priceSlider.onValueChanged.AddListener(delegate { OnPriceSliderChange(); });
    }

    void LoadConversation(int id)
    {
        var conversation = jsonReader.dialogueData.conversations.Find(conv => conv.id == id);
        if (conversation != null)
        {
            questionText.text = conversation.question;
            for (int i = 0; i < optionButtons.Count; i++)
            {
                if (i < conversation.options.Count)
                {
                    optionButtons[i].gameObject.SetActive(true);
                    optionButtons[i].GetComponentInChildren<Text>().text = conversation.options[i];
                    optionButtons[i].onClick.AddListener(delegate { OnOptionSelected(conversation.options[i]); });
                }
                else
                {
                    optionButtons[i].gameObject.SetActive(false);
                }
            }
        }
    }

    void OnOptionSelected(string option)
    {
        npcResponseText.text = "NPC: " + option;
    }

    void OnPriceSliderChange()
    {
        int price = (int)priceSlider.value;
        foreach (var response in jsonReader.dialogueData.bargaining.responses)
        {
            var range = response.range.Split('-');
            int min = int.Parse(range[0]);
            int max = int.Parse(range[1]);
            if (price >= min && price <= max)
            {
                npcResponseText.text = response.response;
                return;
            }
        }
        bargainingAttempts++;
        if (bargainingAttempts >= jsonReader.dialogueData.bargaining.attempts)
        {
            npcResponseText.text = "No more bargaining!";
        }
    }
}
