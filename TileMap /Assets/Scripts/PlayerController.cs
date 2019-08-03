using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float jumpForce;
    public Text scoreText;
    public Text winText;
    public Text livesText;
    public Text loseText;
    public Text countdownText;
    public Text restartText;


    private bool facingRight = true;
    private bool restart;
    private bool gameOver;
    private bool timerIsActive = true;


    private int score;
    private int lives;
    public Vector3 localScale;

    float currentTime = 0f;
    float startingTime = 45f;
    float speed;






    Animator anim;

    void Start()
    {
        gameOver = false;
        restart = false;
        rb2d = GetComponent<Rigidbody2D>();
        score = 0;
        lives = 3;
        speed = 10;
        SetScoreText();
        SetLivesText();
        winText.text = "";
        loseText.text = "";
        restartText.text = "";
        anim = GetComponent<Animator>();
        transform.localScale += new Vector3(0.1F, 0, 0);
        currentTime = startingTime;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            anim.SetInteger("State", 2);
        }


        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }

        if (gameOver)
        {
            restartText.text = "Press 'S' for Restart";
            restart = true;
            speed = 0;
            timerIsActive = false;
        }

        if (timerIsActive)
        {
            currentTime -= 1 * Time.deltaTime;
            countdownText.text = currentTime.ToString("0");
        }

        if (currentTime <= 10)
        {
            countdownText.color = Color.red;
        }
        else if (currentTime > 10)
        {
            countdownText.color = Color.black;
        }



            if (currentTime <= 0)
            {
                currentTime = 0;
                loseText.text = "You Lose!";
                gameOver = true;
                
        }
        }


   

    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(moveHorizontal, 0);

        rb2d.AddForce(movement * speed);

        if (facingRight == false && moveHorizontal > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveHorizontal < 0)
        {
            Flip();
        }

        if (Input.GetKey("escape"))
            Application.Quit();


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            score = score + 1;
            SetScoreText();
            FindObjectOfType<AudioManager>().Play("Coin");
        }

        else if (other.gameObject.CompareTag("Gold"))
        {
            other.gameObject.SetActive(false);
            SetScoreText();
            FindObjectOfType<AudioManager>().Play("Coin");
            currentTime += 10;




        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            lives = lives - 1;
            SetScoreText();
            SetLivesText();
            FindObjectOfType<AudioManager>().Play("Enemy");
        }

        if (other.gameObject.CompareTag("Bad"))
        {
            other.gameObject.SetActive(false);
            lives = lives - 1;
            SetScoreText();
            SetLivesText();
            currentTime -= 1 * Time.deltaTime + 10;



            FindObjectOfType<AudioManager>().Play("Enemy");
        }

        if (score == 5)
        {
            transform.position = new Vector3(0f, (float)13.49, 3.0f);
            GameObject.Find("Main Camera").transform.position = new Vector3(0.0f, 16.02f, -10.48f );
            lives = 3;
            SetLivesText();

        }

    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
        if (score >= 9)
        {
            winText.text = "You Win!";
            FindObjectOfType<AudioManager>().Play("PlayerWin");
            FindObjectOfType<AudioManager>().Stop("Theme");
            gameOver = true;
            restart = true;
        }
    }
    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
        if (lives == 0)
        {
           
            SetLivesText();
            loseText.text = "You Lose!";
            gameOver = true;
            restart = true;




        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            anim.SetInteger("State", 0);

            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                FindObjectOfType<AudioManager>().Play("Jump");
            }


        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }


}