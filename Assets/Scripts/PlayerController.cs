using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    private float movementX;
    private float movementY;
    public float speed = 10f;
    private int count;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    // Power-up states
    private float originalSpeed;
    private Coroutine currentPowerUpCoroutine;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
        
        // Store original values
        originalSpeed = speed;
    }

    public void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            winTextObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    public void AddScore(int points)
    {
        count += points;
        SetCountText();
    }

    public void ApplySpeedBoost(float multiplier, float duration)
    {
        if (currentPowerUpCoroutine != null)
        {
            StopCoroutine(currentPowerUpCoroutine);
        }
        currentPowerUpCoroutine = StartCoroutine(SpeedBoostCoroutine(multiplier, duration));
    }

    private IEnumerator SpeedBoostCoroutine(float multiplier, float duration)
    {
        speed *= multiplier;
        yield return new WaitForSeconds(duration);
        speed = originalSpeed;
    }
}
