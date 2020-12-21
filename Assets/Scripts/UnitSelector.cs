using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitSelector : MonoBehaviour
{
    GameObject selectedUnit;
    GameObject hoveredUnit;

    public Sprite selectorSprite;

    GameObject[] selectors;

    private void Start() {
        selectors = new GameObject[4];
        selectors[0] = new GameObject("Top Left Selector");
        selectors[1] = new GameObject("Top Right Selector");
        selectors[1].transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
        selectors[2] = new GameObject("Bottom Left Selector");
        selectors[2].transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
        selectors[3] = new GameObject("Bottom Right Selector");
        selectors[3].transform.localRotation = Quaternion.Euler(0f, 0f, 180f);
        foreach (GameObject go in selectors) {
            go.AddComponent<SpriteRenderer>().sprite = selectorSprite;
            go.SetActive(false);
        }
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
                PlaceSelectorsAroundGameObject(go);
                
                foreach (GameObject selector in selectors) {
                    selector.SetActive(true);
                }
            } else {
                foreach (GameObject selector in selectors) {
                    selector.SetActive(false);
                }
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
        Bounds bo = new Bounds(unit.transform.position, Vector2.zero);
        foreach (Renderer r in unit.GetComponentsInChildren<SpriteRenderer>()) {
            bo.Encapsulate(r.bounds);
        }
        unitNameText.transform.position = new Vector2(
            bo.center.x, bo.center.y + bo.extents.y
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

    private void PlaceSelectorsAroundGameObject(GameObject go) {
        Transform tf = go.transform;

        Bounds bo = new Bounds(tf.position, Vector2.zero);
        foreach (Renderer r in go.GetComponentsInChildren<SpriteRenderer>()) {
            bo.Encapsulate(r.bounds);
        }
        selectors[0].transform.position = new Vector2(bo.center.x - bo.extents.x - .02f, bo.center.y + bo.extents.y);
        selectors[1].transform.position = new Vector2(bo.center.x + bo.extents.x, bo.center.y + bo.extents.y);
        selectors[2].transform.position = new Vector2(bo.center.x - bo.extents.x, bo.center.y - bo.extents.y);
        selectors[3].transform.position = new Vector2(bo.center.x + bo.extents.x, bo.center.y - bo.extents.y);
    }

    public static void SetUnitColor(GameObject unit, Color c) {
        SpriteRenderer[] allSr = unit.GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < allSr.Length; ++i) {
            SpriteRenderer sr = allSr[i];
            sr.color = new Color(c.r, c.g, c.b, c.a);
        }
    }
}
