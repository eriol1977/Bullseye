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

}

