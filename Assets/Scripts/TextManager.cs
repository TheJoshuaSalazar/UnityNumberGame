using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextManager : MonoBehaviour 
{
	public Text TotalScoreText;
	public Text DirectionText;
	public Text OperationText;
	public Text GameOverText;
	
	public void SetTotalScore(int totalScore)
	{
		TotalScoreText.text = "Score: " + totalScore;
	}

	public void GameOver()
	{
		GameOverText.text = "Game Over";
	}

	public void Restart()
	{
		GameOverText.text = "";
	}
	
	public void UpdateDirection(ref bool clockWise)
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
