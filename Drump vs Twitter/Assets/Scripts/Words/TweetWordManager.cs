using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweetWordManager : Singleton<TweetWordManager> {

	private List<TweetWord> tweetWords;

	/// <summary>
	/// The name of the file containing the tweet words data.
	/// </summary>
	private const string FILE_NAME_TWEET_WORDS = "TweetWords/Tweets";

	/// <summary>
	/// Loads the words from the tweet word data file.
	/// </summary>
	private void LoadWords(){
		tweetWords = new List<TweetWord> ();

		TextAsset wordsData = Resources.Load (FILE_NAME_TWEET_WORDS) as TextAsset;
		string[] lines = Regex.Split (wordsData.text, "\r\n|\r|\n");

		//ignore first line of the file, since it is a 
		for (int i = 1; i < lines.Length; i++) {
			TweetWord lineWord = TweetWordFromCSVLine (lines [i]);

			if (lineWord != null) {
				tweetWords.Add (lineWord);
			}
		}
	}

	/// <summary>
	/// Creates a TweetWord object from a CSV-Line and returns it.
	/// If there is no TweetWord in the line this method will return null.
	/// </summary>
	/// <returns>The TweetWord created from the CSV line.</returns>
	/// <param name="line">A CSV-Line</param>
	private TweetWord TweetWordFromCSVLine(string line){
		string[] values = line.Split (';');

		//TODO: consider language - for the time being only english!

		//it is only a word if everything needed is set
		if (values [0].Length <= 0 || values[1].Length <= 0 || values[2].Length <= 0 || values[4].Length <= 0) {
			return null;
		}

		//map type
		TweetWordType type;
		switch (values [0]) {
		case "Negativ":
			type = TweetWordType.NEGATIVE;
			break;
		case "Neutral":
			type = TweetWordType.NEUTRAL;
			break;
		case "Positiv":
			type = TweetWordType.POSITIVE;
			break;
		default:
			//invalid value at type position -> no TweetWord possible
			return null;
		}

		//map value
		int value;
		if(!Int32.TryParse(values[1], out value)){
			return null;
		}

		//map word phrase
		TweetWordPhrase phrase;
		switch (values [2]) {
		case "Subjektiv":
			phrase = TweetWordPhrase.SUBJECT;
			break;
		case "Fuellsaetze":
			phrase = TweetWordPhrase.FILL;
			break;
		case "Verben":
			phrase = TweetWordPhrase.VERB;
			break;
		default:
			return null;
		}

		return new TweetWord (type, phrase, values [4], value);
	}

	/// <summary>
	/// Gets a random word.
	/// </summary>
	/// <returns>The random word.</returns>
	public TweetWord GetRandomWord(){
		if (tweetWords == null) {
			LoadWords ();
		}
		return tweetWords[UnityEngine.Random.Range(0, tweetWords.Count)];
	}
}
