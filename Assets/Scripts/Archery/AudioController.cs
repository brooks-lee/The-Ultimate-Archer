using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController {
    private AudioSource audioSourceBow, audioSourceScoreBoard, audioSourceBalloon, bgAudioSource;
    private GameObject gameController;
    public AudioController()
    {
        gameController = GameObject.Find("Game Controller");
        bgAudioSource = gameController.AddComponent<AudioSource>();
        bgAudioSource.clip = Resources.Load<AudioClip>("Sounds/BackgroundMusic.wav");
        audioSourceBow = gameController.AddComponent<AudioSource>();
        audioSourceBow.clip= Resources.Load<AudioClip>("Sounds/BowTension.wav");
        audioSourceScoreBoard = gameController.AddComponent<AudioSource>();
        audioSourceScoreBoard.clip = Resources.Load<AudioClip>("Sounds/ArrowStrike.wav");
        audioSourceBalloon = gameController.AddComponent<AudioSource>();
        audioSourceBalloon.clip = Resources.Load<AudioClip>("Sounds/BalloonPop.mp3");

        bgAudioSource.Play();
    }

    public void playBowSound()
    {
        if(!audioSourceBow.isPlaying)
            audioSourceBow.Play();
    }

    public void stopBowSound()
    {
        audioSourceBow.Stop();
    }
    public void playScoreBoardSound()
    {
        if (!audioSourceScoreBoard.isPlaying)
            audioSourceScoreBoard.Play();
    }

    public void playBalloonSound()
    {
        if (!audioSourceBalloon.isPlaying)
            audioSourceBalloon.Play();
    }
}
