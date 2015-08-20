using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextManager : MonoBehaviour 
{
	public Text TotalScoreText;
	public Text NextNumberText;
	public Text DirectionText;
	public Text OperationText;
	public Text GameOverText;
	
	public void SetTotalScore(int totalScore)
	{
		TotalScoreText.text = "Score: " + totalScore;
	}
	
	public void SetNextNumberText(int lowEndNumber, int highEndNumber, int highNumberOffset)
	{
		NextNumberText.text = "Between " + lowEndNumber + " and " + (highEndNumber + highNumberOffset);
	}

	public void GameOver()
	{
		GameOverText.text = "Game Over";
	}
	
	public void UpdateDirection(bool clockWise)
	{
		int trueOrFalse = Random.Range (0, 2);
		if (trueOrFalse == 1) 
		{
			DirectionText.text = "ClockWise";
			clockWise = true;
		} 
		else 
		{
			DirectionText.text = "Counter Clockwise";
			clockWise = false;
		}
	}
	
	public void UpdateOperation(bool greaterThan)
	{
		int trueOrFalse = Random.Range (0, 2);
		if (trueOrFalse == 1) 
		{
			OperationText.text = "Greater Than";
			greaterThan = true;
		} 
		else 
		{
			OperationText.text = "Less Than";
			greaterThan = false;
		}
	}
}
