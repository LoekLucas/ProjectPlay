using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackScript : MonoBehaviour
{
    public GameObject playerFire;
    public GameObject playerSword;
    private Vector2 mousePos;
    private bool isFiring = false;
    public float spreadAngle = 10f; // Angle to offset bullets
    private float timer = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
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
}
