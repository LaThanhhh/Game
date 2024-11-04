using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Image SettingImage;
    public Image BestScoreImage;
    public Image CreditImage;
    public Image VolumeImage;
    public Text scoreText;

    public AudioSource myAudio;
    public Slider volumeSlider;

    public float bestScore;


    void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }


    void Update()
    {
        bestScore = PlayerPrefs.GetFloat("MyScore");
        scoreText.text = bestScore.ToString();

        myAudio.volume = volumeSlider.value;
    }
    public void Setting()
    {
        SettingImage.gameObject.SetActive(true);
    }
    public void Exit()
    {
        SettingImage.gameObject.SetActive(false);
    }

    public void BestScore()
    {
        BestScoreImage.gameObject.SetActive(true);
    }
    public void ExitBestScore()
    {
        BestScoreImage.gameObject.SetActive(false);
    }
    public void Credit()
    {
        CreditImage.gameObject.SetActive(true);
    }
    public void ExitCredit()
    {
        CreditImage.gameObject.SetActive(false);
    }

    public void Volume()
    {
        VolumeImage.gameObject.SetActive(true);
    }
    public void ExitVolume()
    {
        VolumeImage.gameObject.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Level");
    }

}
