using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ShapesAndColours
{
    public GameObject shapesObject;
    public string colorName;
}
[Serializable]

public class ShapeGroup
{
    public List<ShapesAndColours> Shapes;
}
public class ShapesInCircle : MonoBehaviour
{
    public delegate void GamePlay();

    public static GamePlay gameplay;

    public List<ShapeGroup> ShapesAndColours;
    public float radius;
    public int amount = 4;
    Color newColor;
    private void OnEnable()
    {
        StartGame.startGame += Game;
    }
    private void OnDisable()
    {
        StartGame.startGame -= Game;
    }
    private void Game()
    {

        int totalAmount = ShapesAndColours.Count * ShapesAndColours[0].Shapes.Count;
        for (int s = 0; s < ShapesAndColours.Count; s++)
        {
            for (int c = 0; c < ShapesAndColours[s].Shapes.Count; c++)
            {
                float index = (c * ShapesAndColours.Count) + s;
                float radian = index * (2f * Mathf.PI / totalAmount);

                float x = Mathf.Cos(radian) * radius;
                float y = Mathf.Sin(radian) * radius;

                Vector3 spawnPosition = transform.position + new Vector3(x, y, 0);


                GameObject shape = Instantiate(ShapesAndColours[s].Shapes[c].shapesObject, spawnPosition, Quaternion.identity);
                shape.name = ShapesAndColours[s].Shapes[c].shapesObject.name + ShapesAndColours[s].Shapes[c].colorName;
     
                string colour = ShapesAndColours[s].Shapes[c].colorName;
                
                if (ColorUtility.TryParseHtmlString(colour,out newColor))
                {
                    shape.GetComponent<Renderer>().material.color = newColor;
                }
                else
                {
                    Debug.LogError("failed to parse color");
                }
                
            }
        }
        gameplay.Invoke();
    }
}
