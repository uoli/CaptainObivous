using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIPhone : MonoBehaviour 
{
	public Text display;
	public Phone phone;

	void OnEnable()
	{
		display.text = "";
	}

	public void OnHangUp()
	{
		phone.Close();
		
	}


	public void OnDialpadButtonPressed(string dialed)
	{
		display.text += dialed;

		if (display.text == "12345678")
			phone.CallCorrectNumber();
			
		
	}
}
