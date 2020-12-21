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
    GameObject nameTag;

    void Start() {
        selectors = CreateSelectors();
        foreach (GameObject go in selectors) {
            go.SetActive(false);
        }
    }

    void Update() {
        DetectSelection();
        DetectHover();
    }

    void DetectHover() {
        ClearHoveredUnit();

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null && Unit.IsUnit(hit.collider.gameObject)) {
            GameObject go = GetRootGameObject(hit.collider);
            SetHoveredUnit(go);
            UpdateNameTagPosition(go);
        }
    }

    void DetectSelection() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null) {
                GameObject go = GetRootGameObject(hit.collider);
                PlaceSelectorsAroundGameObject(go, selectors);
                selectedUnit = go;
            } else {
                DisableSelectors(selectors);
                selectedUnit = null;
            }

        }

        // Update selected unit features like selector distance if its attributes change
        if (selectedUnit != null) {
            if (selectedUnit.transform.hasChanged) {
                PlaceSelectorsAroundGameObject(selectedUnit, selectors);
            }
        }
    }

    void SetHoveredUnit(GameObject unit) {
        Color c = unit.GetComponentInChildren<SpriteRenderer>().color;
        SetUnitColor(unit, new Color(c.r, c.g, c.b, .8f));

        if (nameTag == null) {
            nameTag = CreateNameTag(unit);
        } else {
            nameTag.gameObject.SetActive(true);
        }

        hoveredUnit = unit;
    }

    void ClearHoveredUnit() {
        if (hoveredUnit != null) {
            Color c = hoveredUnit.GetComponentInChildren<SpriteRenderer>().color;
            SetUnitColor(hoveredUnit, new Color(c.r, c.g, c.b, 1));
            nameTag.SetActive(false);
            hoveredUnit = null;
        }
    }

    /***** STATIC FUNCTIONS *****/
    public static void SetUnitColor(GameObject unit, Color c) {
        SpriteRenderer[] allSr = unit.GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < allSr.Length; ++i) {
            SpriteRenderer sr = allSr[i];
            sr.color = new Color(c.r, c.g, c.b, c.a);
        }
    }

    /***** HELPER FUNCTIONS *****/
    private GameObject CreateNameTag(GameObject unit) {
        GameObject nameTagGo = new GameObject("Name Tag");
        nameTagGo.transform.parent = transform;
        TextMesh unitNameText = nameTagGo.AddComponent<TextMesh>();
        unitNameText.text = unit.GetComponent<INameable>().GetName();
        unitNameText.offsetZ = 10;
        unitNameText.alignment = TextAlignment.Center;
        unitNameText.anchor = TextAnchor.LowerCenter;
        unitNameText.fontSize = 200;
        unitNameText.characterSize = .02f;
        unitNameText.color = Color.black;
        return nameTagGo;
    }

    private void UpdateNameTagPosition(GameObject unit) {
        TextMesh unitNameText = nameTag.GetComponent<TextMesh>();
        Bounds bo = new Bounds(unit.transform.position, Vector2.zero);
        foreach (Renderer r in unit.GetComponentsInChildren<SpriteRenderer>()) {
            bo.Encapsulate(r.bounds);
        }
        unitNameText.transform.position = new Vector2(
            bo.center.x, bo.center.y + bo.extents.y
        );
    }

    private GameObject GetRootGameObject(Collider2D coll) {
        Transform tf = coll.transform;
        while (tf.parent != null) {
            tf = tf.transform.parent;
        }
        return tf.gameObject;
    }

    private void PlaceSelectorsAroundGameObject(GameObject go, GameObject[] selectors) {
        Transform tf = go.transform;

        Bounds bo = new Bounds(tf.position, Vector2.zero);
        foreach (Renderer r in go.GetComponentsInChildren<SpriteRenderer>()) {
            bo.Encapsulate(r.bounds);
        }
        selectors[0].transform.position = new Vector2(bo.center.x - bo.extents.x - .02f, bo.center.y + bo.extents.y);
        selectors[1].transform.position = new Vector2(bo.center.x + bo.extents.x, bo.center.y + bo.extents.y);
        selectors[2].transform.position = new Vector2(bo.center.x - bo.extents.x, bo.center.y - bo.extents.y);
        selectors[3].transform.position = new Vector2(bo.center.x + bo.extents.x, bo.center.y - bo.extents.y);

        foreach (GameObject selector in selectors) {
            selector.SetActive(true);
        }
    }

    private void DisableSelectors(GameObject[] selectors) {
        foreach (GameObject selector in selectors) {
            selector.SetActive(false);
        }
    }

    private GameObject[] CreateSelectors() {
        GameObject[] res = new GameObject[4];
        res[0] = new GameObject("Top Left Selector");
        res[1] = new GameObject("Top Right Selector");
        res[1].transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
        res[2] = new GameObject("Bottom Left Selector");
        res[2].transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
        res[3] = new GameObject("Bottom Right Selector");
        res[3].transform.localRotation = Quaternion.Euler(0f, 0f, 180f);

        GameObject selectorHolder = new GameObject("Selectors");
        foreach (GameObject go in res) {
            go.AddComponent<SpriteRenderer>().sprite = selectorSprite;
            go.transform.parent = selectorHolder.transform;
        }

        return res;
    }
}
