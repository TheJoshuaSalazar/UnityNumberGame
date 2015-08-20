using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	public GamePiece[] GamePieces;
	public TextManager TextManager;

	private int totalScore;
	private int lowEndNumber = 0;
	private int highEndNumber = 0;
	private int lowNumberOffset = 1;
	private int highNumberOffset = 10;

	private bool canClick = true;
	private bool clockWise = true;
	private bool greaterThan = true;

	// Use this for initialization
	void Start () 
	{
		StartGame ();
		AddClickEventsToPieces ();
	}

	private void StartGame()
	{
		foreach (var gamePiece in GamePieces) 
		{
			int value = Random.Range(1, 11);
			gamePiece.Value = value;
		}

		FindLowAndHigh ();
		AddTotalScore ();
	}

	private int AddTotalScore ()
	{
		totalScore = 0;
		foreach (var gamePiece in GamePieces)
		{
			totalScore += gamePiece.Value;
		}

		TextManager.SetTotalScore (totalScore);
		return totalScore;
	}

	private void FindLowAndHigh()
	{
		lowEndNumber = GamePieces [0].Value;
		foreach (var gamePiece in GamePieces)
		{
			if(gamePiece.Value < lowEndNumber)
				lowEndNumber = gamePiece.Value;

			if(highEndNumber < gamePiece.Value)
				highEndNumber = gamePiece.Value;
		}

		TextManager.SetNextNumberText (lowEndNumber, highEndNumber, highNumberOffset);
	}

	private void Clicked(GamePiece gamePiece)
	{
		if (canClick) 
		{
			gamePiece.Value = Random.Range (lowEndNumber + lowNumberOffset, highEndNumber + highNumberOffset);
			SetUpNextRound (gamePiece);


			if(canClick)
			{
				FindLowAndHigh ();
				AddTotalScore ();
				TextManager.SetNextNumberText (lowEndNumber, highEndNumber, highNumberOffset);
				TextManager.UpdateDirection (clockWise);
				TextManager.UpdateOperation (greaterThan);
			}
			else
			{
				RemoveClickEventsToPieces ();
				TextManager.GameOver();
			}
		}
	}

	#region Game Logic

	private void SetUpNextRound(GamePiece gamePiece)
	{
		int index = 0;
		int nextGamePieceIndex;

		for (int i = 0; i < GamePieces.Length; i++) 
		{
			if(GamePieces[i] == gamePiece)
			{
				index = i;
				break;
			}
		}

		if ((index + 1) == GamePieces.Length)
			nextGamePieceIndex = 0;
		else
			nextGamePieceIndex = index + 1;

		if (clockWise) 
		{
			if(greaterThan)
			{
				if(GamePieces[index].Value < GamePieces[nextGamePieceIndex].Value)
					canClick = false;
			}
			else
			{
				if(GamePieces[index].Value > GamePieces[nextGamePieceIndex].Value)
					canClick = false;
			}
		} 
		else 
		{
			if(greaterThan)
			{
				if(GamePieces[nextGamePieceIndex].Value < GamePieces[index].Value)
					canClick = false;
			}
			else
			{
				if(GamePieces[nextGamePieceIndex].Value > GamePieces[index].Value)
					canClick = false;
			}
		}
	}

	#endregion

	private void AddClickEventsToPieces()
	{
		foreach (var gamePiece in GamePieces) 
		{
			gamePiece.OnClicked += Clicked;
		}
	}
	
	private void RemoveClickEventsToPieces()
	{
		foreach (var gamePiece in GamePieces) 
		{
			gamePiece.OnClicked -= Clicked;
		}
	}
}
