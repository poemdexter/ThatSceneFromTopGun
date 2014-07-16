using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
    public float speed;
    private bool running, runningRight;
    private Animator anim;
    private JumpPhysics2D jump;
    void Start()
    {
        anim = GetComponent<Animator>();
        jump = GetComponent<JumpPhysics2D>();

        if (CompareTag("P2") || CompareTag("P4"))
            transform.localScale = new Vector3(-1f, 1f, 1f);
    }

	void Update() 
    {
        if (Input.GetButtonDown(tag + "_Left"))
        {
            running = true;
            runningRight = false;
            transform.localScale = new Vector3(-1f, 1f, 1f);
        } 
        else if (Input.GetButtonDown(tag + "_Right"))
        {
            running = true;
            runningRight = true;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (Input.GetButtonUp(tag + "_Left") || Input.GetButtonUp(tag + "_Right"))
        {
            running = false;
            rigidbody2D.velocity = Vector2.zero;
        }
	    
        if (running && !CheckForHorizontalCollision())
        {
            if (!jump.IsJumping())
                anim.SetTrigger("Running");
            else
                anim.SetTrigger("Jumping");

            Vector2 dir = (runningRight) ? Vector2.right : -Vector2.right;
            rigidbody2D.velocity = (Time.deltaTime * speed * dir);
        } 
        else
        {
            if (!jump.IsJumping())
            {
                anim.SetTrigger("Idle");
                rigidbody2D.velocity = Vector2.zero;
            }
            else
                anim.SetTrigger("Jumping");
        }
    }

    public bool IsRunning()
    {
        return running;
    }

    private bool CheckForHorizontalCollision()
    {
        Vector2 dir = (runningRight) ? Vector2.right : -Vector2.right;
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, dir, this.renderer.bounds.extents.x + 0.2f);

        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.tag);

            if (hit.collider.gameObject.CompareTag("wall")) return true;

            if (CompareTag("P3") && hit.collider.gameObject.CompareTag("P1")) return true;
            if (CompareTag("P4") && hit.collider.gameObject.CompareTag("P2")) return true;
            if (CompareTag("P1") && hit.collider.gameObject.CompareTag("P3")) return true;
            if (CompareTag("P2") && hit.collider.gameObject.CompareTag("P4")) return true;
        }
        return false;
    }
}
