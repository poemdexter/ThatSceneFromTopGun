using UnityEngine;
using System.Collections;

public class BallBounce : MonoBehaviour 
{
    public float speed;
    public float maxVelocity;
    public float gravity;
    private bool goingUp, goingRight;
    private GameController game;
    private bool playing;
    public Vector3 spawnpoint;

	void Start () 
    {
        goingUp = true;
        goingRight = true;
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	
	void Update () 
    {
        if (rigidbody2D.velocity.sqrMagnitude > maxVelocity)
        {
            //smoothness of the slowdown is controlled by the 0.99f, 
            //0.5f is less smooth, 0.9999f is more smooth
            rigidbody2D.velocity *= 0.5f;
        }
	}

    public void Play()
    {
        playing = true;
        int x = (Random.Range(0,2) == 1) ? 1 : -1;
        rigidbody2D.velocity = (speed * new Vector2(x, -1f));
        rigidbody2D.gravityScale = gravity;
    }

    private float GetHorizDirection()
    {
        return (goingRight) ? 1 : -1;
    }

    private float GetVertDirection()
    {
        return (goingUp) ? 1 : -1;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("ceiling"))
        {
            goingUp = !goingUp;
        } 
        else if (col.gameObject.CompareTag("wall"))
        {
            goingRight = !goingRight;
        }
        else if (col.gameObject.CompareTag("sand"))
        {
            game.Score(transform.position);
            rigidbody2D.velocity = Vector2.zero;
            playing = false;
        }
        else if (col.gameObject.CompareTag("P1") || col.gameObject.CompareTag("P3"))
        {
            goingUp = true;
            goingRight = true;
        }
        else if (col.gameObject.CompareTag("P2") || col.gameObject.CompareTag("P4"))
        {
            goingUp = true;
            goingRight = false;
        }
    }

    public void Reset()
    {
        transform.position = spawnpoint;
        rigidbody2D.gravityScale = 0;
    }
}
