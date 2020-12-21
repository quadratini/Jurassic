using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dinosaur : Unit, INameable
{
    public float tailHeight;
    public float tailWidth;
    public float headHeight;
    public float headWidth;
    public float torsoHeight;
    public float torsoWidth;
    public float speed;
    public float vision;
    public float eyePosition;
    public string speciesName;

    public Color color;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetName() {
        return speciesName;
    }
}
