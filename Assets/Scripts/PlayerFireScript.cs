using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering.Universal;

public class PlayerFireScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isHoming = false;
    public Vector3 direction;
    private GameObject target = null;
    private SpriteRenderer spriteRenderer;
    private bool canHome = true;
    public Animator animator;
    public AudioSource audioSource;
    public Light2D spotLight;
    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer.material.color = new Color(1, 1, 1, 0);
        spotLight.color = Color.red;
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(spriteRenderer.material.color != new Color(1, 1, 1, 1))
        {
            spriteRenderer.material.color += new Color(0, 0, 0, 0.05f);
        }
        if (isHoming && canHome)
        {
            if(target != null)
            {
                // Get the current position of the object and the target
                Vector3 currentPosition = transform.position;
                Vector3 targetPosition = target.transform.position;

                // Calculate the direction to the target
                Vector3 direction = (targetPosition - currentPosition).normalized;

                // Move the object towards the target
                transform.position = Vector3.MoveTowards(currentPosition, targetPosition, 15 * Time.deltaTime);
            }
            else
            {
                isHoming = false;
                rb.velocity = direction.normalized * 15;
            }
        }
        else
        {   
            rb.AddForce(direction.normalized,ForceMode2D.Impulse);
            //rb.velocity = direction.normalized * 15;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            spriteRenderer.material.color = new Color(1, 1, 1, 0);
            audioSource.Play();
            Vector2 normal = collision.contacts[0].normal; // Get the surface normal at the collision point
            direction = Vector2.Reflect(rb.velocity.normalized, normal); // Reflect the velocity
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject, 0.1f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isHoming && canHome)
        {
            isHoming = true;
            spotLight.color = Color.yellow;
            rb.velocity = Vector3.zero;
            target = collision.gameObject;
            if (!animator.GetBool("isHoming") && canHome)
            {
                animator.SetBool("isHoming", true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isHoming = false;
            target = null;
            animator.SetBool("isHoming", false);
        }
    }
}
