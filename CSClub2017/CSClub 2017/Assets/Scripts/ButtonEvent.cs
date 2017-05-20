using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonEvent : MonoBehaviour {

	public void PointerEnter(BaseEventData eventData)
	{
		this.GetComponentInChildren<Text> ().fontSize = 25;
		this.GetComponentInChildren<Text> ().fontStyle = FontStyle.Bold;
	}

	public void PointerExit(BaseEventData eventData)
	{
		this.GetComponentInChildren<Text> ().fontSize = 18;
		this.GetComponentInChildren<Text> ().fontStyle = FontStyle.Normal;
	}
}
