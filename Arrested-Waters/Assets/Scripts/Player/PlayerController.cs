using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public BoatController boat;
    public bool onBoat = false;

    public float moveSpeed;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 newMovement;
    private Animator animator;
    private SpriteRenderer sprite;
    public bool isAiming;
    public GameObject gun;
    public GameObject axe;
    public PolygonCollider2D interaction_collider;
    public Texture2D cursorArrow;
    public Texture2D cursorCrosshair;

    private bool isDodging;
    private bool canDodge = true;

    private void Start()
    {
        // DontDestroyOnLoad(gameObject);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        boat = GameManager.instance.boat;
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
    }

    void Update()
    {
        // Get x-value of movement. Works with arrow keys and WASD.
        movement.x = Input.GetAxis("Horizontal");
        // Get y-value of movement. Works with arrow keys and WASD.
        movement.y = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift))
            Dodge();
        
    }

    void FixedUpdate()
    {
        if (!isDodging)
        {
            if (!isAiming)
            {
                newMovement = rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime;
                if (onBoat)
                    newMovement += new Vector2(boat.GetComponent<Rigidbody2D>().velocity.x, boat.GetComponent<Rigidbody2D>().velocity.y) * Time.fixedDeltaTime;
                rb.MovePosition(newMovement);
                PutGunDown();
            }

            else
            {
                newMovement = rb.position + movement.normalized * moveSpeed / 2 * Time.fixedDeltaTime;
                if (onBoat)
                    newMovement += new Vector2(boat.GetComponent<Rigidbody2D>().velocity.x, boat.GetComponent<Rigidbody2D>().velocity.y) * Time.fixedDeltaTime;
                rb.MovePosition(newMovement);
            }
        }

    }

    private void Dodge()
    {
        if (canDodge)
        {
            canDodge = false;
            rb.AddForce(movement.normalized * 10, ForceMode2D.Impulse);
            isDodging = true;
            StartCoroutine(dodgeCooldownCorotine());
        }
    }

    IEnumerator dodgeCooldownCorotine()
    {
        yield return new WaitForSeconds(0.1f);
        isDodging = false;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.2f);
        canDodge = true;
        
    }

    private void AimGun()
    {
        //gun.SetActive(true);  //Axe Animation is now controlling the gun activation
 
    }
    private void PutGunDown()
    {
        //gun.SetActive(false);
        interaction_collider.enabled = true;
        
    }
}
