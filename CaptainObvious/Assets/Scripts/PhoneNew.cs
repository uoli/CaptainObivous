﻿using UnityEngine;
using System.Collections;

public class PhoneNew : MonoBehaviour
{
	public AudioClip soundBite;
	public AudioClip soundBite2;

	private int _dialedNumberLength;
	private char[] _dialedNumber = new char[8];

	public void DialDigit(char digit)
	{
		if (digit < '0' || digit > '9')
			return;

		_dialedNumber[_dialedNumberLength] = digit;
		++_dialedNumberLength;

		if (_dialedNumberLength == 8)
		{
			var number = new string(_dialedNumber);
			if (number == "22808790")
			{
				Camera.main.audio.clip = soundBite;
				Camera.main.audio.Play();
				StartCoroutine(WhenFinishedPlaySoundbite2());
			}
			else
			{
				////TODO: give some indication of failure	
			}

			ResetDialedNumber();
		}
	}

	public void ResetDialedNumber()
	{
		_dialedNumberLength = 0;
		for (var i = 0; i < _dialedNumber.Length; ++i)
			_dialedNumber[i] = ' ';
	}

	IEnumerator WhenFinishedPlaySoundbite2()
	{
		var audioSource = Camera.main.audio;
		while(audioSource.isPlaying)
		{
			yield return new WaitForEndOfFrame();
		}
		Camera.main.audio.clip = soundBite2;
		Camera.main.audio.Play();
	}
}
