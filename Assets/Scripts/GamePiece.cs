using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GamePiece : MonoBehaviour 
{
	public Text NumberText;
	public int Value;
	public GamePiece ClockwisePiece;
	public GamePiece CounterClockwisePiece;

	public delegate void ClickAction(GamePiece gamePiece);
	public event ClickAction OnClicked;

	// Use this for initialization
	void Start () 
	{
		TextUpdate ();
	}

	public void TextUpdate()
	{
		NumberText.text = Value.ToString();
	}

	void OnMouseUp() 
	{
		if(OnClicked != null)
			OnClicked(this);

		TextUpdate ();
	}

	void OnMouseDown() 
	{
		
	}
}
