using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class EndGame : MonoBehaviour
{
    QuickTimeEvent qte;

    public GameObject endUI;
    public TextMeshProUGUI Correct;
    public TextMeshProUGUI Incorrect;
    public TextMeshProUGUI Missed;
    public TextMeshProUGUI time;

    float timer;
    bool isOver;
    private int minutes;
    private int seconds;

    private void Start()
    {
        qte = GetComponent<QuickTimeEvent>();
    }
    public void Update()
    {
        if (!isOver)
        {
            timer += Time.deltaTime;
             minutes = Mathf.FloorToInt(timer / 60f);
             seconds = Mathf.FloorToInt(timer % 60f);
            if(seconds == 0)
            {
                Debug.Log("add");
            }
        }
        if (Keyboard.current != null)
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                End();
                isOver = true;
            }
        }
    }
    public void End()
    {
        qte.GameEnd();
        endUI.SetActive(true);

       
        if (seconds >= 0 && seconds <= 9)
        {
            string secondString = 0.ToString() + seconds.ToString();
            time.text = "time played: " + string.Format("{0,00}:{1,00}", minutes, secondString);
        }
        else
            time.text = "time played: " + string.Format("{0,00}:{1,00}", minutes, seconds);

        ;
        Correct.text = "Correct: " + qte.correct.ToString();
        Incorrect.text = "Incorrect: " + qte.incorrect.ToString();
        Missed.text = "Missed: " + qte.missed.ToString();
    }
    
}
