using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	public GamePiece[] GamePieces;
	public TextManager TextManager;
	public Button RestartButton;

	private int totalScore;
	private bool canClick = true;
	private bool clockWise = true;
	private bool greaterThan = true;

	// Use this for initialization
	void Start () 
	{
		StartGame ();
		AddClickEventsToPieces ();
		RestartButton.gameObject.SetActive (false);
	}

	/// <summary>
	/// Adds the starting values to the cubes
	/// and sets up everything
	/// </summary>
	private void StartGame()
	{
		foreach (var gamePiece in GamePieces) 
		{
			int value = Random.Range(1, 11);
			gamePiece.Value = value;
			gamePiece.TextUpdate ();
		}

		AddTotalScore ();
		TextManager.UpdateDirection (ref clockWise);
	}

	/// <summary>
	/// Adds the total score
	/// </summary>
	/// <returns>The total score</returns>
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

	private void Clicked(GamePiece gamePiece)
	{
		if (canClick) 
		{
			var additionPiece = (clockWise ? gamePiece.CounterClockwisePiece : gamePiece.ClockwisePiece);
			gamePiece.Value += additionPiece.Value;
			SetUpNextRound (gamePiece);

			//Debug.Log (clockWise + " " + additionPiece.Value);

			if(canClick)
			{
				AddTotalScore ();
				TextManager.UpdateDirection (ref clockWise);
				TextManager.UpdateOperation (greaterThan);
			}
			else
			{
				RemoveClickEventsToPieces ();
				TextManager.GameOver();

				RestartButton.gameObject.SetActive (true);
				RestartButton.onClick.AddListener(delegate { RestartGameEvent(); });
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

	/// <summary>
	/// Restarts the game scene
	/// </summary>
	private void RestartGameEvent()
	{
		StartGame ();
		TextManager.Restart ();
		AddClickEventsToPieces ();
		RestartButton.gameObject.SetActive (false);

		canClick = true;
	}
}
