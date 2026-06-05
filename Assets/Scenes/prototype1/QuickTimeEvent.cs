using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

using TMPro;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class QuickTimeEvent : MonoBehaviour
{
    public ShapesInCircle listFinder;
    public GameObject textObject;
    public TextMeshProUGUI displayShapeText;
    string targetName;
    Color newColor;
    public float baseTime;

    bool isClicked;
    public void Start()
    {
        listFinder = GetComponent<ShapesInCircle>();
        StartRound();
    }

   void StartRound()
    {
        List<ShapesAndColours> target = RandomShape();
        int index = Random.Range(0, target.Count);
        string shape = target[index].shapesObject.name;
        string shapeName = shape + target[index].colorName;
       displayShapeText.text = shape;
        if (UnityEngine.ColorUtility.TryParseHtmlString(target[index].colorName, out newColor))
        {
            displayShapeText.color = newColor;
        }
        StartCoroutine(SelectShapeColor(shapeName));
    }
    IEnumerator SelectShapeColor(string shapeName)
    {
       textObject.SetActive(true);
        float timer = baseTime;
        while(timer > 0)
        {
            
            if(isClicked)
            {
                if(shapeName == targetName)
                {
                    Debug.Log("Correct");
                    StartCoroutine(WaitBetweenInputs());
                    yield break;
                }
                else
                {
                    Debug.Log("wrong");
                    StartCoroutine(WaitBetweenInputs());
                    yield break;
                }
               
            }
            timer -= Time.deltaTime;
            yield return null;
        }
        StartCoroutine(WaitBetweenInputs());
        yield return null;
    }
    public IEnumerator WaitBetweenInputs()
    {
        textObject.SetActive(false);
        isClicked = false;
        targetName = null;
        yield return new WaitForSeconds(1f);
        StartRound();
        
    }
    public void Clicked(string objectName)
    {
        targetName = objectName;
        isClicked = true;
    }

    List<ShapesAndColours> RandomShape()
    {
        List<ShapesAndColours> sc = listFinder.ShapesAndColours[Random.Range(0, listFinder.ShapesAndColours.Count)].Shapes;
        return sc;
    }
}
