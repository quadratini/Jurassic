using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitSelector : MonoBehaviour
{
    GameObject selectedUnit;
    GameObject hoveredUnit;

    private void Start() {
    }

    private void Update() {
        DetectSelection();
        DetectHover();
    }

    void DetectHover() {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null) {
            GameObject go = GetRootGameObject(hit.collider);
            UnhoverUnit();
            HoverUnit(go);
        } else {
            UnhoverUnit();
        }
    }

    void DetectSelection() {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (Input.GetMouseButtonDown(0)) {
            if (hit.collider != null) {
                GameObject go = GetRootGameObject(hit.collider);
                Transform tf = go.transform;
                //DeselectUnit();
                //SelectUnit(go);
            } else {
                //DeselectUnit();
            }

        }
    }

    void HoverUnit(GameObject unit) {
        Color c = unit.GetComponentInChildren<SpriteRenderer>().color;
        SetUnitColor(unit, new Color(c.r, c.g, c.b, .8f));

        Transform nameTag = unit.transform.Find("Name Tag");
        if (nameTag == null) {
            CreateNameTag(unit);
        } else {
            nameTag.gameObject.SetActive(true);
        }

        hoveredUnit = unit;
    }

    void UnhoverUnit() {
        if (hoveredUnit != null) {
            Color c = hoveredUnit.GetComponentInChildren<SpriteRenderer>().color;
            SetUnitColor(hoveredUnit, new Color(c.r, c.g, c.b, 1));
            hoveredUnit.transform.Find("Name Tag").gameObject.SetActive(false);
            hoveredUnit = null;
        }
    }

    private void CreateNameTag(GameObject unit) {
        GameObject nameTagGo = new GameObject("Name Tag");
        nameTagGo.transform.parent = unit.transform;
        TextMesh unitNameText = nameTagGo.AddComponent<TextMesh>();
        unitNameText.text = unit.GetComponent<Dinosaur>().speciesName;
        unitNameText.offsetZ = 10;
        unitNameText.alignment = TextAlignment.Center;
        unitNameText.anchor = TextAnchor.LowerCenter;
        unitNameText.transform.localPosition = new Vector2(
            0, (unit.GetComponent<Dinosaur>().torsoWidth + .25f) / 2
        );
        unitNameText.fontSize = 200;
        unitNameText.characterSize = .02f;
        unitNameText.color = Color.black;
    }

    private GameObject GetRootGameObject(Collider2D coll) {
        Transform tf = coll.transform;
        while (tf.parent != null) {
            tf = tf.transform.parent;
        }
        return tf.gameObject;
    }

    public static void SetUnitColor(GameObject unit, Color c) {
        SpriteRenderer[] allSr = unit.GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < allSr.Length; ++i) {
            SpriteRenderer sr = allSr[i];
            sr.color = new Color(c.r, c.g, c.b, c.a);
        }
    }
}
