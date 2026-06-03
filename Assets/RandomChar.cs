using JetBrains.Annotations;
using System.Collections;
using System.Linq;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class RandomChar : MonoBehaviour
{
    string listOfChar = "1234";
    //abcdefghijklmnopqrstuvwxyz
    [SerializeField] char[] charArray;
    [SerializeField] float timerLength = 2;
    float waitTime = 0.5f;

    public TextMeshProUGUI displayKey;



    void Start()
    {
        charArray = listOfChar.ToCharArray();
        SelectChar();
    }

    private void SelectChar()
    {
        int index = Random.Range(0, charArray.Length);
        string selectedChar = charArray[index].ToString();
        displayKey.text = selectedChar;

        StartCoroutine(CharLoop(selectedChar));
    }

    IEnumerator CharLoop(string selectedChar)
    {
        float inputCheckTimer = timerLength;

        while (inputCheckTimer > 0f)
        {
            if (Keyboard.current.anyKey.wasPressedThisFrame)
            {
                string pressedKey = Keyboard.current.allKeys.FirstOrDefault(k => k.wasPressedThisFrame).name;
                foreach (var c in charArray)
                {
                    if (pressedKey == c.ToString())
                    {
                        Debug.Log(pressedKey);
                        if (pressedKey == selectedChar)
                        {
                            Debug.Log("correct match");
                            
                        }
                        else
                        {
                            Debug.LogError("Failed Match");
                        }
                    }
                    Event. 
                    StartCoroutine(WaitBetweenInputs());
                    yield break;
                }

               

            }

            inputCheckTimer -= Time.deltaTime;
            yield return null;
        }
        StartCoroutine(WaitBetweenInputs());
        yield break;
    }

    public IEnumerator WaitBetweenInputs()
    {
        yield return new WaitForSeconds(waitTime);
        SelectChar();
    }
}
