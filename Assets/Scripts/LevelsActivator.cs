using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelsActivator : MonoBehaviour
{
	public GameObject buttonPrefab;

	void Start ()
	{
		RectTransform containerRectTransform = gameObject.GetComponent<RectTransform> ();
		float contW = containerRectTransform.rect.width;
		float contH = containerRectTransform.rect.height;

		RectTransform rectTransform = buttonPrefab.GetComponent<RectTransform> ();
		float w = rectTransform.rect.width;
		float h = rectTransform.rect.height;

		float baseX = -contW / 2 + w / 2;
		float baseY = contH / 2 - h / 2;

		int buttonsCount = DataControl.Instance.GetLastLevelNumber ();
		int distance = 30;

		float offsetX = w / 2;
		float offsetY = h / 2;
		float x;
		float y;
		for (int i = 1; i <= buttonsCount; i++) {
			GameObject button = (GameObject)Instantiate (buttonPrefab);	
			button.transform.SetParent (gameObject.transform, false);
			button.GetComponentInChildren<Text> ().text = "" + i;
			button.GetComponentInChildren<LevelButtonBehavior> ().level = i;
			x = baseX + offsetX;
			y = baseY - offsetY;
			if ((x + w) > contW / 2) {
				offsetX = w / 2;
				offsetY += h + distance;
				x = baseX + offsetX;
				y = baseY - offsetY;
			}
			button.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (x, y);

			offsetX += w + distance;
		}
	}
}
