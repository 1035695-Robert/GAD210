using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Collections;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;

public class RandomChar : MonoBehaviour
{
    string listOfChar = "qwertyuiopasdfghjklzxcvbnm";

    [SerializeField] List<char> charArray;
    public float defaultTimerLenght = 2;
    public float timerLength;

    public float inputCheckTimer;

    float waitTime = 0.5f;

    public TextMeshProUGUI displayKey;
    public TextMeshProUGUI correctCounter;
    public int bonusCounter = 0;



    void Start()
    {
        foreach (char a in listOfChar)
        {
            charArray.Add(a);
        }
        timerLength = defaultTimerLenght;
        SelectChar();
    }

    private void SelectChar()
    {
        int index = Random.Range(0, charArray.Count);
        string selectedChar = charArray[index].ToString();
        displayKey.text = selectedChar.ToUpper();

        StartCoroutine(CharLoop(selectedChar));
    }

    IEnumerator CharLoop(string selectedChar)
    {
        Event.toggleOn.Invoke();
        inputCheckTimer = timerLength;

        while (inputCheckTimer > 0f)
        {
            if (Keyboard.current.anyKey.wasPressedThisFrame)
            {
                string pressedKey = Keyboard.current.allKeys.FirstOrDefault(k => k.wasPressedThisFrame).name;
                Debug.Log(pressedKey);
                foreach (char c in charArray)
                {
                    if (pressedKey == c.ToString())
                    {
                        Debug.Log("pressed this key " + pressedKey);
                        if (pressedKey == selectedChar)
                        {
                            Correct();
                            StartCoroutine(WaitBetweenInputs());
                            yield break;
                        }
                        else
                        {
                            Failed();
                            StartCoroutine(WaitBetweenInputs());
                            yield break;
                        }
                    }
                    yield return null;
                }
            }
            inputCheckTimer -= Time.deltaTime;
            yield return null;
        }
        Failed();
        StartCoroutine(WaitBetweenInputs());
        yield break;
    }

    public IEnumerator WaitBetweenInputs()
    {
        Event.toggleOff.Invoke();
        yield return new WaitForSeconds(waitTime);
        SelectChar();
    }
    void Correct()
    {
        Debug.Log("correct match");
        
        timerLength -= 0.2f;
        if (timerLength < 0.5f)
        {
            timerLength = 0.5f;
        }

        bonusCounter++;
        correctCounter.text = "Combo: "+ bonusCounter.ToString() +"x";
    }
    void Failed()
    {
        Debug.Log("Failed Match");
        charArray.RemoveAt(charArray.Count - 1);

        timerLength = defaultTimerLenght;
        bonusCounter = 0;
        correctCounter.text = bonusCounter.ToString();
    }
}
