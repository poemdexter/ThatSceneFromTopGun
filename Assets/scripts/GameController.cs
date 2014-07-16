using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    private int team1Score, team2Score;
    private BallBounce ball;
    private bool playing;
    private bool jetfly;
    public GameObject jet;
    public float speed;
    public TextMesh t1Score, t2Score,winnerText;

    private Vector3 jetSpawn = new Vector3(10.5f, 3.5f, 0f);

    private void Start()
    {
        ball = GameObject.FindGameObjectWithTag("ball").GetComponent<BallBounce>();
        playing = false;
        Physics2D.IgnoreLayerCollision(9, 10);
    }

    private void Update()
    {
        if (!playing)
        {
            playing = true;
            StartCoroutine(StartPlaying());
        }

        if (Input.GetButtonDown("RESET"))
            Application.Quit();

        if (jetfly)
        {
            jet.transform.Translate(Time.deltaTime * speed * Vector3.left);
        }
    }

    private IEnumerator StartPlaying()
    {
        jetfly = true;
        yield return new WaitForSeconds(2f);
        ball.Reset();
        yield return new WaitForSeconds(2f);
        jetfly = false;
        winnerText.text = "";
        jet.transform.position = jetSpawn;
        ball.Play();
    }

    public void Score(Vector3 ballPosition)
    {
        if (ballPosition.x > 0)
        {
            team1Score++;
            if (team1Score == 10)
            {
                winnerText.text = "Team 1 Wins";
                Reset();
            }
            t1Score.text = "" + team1Score;
        }
        else
        {
            team2Score++;
            if (team2Score == 10)
            {
                winnerText.text = "Team 2 Wins";
                Reset();
            }
            t2Score.text = "" + team2Score;
        }
            
        StartCoroutine(StartPlaying());
    }

    private void Reset()
    {
        team1Score = 0;
        team2Score = 0;
        t1Score.text = "";
        t2Score.text = "";
    }
}
