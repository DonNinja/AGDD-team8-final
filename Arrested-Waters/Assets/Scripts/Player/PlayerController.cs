using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    private SpriteRenderer sprite;
    private bool isAiming;
    public GameObject gun;


    private void Start()
    {
        // DontDestroyOnLoad(gameObject);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Get x-value of movement. Works with arrow keys and WASD.
        movement.x = Input.GetAxis("Horizontal");
        // Get y-value of movement. Works with arrow keys and WASD.
        movement.y = Input.GetAxis("Vertical");
        isAiming = Input.GetMouseButton(1);
    }

    void FixedUpdate()
    {
        //if (!GameManager.instance.isDead)
        //{
        if (!isAiming)
        {
            rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
            PutGunDown();
        }

        else
        {
            rb.MovePosition(rb.position + movement.normalized * moveSpeed / 2 * Time.fixedDeltaTime);
            AimGun();
        }
        // Set values for the animation.
        //animator.SetFloat("Horizontal", movement.x);
        //animator.SetFloat("Speed", movement.sqrMagnitude);

        //Rotate toward mouse
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        // }
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    private void AimGun()
    {
        gun.SetActive(true);
    }
    private void PutGunDown()
    {
        gun.SetActive(false);
    }
}