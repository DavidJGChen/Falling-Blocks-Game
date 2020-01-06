using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed;
    public Vector2 minMaxSpeed;
    public event Action OnPlayerDeath;
    private float screenHalfWidth;
    private Vector2 input;
    private Vector2 target;
    private bool wasd;
    // Start is called before the first frame update
    private void Awake() {
        Screen.SetResolution(600, 1000, FullScreenMode.Windowed);
        Application.targetFrameRate = 60;
        // QualitySettings.vSyncCount = 0;
    }
    void Start()
    {
        var playerHalfWidth = transform.localScale.x * 0.5f;
        screenHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        screenHalfWidth += playerHalfWidth;
        target = transform.position;
        speed = minMaxSpeed.x;
    }

    // Update is called once per frame
    void Update()
    {
        input = Vector2.zero;
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) {
            wasd = true;
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
        }

        if (Input.GetMouseButtonDown(1)) {
            wasd = false;
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        speed = Mathf.Lerp(minMaxSpeed.x, minMaxSpeed.y, Difficulty.GetDifficultyPercent());
    }

    private void FixedUpdate() {
        if (wasd) {
            var velocity = input.normalized * speed;
            transform.Translate(velocity * Time.deltaTime);

            if (transform.position.x < -screenHalfWidth) {
                transform.position = new Vector2(screenHalfWidth,transform.position.y);
            }
            if (transform.position.x > screenHalfWidth) {
                transform.position = new Vector2(-screenHalfWidth,transform.position.y);
            }
        }
        else {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Falling Block")) {
            OnPlayerDeath();
            Destroy(gameObject);
        }
    }
}
