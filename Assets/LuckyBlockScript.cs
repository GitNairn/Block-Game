using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine.Video;
using UnityEngine.UI;

public class LuckyBlockScript : MonoBehaviour
{
    private List<Action> effects;
    public System.Random rand = new System.Random();
    public TMP_Text log;
    public VideoPlayer videoPlayer;
    public RawImage videoScreen;
    public GameObject player;
    public GameObject camera;
    public VideoClip catVideo;
    public VideoClip spongebobVideo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        effects = new List<Action>()
        {
            insult,
            catLaugh,
            flipCamera,
            compliment,
            //spongebob

        };

        if (videoPlayer != null)
            videoPlayer.loopPointReached += onVideoEnd;
        if (videoScreen != null)
            videoScreen.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            int effectIndex = rand.Next(effects.Count);
            effects[effectIndex]();
            Destroy(gameObject);
        }
    }

    public void insult()
    {
        List<String> insults = new List<String>()
        {
            "You smell",
            "You suck at this game",
            "Your feet smell like cat food",
            "Get a job",
            "I bet you suck your toes",
            "lol",
            "Touch grass",
            "ew"
        };
        
        int insultIndex = rand.Next(insults.Count);
        String insult = insults[insultIndex];
        log.text = insult;
    }

    public void catLaugh()
    {
        videoPlayer.clip = catVideo;
        videoScreen.gameObject.SetActive(true);
        videoPlayer.Play();
    }

    void onVideoEnd(VideoPlayer vp)
    {
        if (videoScreen != null)
            videoScreen.gameObject.SetActive(false);
    }

    void flipCamera()
    {
        camera.GetComponent<CameraScript>().flip();
    }

    void compliment()
    {
        List<String> compliments = new List<String>()
        {
            "You're doing great!",
            "Keep it up!",
            "You're a star!",
            "Amazing job!",
            "You're incredible!",
            "Fantastic work!",
            "You're a legend!",
            "Brilliant effort!"
        };
        int complimentIndex = rand.Next(compliments.Count);
        String compliment = compliments[complimentIndex];
        log.text = compliment;
    }

    public void spongebob()
    {
        videoPlayer.clip = spongebobVideo;
        videoScreen.gameObject.SetActive(true);
        videoPlayer.Play();
    }
}
