using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public MeshRenderer Renderer;
    
    //random position varibales
    private Vector3 randomPos;
    private float ranPos = 5.0f;
    
    //random rotation varibales
    private Vector3 randomRot;
    private float ranRot = 100.0f;

    //random scale varibales
    private float randomScale;
    private float scaleMin = 1.0f;
    private float scaleMax = 5.0f;

    //random update time variables
    private float timeInterval;
    private float timeMin = 1.0f;
    private float timeMax = 5.0f;

    //random color variables
    private Color firstColor;
    private Color targetColor;
    private float colorMin = 0.0f;
    private float colorMax = 1.0f;

    void Start()
    {
        //generate random position
        randomPos = new Vector3(Random.Range(-ranPos, ranPos), Random.Range(-ranPos, ranPos), Random.Range(-ranPos, ranPos));
        transform.position = randomPos;

        //generate random scale
        randomScale = Random.Range(scaleMin, scaleMax);
        transform.localScale = Vector3.one * randomScale;

        //generate random rotation
        randomRot = new Vector3(Random.Range(-ranRot, ranRot), Random.Range(-ranRot, ranRot), Random.Range(-ranRot, ranRot));

        //generate random timeInterval
        timeInterval = Random.Range(timeMin, timeMax);

        //generate random color
        firstColor = new Color(Random.Range(colorMin, colorMax), Random.Range(colorMin, colorMax), Random.Range(colorMin, colorMax), Random.Range(colorMin, colorMax));
        targetColor = new Color(Random.Range(colorMin, colorMax), Random.Range(colorMin, colorMax), Random.Range(colorMin, colorMax), Random.Range(colorMin, colorMax));

        //assign first random color
        Material material = Renderer.material;
        material.color = firstColor;

        //repeat random generation
        Invoke("Start", timeInterval);

        //start ColorDissolve
        StartCoroutine("ColorDissolve");
    }

    void Update()
    {
        //rotate
        transform.Rotate(randomRot * Time.deltaTime);
    }
    
    IEnumerator ColorDissolve()
    {
        //dissolve to target color
        float timeStart = 0.0f;
        Material material = Renderer.material;
        while (timeStart < timeInterval)
        {
            Debug.Log(timeStart / timeInterval);
            material.color = Color.Lerp(firstColor, targetColor, timeStart / timeInterval);
            timeStart += Time.deltaTime;
            yield return null;
        }
        material.color = targetColor;
    }
}
