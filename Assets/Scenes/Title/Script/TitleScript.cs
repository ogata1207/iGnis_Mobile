using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScript : MonoBehaviour {

    private FadeController fadeController;

	// Use this for initialization
	void Start () {
        fadeController = FindObjectOfType<FadeController>();
	}
	void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) StartCoroutine(GameStart("SampleScene"));
    }

    IEnumerator GameStart(string nextScene)
    {
        var wait = fadeController.FadeOut();
        yield return wait;

        LoadScene.RequestScene(nextScene);
    }
}
