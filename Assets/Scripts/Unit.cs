using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public static bool IsUnit(GameObject go) {
        return go.GetComponent<Unit>() != null || go.GetComponentsInParent<Unit>().Length > 0;
    }
}
