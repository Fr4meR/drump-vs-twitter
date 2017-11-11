using System.Collections;
using System.Collections.Generic;

public class TweetWord{

	public TweetWordType Type { get; private set; }

	public int Value { get; private set; }

	public TweetWordPhrase Phrase { get; private set; }

	public string Word { get; private set; }

	public TweetWord(TweetWordType type, TweetWordPhrase phrase, string word, int value){
		Type = type;
		Phrase = phrase;
		Word = word;
		Value = value;
	}
}
