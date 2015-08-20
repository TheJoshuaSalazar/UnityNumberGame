using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GamePiece : MonoBehaviour 
{
	public Text NumberText;
	public int Value;

	public delegate void ClickAction(GamePiece gamePiece);
	public event ClickAction OnClicked;

	// Use this for initialization
	void Start () 
	{
		TextUpdate ();
	}

	private void TextUpdate()
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
