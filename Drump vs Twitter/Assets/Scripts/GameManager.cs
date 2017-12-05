using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {
	[SerializeField]
	private Text scoreText;
	[SerializeField]
	private Text secondsLeftText;

	[SerializeField]
	private GameObject tweetWordsContainer;
	[SerializeField]
	private GameObject tweetWordPrefab;

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

		SpawnWords ();
	}

	/// <summary>
	/// Counts the time to play down.
	/// </summary>
	private void TimeDown(){
		if(!paused && SecondsLeft > 0){
			SecondsLeft -= Time.deltaTime;
		}
	}

	/// <summary>
	/// Spawns all the words.
	/// </summary>
	private void SpawnWords(){
		for (int i = 0; i < 10; i++) {
			createWordObject (TweetWordManager.Instance.GetRandomWord ());
		}
	}

	/// <summary>
	/// Creates a new Word Game Object.
	/// </summary>
	/// <returns>The word object.</returns>
	/// <param name="word">The Tweet Word to create the Word Game Object from.</param>
	private GameObject createWordObject(TweetWord word){
		GameObject newWordObject = Instantiate(tweetWordPrefab);
		newWordObject.transform.SetParent (tweetWordsContainer.transform);
		newWordObject.transform.localScale = new Vector3 (1f, 1f, 1f);
		newWordObject.transform.position = new Vector3 (UnityEngine.Random.Range(-0.8f, 0.8f), UnityEngine.Random.Range(-0.8f, 0.8f), 0f);

		Color buttonColor = Color.white;
		switch (word.Phrase) {
		case TweetWordPhrase.FILL:
			buttonColor = Color.blue;
			break;
		case TweetWordPhrase.VERB:
			buttonColor = Color.green;
			break;
		case TweetWordPhrase.SUBJECT:
			buttonColor = Color.red;
			break;
		}
		newWordObject.GetComponent<CanvasRenderer> ().SetColor (buttonColor);
		ColorBlock cb = newWordObject.GetComponent<Button> ().colors;
		cb.normalColor = buttonColor;
		cb.highlightedColor = new Color (buttonColor.r * 0.8f, buttonColor.g * 0.8f, buttonColor.b * 0.8f);
		cb.pressedColor = new Color (buttonColor.r * 0.6f, buttonColor.g * 0.6f, buttonColor.b * 0.6f);
		cb.disabledColor = Color.gray;
		newWordObject.GetComponent<Button> ().colors = cb;

		Text wordText = newWordObject.transform.Find ("WordText").GetComponent<Text>();
		wordText.text = word.Word;

		return newWordObject;
	}
}