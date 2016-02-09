using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelsActivator : MonoBehaviour
{
	public GameObject buttonPrefab;

	public GameObject previousButton;

	public GameObject nextButton;

	public int cols = 6;

	public int rows = 5;

	private LevelPages pagesContainer;

	void Start ()
	{
		pagesContainer = new LevelPages (gameObject, buttonPrefab, cols, rows);
		previousButton.SetActive (false);
//		Vector2 buttonSize;
//		List<Vector2> coords = CalcCoords (out buttonSize);
//
//		int n = 1;
//		GameObject button;
//		foreach (Vector2 buttonCoords in coords) {
//			button = (GameObject)Instantiate (buttonPrefab);
//			button.transform.SetParent (gameObject.transform, false);
//			button.GetComponentInChildren<Text> ().text = "" + n;
//			button.GetComponentInChildren<LevelButtonBehavior> ().level = n;
//			button.GetComponent<RectTransform> ().sizeDelta = buttonSize;
//			button.GetComponent<RectTransform> ().anchoredPosition = buttonCoords;
//			n++;
//		}
	}

	public void NextPage ()
	{
		pagesContainer.Next ();
		previousButton.SetActive (true);
		if (pagesContainer.isLastPage ())
			nextButton.SetActive (false);
	}

	public void PreviousPage ()
	{
		pagesContainer.Previous ();
		nextButton.SetActive (true);
		if (pagesContainer.isFirstPage ())
			previousButton.SetActive (false);
	}

	//	private List<Vector2> CalcCoords (out Vector2 buttonSize)
	//	{
	//		RectTransform containerRectTransform = gameObject.GetComponent<RectTransform> ();
	//		float contW = containerRectTransform.rect.width;
	//		float contH = containerRectTransform.rect.height;
	//
	//		float distanceX = contW / 40;
	//		float distanceY = contH / 40;
	//
	//		float wForButtons = contW - ((cols + 1) * distanceX);
	//		float hForButtons = contH - ((cols + 1) * distanceY);
	//		float buttonW = wForButtons / cols;
	//		float buttonH = hForButtons / rows;
	//		float offsetX = buttonW / 2;
	//		float offsetY = buttonH / 2;
	//
	//		float x;
	//		float y;
	//		List<Vector2> coords = new List<Vector2> ();
	//		for (int j = 0; j < rows; j++) {
	//			for (int i = 0; i < cols; i++) {
	//				x = -contW / 2 + distanceX + offsetX + (distanceX + buttonW) * i;
	//				y = contH / 2 - (distanceY + offsetY) - (distanceY + buttonH) * j;
	//				coords.Add (new Vector2 (x, y));
	//			}
	//		}
	//
	//		buttonSize = new Vector2 (buttonW, buttonH);
	//		return coords;
	//	}
}

