using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class RandomChar : MonoBehaviour
{
    string listOfChar = "ABCDEFGHIJKLMNPOQRSTUVWXYZ1234567890";
    [SerializeField] char[] charArray;
    [SerializeField] float timerLength;


    void Start()
    {
        charArray = listOfChar.ToCharArray();
        SelectChar();
    }

    private void SelectChar()
    {
        int index = Random.Range(0, charArray.Length);
        string selectedChar = charArray[index].ToString();
        Debug.Log("selected Char: " + selectedChar);
        StartCoroutine(RandomCharLoop(selectedChar));
    }

    IEnumerator RandomCharLoop(string selectedChar)
    {
        float inputCheckTimer = timerLength;
        while (true)
        {
            if (Keyboard.current.anyKey.wasPressedThisFrame)
            {
                foreach (var key in Keyboard.current.allKeys)
                {
                    if (key.wasPressedThisFrame)
                    {

                        string pressedKey = key.displayName;

                        if (pressedKey == selectedChar)
                        {
                            Debug.Log("correct match");
                        }
                        else
                        {
                            Debug.LogError("Failed Match");
                        }
                        Debug.Log(pressedKey);
                        SelectChar();
                    }
                }
            }
            yield return null;
        }
    }
}
