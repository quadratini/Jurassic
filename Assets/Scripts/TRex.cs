using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRex : Dinosaur
{
    public float clawWidth;
    public float clawHeight;
    public Sprite tailSprite;
    public Sprite torsoSprite;
    public Sprite clawSprite;
    public Sprite headSprite;

    GameObject tailGo;
    GameObject torsoGo;
    GameObject leftClawGo;
    GameObject rightClawGo;
    GameObject headGo;
    // Start is called before the first frame update
    void Start()
    {
        speciesName = "T-Rex";

        torsoGo = new GameObject("torso");
        tailGo = new GameObject("tail");
        leftClawGo = new GameObject("leftClaw");
        rightClawGo = new GameObject("rightClaw");
        headGo = new GameObject("head");

        torsoGo.transform.parent = gameObject.transform;
        tailGo.transform.parent = gameObject.transform;
        leftClawGo.transform.parent = gameObject.transform;
        rightClawGo.transform.parent = gameObject.transform;
        headGo.transform.parent = gameObject.transform;

        torsoGo.AddComponent<SpriteRenderer>().sprite = torsoSprite;
        tailGo.AddComponent<SpriteRenderer>().sprite = tailSprite;
        leftClawGo.AddComponent<SpriteRenderer>().sprite = clawSprite;
        rightClawGo.AddComponent<SpriteRenderer>().sprite = clawSprite;
        headGo.AddComponent<SpriteRenderer>().sprite = headSprite;

        headGo.AddComponent<CapsuleCollider2D>().direction = CapsuleDirection2D.Horizontal;
        torsoGo.AddComponent<CapsuleCollider2D>().direction = CapsuleDirection2D.Horizontal;
        tailGo.AddComponent<CapsuleCollider2D>().direction = CapsuleDirection2D.Horizontal;
        leftClawGo.AddComponent<CapsuleCollider2D>().direction = CapsuleDirection2D.Horizontal;
        rightClawGo.AddComponent<CapsuleCollider2D>().direction = CapsuleDirection2D.Horizontal;

        torsoGo.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        headGo.transform.localPosition = new Vector3(0.5f, 0.0f, -0.5f);
        tailGo.transform.localPosition = new Vector3(-0.4f, 0.0f, 1.0f);
        leftClawGo.transform.localPosition = new Vector3(0.2f, 0.3f, 1.0f);
        rightClawGo.transform.localPosition = new Vector3(0.2f, -0.3f, 1.0f);

        leftClawGo.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 45.0f);
        rightClawGo.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, -45.0f);


        //color = new Color(9/255f, 82/255f, 9/255f, 1); // yayoyaa yoo

        SpriteRenderer tailRenderer = tailGo.GetComponent<SpriteRenderer>();
        SpriteRenderer torsoRenderer = torsoGo.GetComponent<SpriteRenderer>();
        SpriteRenderer leftClawRenderer = leftClawGo.GetComponent<SpriteRenderer>();
        SpriteRenderer rightClawRenderer = rightClawGo.GetComponent<SpriteRenderer>();
        SpriteRenderer headRenderer = headGo.GetComponent<SpriteRenderer>();

        torsoRenderer.color = new Color(color.r, color.g, color.b, 1);
        tailRenderer.color = new Color(color.r, color.g, color.b, 1);
        leftClawRenderer.color = new Color(color.r, color.g, color.b, 1);
        rightClawRenderer.color = new Color(color.r, color.g, color.b, 1);
        headRenderer.color = new Color(color.r, color.g, color.b, 1);

        tailGo.transform.localScale = new Vector3(tailHeight, tailWidth, 1.0f);
        leftClawGo.transform.localScale = new Vector3(clawHeight, clawWidth, 1.0f);
        rightClawGo.transform.localScale = new Vector3(clawHeight, clawWidth, 1.0f);
        headGo.transform.localScale = new Vector3(headHeight, headWidth, 1.0f);
        torsoGo.transform.localScale = new Vector3(torsoHeight, torsoWidth, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
