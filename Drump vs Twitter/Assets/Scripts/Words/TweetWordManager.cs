using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweetWordManager : Singleton<TweetWordManager> {

	private List<TweetWord> tweetWords;

	/// <summary>
	/// Loads the words.
	/// </summary>
	private void LoadWords(){
		tweetWords = new List<TweetWord> ();

		TweetWord word1 = new TweetWord (TweetWordType.NEGATIVE, TweetWordPhrase.SUBJECT, "Mexicans", 30);
		TweetWord word2 = new TweetWord (TweetWordType.POSITIVE, TweetWordPhrase.SUBJECT, "Trump", 30);

		TweetWord word3 = new TweetWord (TweetWordType.NEGATIVE, TweetWordPhrase.VERB, "they're bringen drugs.", 30);
		TweetWord word4 = new TweetWord (TweetWordType.POSITIVE, TweetWordPhrase.VERB, "very talented.", 30);

		TweetWord word5 = new TweetWord (TweetWordType.NEUTRAL, TweetWordPhrase.FILL, "So foolish!", 30);

		tweetWords.Add (word1);
		tweetWords.Add (word2);
		tweetWords.Add (word3);
		tweetWords.Add (word4);
		tweetWords.Add (word5);
	}

	/// <summary>
	/// Gets a random word.
	/// </summary>
	/// <returns>The random word.</returns>
	public TweetWord GetRandomWord(){
		if (tweetWords == null) {
			LoadWords ();
		}
		return tweetWords[Random.Range(0, tweetWords.Count)];
	}
}
