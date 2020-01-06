using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    private float speed;
    private Vector2 dir;
    public Vector2 minMaxSpeed;
    private float rotMod;
    private float playerHalfHeight;
    private Vector2 screenHalfSize;
    private float speedMod = 1f;
    // Start is called before the first frame update
    private bool dynamicRigidBody = false;
    void Start()
    {
        playerHalfHeight = transform.localScale.y * 0.5f;
        screenHalfSize = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
        speed = Mathf.Lerp(minMaxSpeed.x, minMaxSpeed.y, Difficulty.GetDifficultyPercent()) / (transform.localScale.x + 1);
        var rotation = transform.rotation.eulerAngles.z - 90;
        dir = new Vector2(Mathf.Cos(rotation * Mathf.Deg2Rad), Mathf.Sin(rotation * Mathf.Deg2Rad));
        rotMod = Random.Range(-5f,5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameOver.SharedInstance.IsGameOver) {
            if (dynamicRigidBody) {
                dynamicRigidBody = false;
                GetComponent<Rigidbody2D>().isKinematic = true;
                GetComponent<Collider2D>().isTrigger = true;
            }
            transform.Translate(dir * speed * Time.deltaTime, Space.World);
            transform.Rotate(0,0, speed * speed * rotMod * Time.deltaTime);

            if (transform.position.y < - (screenHalfSize.y + playerHalfHeight * 1.5f)) {
                Destroy(gameObject);
            }
        }
        else {
            if (!dynamicRigidBody) {
                dynamicRigidBody = true;
                GetComponent<Rigidbody2D>().isKinematic = false;
                GetComponent<Collider2D>().isTrigger = false;
            }
        }
    }
}
