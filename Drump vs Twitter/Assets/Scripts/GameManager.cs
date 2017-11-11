using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	[SerializeField]
	private Text scoreText;
	[SerializeField]
	private Text secondsLeftText;

	private float secondsLeft;
	private int score;

	private bool paused = false;

	private int Score {
		get{
			return score;
		}
		set{
			score = value;
			scoreText.text = "Score: " + score.ToString ();
		}
	}

	private float SecondsLeft {
		get {
			return secondsLeft;
		}

		set{
			secondsLeft = value;
			if (secondsLeft <= 0) {
				secondsLeft = 0;
			}
			int seconds = Convert.ToInt32 (secondsLeft) % 60;
			int minutes = Convert.ToInt32 (secondsLeft) / 60;
			secondsLeftText.text = string.Format ("Time: {0}:{1:00}", minutes, seconds);
		}
	}

	// Use this for initialization
	void Start () {
		InitializeGame ();
	}
	
	// Update is called once per frame
	void Update () {
		TimeDown ();
	}

	/// <summary>
	/// Initializes the game.
	/// </summary>
	private void InitializeGame(){
		SecondsLeft = 30;
		Score = 0;
	}

	/// <summary>
	/// Counts the time to play down.
	/// </summary>
	private void TimeDown(){
		if(!paused && SecondsLeft > 0){
			SecondsLeft -= Time.deltaTime;
		}
	}
}