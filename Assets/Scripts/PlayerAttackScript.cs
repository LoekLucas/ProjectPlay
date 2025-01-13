using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class PlayerAttackScript : MonoBehaviour
{
    public GameObject playerFire;
    public GameObject playerSword;
    private Vector2 mousePos;
    private bool isFiring = false;
    public float spreadAngle = 10f; // Angle to offset bullets
    private float timer = 0;
    public GameObject aimIndicator; // Aim visualization object
    private int range = 2;

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (aimIndicator != null)
        {
            Light2D light = aimIndicator.GetComponent<Light2D>();
            if (light == null)
            {
                Debug.LogError("Aim indicator does not have a Light2D component!");
                return;
            }

            Color currentColor = light.color;
            float currentAlpha = currentColor.a;

            if (IsMouseWithinCircle(range))
            {
                // Mouse is within the circle, decrease alpha towards 0
                if (!Mathf.Approximately(currentAlpha, 0f))
                {
                    float newAlpha = Mathf.MoveTowards(currentAlpha, 0f, Time.deltaTime * 2f); // Adjust speed
                    light.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
                }
                else
                {
                    UpdateAimIndicator();
                }
            }
            else
            {
                UpdateAimIndicator();
                // Mouse is outside the circle, increase alpha towards 1
                if (!Mathf.Approximately(currentAlpha, 1f))
                {
                    float newAlpha = Mathf.MoveTowards(currentAlpha, 1f, Time.deltaTime * 2f); // Adjust speed
                    light.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
                }
            }
        }

        if (isFiring && timer > 0.075f)
        {
            // Convert mousePos from screen space to world space
            Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);

            // Calculate direction from firePoint to mouse position
            Vector2 firePointPos = transform.position;
            Vector2 direction = (worldMousePos - firePointPos);

            // Instantiate the two bullets
            FireBullet(direction, -spreadAngle); // Bullet going slightly to the left
            FireBullet(direction * 2.5f, 0); // Bullet going centered
            FireBullet(direction, spreadAngle);  // Bullet going slightly to the right
            timer = 0;
        }
    }
    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isFiring = true;
        }
        if (context.canceled)
        {
            isFiring = false;
        }
    }
    public void SetMousePos(InputAction.CallbackContext context)
    {
        mousePos = context.ReadValue<Vector2>();
    }
    void FireBullet(Vector2 direction, float angleOffset)
    {
        // Rotate the direction by the given angleOffset
        Vector2 newDirection = RotateVector(direction, angleOffset);

        // Instantiate the bullet prefab
        GameObject bullet = Instantiate(playerFire, transform.position, Quaternion.identity);

        bullet.GetComponent<PlayerFireScript>().direction = newDirection;
    }

    Vector2 RotateVector(Vector2 v, float angle)
    {
        float rad = angle * Mathf.Deg2Rad;  // Convert angle to radians
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);
        return new Vector2(cos * v.x - sin * v.y, sin * v.x + cos * v.y);
    }
    void UpdateAimIndicator()
    {
        if (aimIndicator != null)
        {
            // Convert mousePos from screen space to world space
            Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);

            // Calculate direction and target angle
            Vector2 direction = (worldMousePos - (Vector2)transform.position).normalized;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

            // Get the current rotation of the aim indicator
            Quaternion currentRotation = aimIndicator.transform.rotation;

            // Calculate the target rotation
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

            // Smoothly rotate towards the target rotation
            aimIndicator.transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, Time.deltaTime * 5f); // Adjust speed multiplier (5f) as needed

            aimIndicator.transform.position = (Vector2)transform.position + direction; // Offset from the player
        }
    }


    // Check if the mouse is within a circle around the player
    bool IsMouseWithinCircle(float radius)
    {
        // Convert mousePos from screen space to world space
        Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);

        // Calculate the distance between the player and the mouse position
        float distance = Vector2.Distance(worldMousePos, transform.position);

        // Check if the distance is less than or equal to the radius
        return distance <= radius;
    }

}
