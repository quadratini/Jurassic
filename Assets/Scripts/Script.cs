using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script : MonoBehaviour
{
    public const int SIZE = 7;

    public Color colorOne = new Color(0.0f, 0.1f, 0.0f);
    public Color colorTwo = new Color(1.0f, 0.0f, 0.0f);
    public GameObject wallPrefab;
    public GameObject camera;
    public GameObject[,] squares;
    // Start is called before the first frame update
    void Start()
    {
        squares = new GameObject[SIZE, SIZE];
        StartCoroutine(Waiter());
    }

    // Update is called once per frame
    void Update() {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2Int tilePos = new Vector2Int((int) Mathf.Floor(worldPosition.x + 0.5f), (int) Mathf.Floor(worldPosition.y + 0.5f));

        if (Input.GetMouseButtonDown(0)) {
            if (tilePos.x >= 0 && tilePos.x < SIZE && tilePos.y < SIZE && tilePos.y >= 0) {
                GameObject tile = squares[tilePos.x, tilePos.y]; // this is the square
                if (tile != null) {
                    SpriteRenderer spriteRenderer = tile.GetComponent<SpriteRenderer>();
                    if (spriteRenderer.color.Equals(colorTwo)) {
                        spriteRenderer.color = colorOne;
                    } else {
                        spriteRenderer.color = colorTwo;
                    }
                    Debug.Log(tilePos + " " + tile + " " + worldPosition);
                }
            }
        }
        Debug.Log(Input.mousePosition + " " + worldPosition);
    }

    IEnumerator Waiter() {
        // camera.transform.position = new Vector3(SIZE / 2, SIZE / 2, -1);
        for (int i = 0; i < SIZE; i++) {
            for (int j = 0; j < SIZE; j++) {
                if (i == 0 || i == SIZE - 1 || j == 0 || j == SIZE - 1 || i == j || i + j == SIZE - 1) {
                    GameObject gameObject = GameObject.Instantiate(wallPrefab, new Vector3(i, j, 0), Quaternion.identity);
                    squares[i, j] = gameObject;
                    yield return new WaitForSeconds(0);
                }
            }
        }
    }
}
