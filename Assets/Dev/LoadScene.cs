using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum SceneList
{
    Title,
    SampleScene
}

public class LoadScene : MonoBehaviour {

    public Slider slider;
    static private string nextScene;

	// Use this for initialization
	IEnumerator Start () {

        var async = SceneManager.LoadSceneAsync(nextScene);

        async.allowSceneActivation = false;

        //ロードが完了するまで待機
        while (async.progress < 0.9f)
        {
            //なんか0.9fまでしかいきません
            slider.value = async.progress + 0.1f;
            yield return null;
        }

        async.allowSceneActivation = true;

    }
    static public void RequestScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadScene");
    }

}
