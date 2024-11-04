using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{ 

    public bool jump = false;
    public bool slide = false;

    public GameObject trigger;
    public Animator animator;

    public float score = 0;


    public bool boost = false;
    public Rigidbody rbody;
    public CapsuleCollider myCollider;

    public bool death = false;

    public Image gameOverImg;
    public Text scoreText;
    public Text bestScoreText;
    public float lastScore;

    public Text countdownText;
    private bool gameStarted = false;

    public AudioManager audioManager;
    void Start()
    {

        animator = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
        myCollider = GetComponent<CapsuleCollider>();

        lastScore = PlayerPrefs.GetFloat("MyScore");

        audioManager = FindObjectOfType<AudioManager>();
        
        countdownText.gameObject.SetActive(true);
        StartCoroutine(StartCountdown());
    }
    void Update()
    {
        if(!gameStarted) return;
        scoreText.text = score.ToString();

        if (score > lastScore)
        {
            bestScoreText.text = "Best Score:" + score.ToString();
        }
        else
        {
            bestScoreText.text = "Your Score:" + score.ToString();
        }

        if (death == true)
        {
            gameOverImg.gameObject.SetActive(true);

           
        }
        if (score >= 100 && death != true)
        {
            transform.Translate(0, 0, 0.2f);
        }
        else if (score >= 200 && death != true)
        {
            transform.Translate(0, 0, 0.3f);
        }
        else if (death == true)
        {
            transform.Translate(0, 0, 0);
        }
        else
        {
            transform.Translate(0, 0, 0.1f);
        }
        if (boost == true)
        {
            transform.Translate(0, 0, 1f);
            myCollider.enabled = false;
            rbody.isKinematic = true;
        }
        else
        {
            myCollider.enabled = true;
            rbody.isKinematic = false;
        }


        if (Input.GetKey(KeyCode.UpArrow))
        {
            jump = true;

        }
        else { jump = false; }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            slide = true;
        }else { slide = false; }
        

        if (jump == true)
        {
            animator.SetBool("isJump", jump);
            transform.Translate(0, 0.2f, 0.1f);
        }
        else if (jump == false)
        {
            animator.SetBool("isJump", jump);

        }
        if (slide == true)
        {
            animator.SetBool("isSlide", slide);
            transform.Translate(0, 0, 0.1f);
            myCollider.height = 1f;
        }
        else if (slide == false)
        {
            animator.SetBool("isSlide", slide);
            myCollider.height = 2.35f;
        }
        trigger = GameObject.FindGameObjectWithTag("Obstacle");

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerTrigger")
        {
            Destroy(trigger.gameObject);
        }
        if (other.gameObject.tag == "Coin")
        {
            Destroy(other.gameObject, 0.5f);
            score += 5f;
        }
        if (other.gameObject.tag == "Boost")
        {
            Destroy(other.gameObject);
            StartCoroutine(BoostController());
        }
        if (other.gameObject.tag == "DeathPoint")
        {
            death = true;
            if (score > lastScore)
            {
                PlayerPrefs.SetFloat("MyScore", score);
            }
            audioManager.PlaySound("GameOver");
        }
    }
    IEnumerator BoostController()
    {
        boost = true;
        yield return new WaitForSeconds(3);
        boost = false;
    }
    IEnumerator StartCountdown()
    {
        countdownText.text = "3";
        yield return new WaitForSeconds(1f);

        countdownText.text = "2";
        yield return new WaitForSeconds(1f);

        countdownText.text = "1";
        yield return new WaitForSeconds(1f);

        countdownText.text = "Go!";
        yield return new WaitForSeconds(1f);

        countdownText.gameObject.SetActive(false); 
        gameStarted = true; 
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene(1);
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }

}