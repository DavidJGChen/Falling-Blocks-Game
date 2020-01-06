using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlockKiller : MonoBehaviour
{
    public float Cooldown = 1f;
    private float coolDownTimer;
    private new Collider2D collider2D;
    private RaycastHit2D[] results = new RaycastHit2D[4];
    // Start is called before the first frame update
    void Start()
    {
        collider2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (coolDownTimer <= 0) {
            if (!GameOver.SharedInstance.IsGameOver && Input.GetKeyDown(KeyCode.Space)) {
                coolDownTimer = Cooldown;
                Array.Clear(results, 0, results.Length);
                if (collider2D.Cast(Vector2.zero, results) > 1) {
                    foreach (var item in results) {
                        if (item.collider != null) {
                            Destroy(item.collider.gameObject);
                        }
                    }
                }
            }
        }
        else {
            coolDownTimer -= Time.deltaTime;
        }
    }
}
