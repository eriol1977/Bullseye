using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelPages
{
	private List<LevelPage> pages = new List<LevelPage> ();

	private int currentPage = 0;

	private int pagesCount;

	public LevelPages (GameObject container, GameObject buttonPrefab, int cols, int rows)
	{
		Vector2 buttonSize;
		List<Vector2> coords = CalcCoords (container, cols, rows, out buttonSize);

		int buttonsForPage = cols * rows;
		int buttonsCount = 100;
		pagesCount = buttonsCount / buttonsForPage;
		if (buttonsCount % buttonsForPage > 0)
			pagesCount++;

		int firstNumber = 0;
		int buttonsLeft = buttonsCount;
		for (int i = 0; i < pagesCount; i++) {
			pages.Add (new LevelPage (container, buttonPrefab, coords, buttonSize, firstNumber, buttonsLeft > buttonsForPage ? buttonsForPage : buttonsLeft));
			firstNumber += buttonsForPage;
			buttonsLeft -= buttonsForPage;
		}

		pages [0].Show ();
	}

	public void Next ()
	{
		pages [currentPage].Hide ();
		if (currentPage < pagesCount - 1)
			currentPage++;
		pages [currentPage].Show ();
	}

	public void Previous ()
	{
		pages [currentPage].Hide ();
		if (currentPage > 0)
			currentPage--;
		pages [currentPage].Show ();
	}

	public bool isFirstPage ()
	{
		return currentPage == 0;
	}

	public bool isLastPage ()
	{
		return currentPage == pagesCount - 1;
	}

	private List<Vector2> CalcCoords (GameObject container, int cols, int rows, out Vector2 buttonSize)
	{
		RectTransform containerRectTransform = container.GetComponent<RectTransform> ();
		float contW = containerRectTransform.rect.width;
		float contH = containerRectTransform.rect.height;

		float distanceX = contW / 40;
		float distanceY = contH / 40;

		float wForButtons = contW - ((cols + 1) * distanceX);
		float hForButtons = contH - ((cols + 1) * distanceY);
		float buttonW = wForButtons / cols;
		float buttonH = hForButtons / rows;
		float offsetX = buttonW / 2;
		float offsetY = buttonH / 2;

		float x;
		float y;
		List<Vector2> coords = new List<Vector2> ();
		for (int j = 0; j < rows; j++) {
			for (int i = 0; i < cols; i++) {
				x = -contW / 2 + distanceX + offsetX + (distanceX + buttonW) * i;
				y = contH / 2 - (distanceY + offsetY) - (distanceY + buttonH) * j;
				coords.Add (new Vector2 (x, y));
			}
		}

		buttonSize = new Vector2 (buttonW, buttonH);
		return coords;
	}
}

class LevelPage
{
	List<GameObject> buttons = new List<GameObject> ();

	public LevelPage (GameObject container, GameObject buttonPrefab, List<Vector2> coords, Vector2 buttonSize, int firstNumber, int buttonCount)
	{
		GameObject button;
		Vector2 buttonCoords;
		int index = 0;
		GameObject scorePanel;
		int score;
		for (int n = firstNumber; n < firstNumber + buttonCount; n++) {
			buttonCoords = coords [index];
			button = GameObject.Instantiate (buttonPrefab);
			button.transform.SetParent (container.transform, false);
			button.GetComponentInChildren<Text> ().text = "" + (n + 1);
			button.GetComponentInChildren<LevelButtonBehavior> ().level = n + 1;
			button.GetComponent<RectTransform> ().sizeDelta = buttonSize;
			button.GetComponent<RectTransform> ().anchoredPosition = buttonCoords;
			scorePanel = button.transform.FindChild ("ScorePanel").gameObject;
			score = FlowControl.Instance.Player.GetScore (n + 1);
			scorePanel.GetComponentInChildren<Text> ().text = score > 0 ? score.ToString () : "---";
			button.SetActive (false);
			buttons.Add (button);
			index++;
		}
	}

	public void Show ()
	{
		int level = 1;
		int lastLevel = FlowControl.Instance.Player.LastLevel;
		foreach (GameObject button in buttons) {
			button.SetActive (true);
			if (level > lastLevel)
				button.GetComponent<Button> ().interactable = false;
			else
				button.GetComponent<Button> ().interactable = true;
			level++;
		}
	}

	public void Hide ()
	{
		foreach (GameObject button in buttons) {
			button.SetActive (false);
		}
	}
}
