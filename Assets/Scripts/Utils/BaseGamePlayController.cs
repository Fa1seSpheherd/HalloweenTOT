using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class BaseGamePlayController : MonoBehaviour
{

    public GameObject buttons;
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI healthText;

    protected bool playerWantsToOpenDoor = false;

    private int _health = 100;
    protected int health {
        get => _health;
        set {            
            _health = value;
            if (_health < 0) _health = 0;
            if (_health > 100) _health = 100;
            if (healthText != null)
            {
                healthText.text = $"{health}%";
            }
        }
    }

    bool hasChosen = false;
    float timeLimit = 5f;
    float messageTimeout = 0f;
    
    void Start()
    {
        StartCoroutine(HandleGameplayLoop());
    }

    protected void ShowMessage(string message)
    {
        messageText.text = message;
        messageTimeout = 2;
    }

    void Update()
    {
        if (messageTimeout > 0f) {
            messageTimeout -= Time.deltaTime;
            if (messageTimeout <= 0f) messageText.text = "";
        }
    }

    virtual protected IEnumerator HandleGameplayLoop() 
    {
        return null;
    }

    protected IEnumerator WaitForPlayerInput()
    {
        hasChosen = false;
        buttons.SetActive(true);
        float timer = 0f;
        playerWantsToOpenDoor = false;
        while (!hasChosen && timer < timeLimit)
        {
            timer += Time.deltaTime;
            int timeLeft = Mathf.FloorToInt(timeLimit - timer);
            timerText.text = $"{timeLeft} sec";
            yield return null;
        }
        timerText.text = "";
        buttons.SetActive(false);
    }

    public void OnOpen()
    {
        if (hasChosen) return;
        hasChosen = true;
        playerWantsToOpenDoor = true;
    }

    public void OnDontOpen()
    {
        if (hasChosen) return;
        hasChosen = true;
        playerWantsToOpenDoor = false;
    }
}