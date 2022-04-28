using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnMouseOver : MonoBehaviour
{
	public string label;

	private bool labelOn = false;

	private void OnMouseEnter()
	{
		labelOn = true;
	}

	private void OnMouseExit()
	{
		labelOn = false;
	}	

	void OnGUI()
	{
		if (labelOn)
		{
			GUIStyle guiStyle = new GUIStyle();
			guiStyle.fontSize = 35;
			guiStyle.normal.textColor = Color.black;

			Vector3 mousePosition = Input.mousePosition;
			float x = mousePosition.x;
			float y = Screen.height - mousePosition.y;
			float width = 400;
			float height = 100;
			Rect rect = new Rect(x + 15, y - 15, width, height);
			GUI.Label(rect, label, guiStyle);
		}
	}
}
