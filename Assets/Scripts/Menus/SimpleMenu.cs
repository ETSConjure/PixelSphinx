using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SimpleMenu : MonoBehaviour
{
	public int NextLevel;

	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown) {
			if (NextLevel == 200)
				Application.Quit();
			else
				SceneManager.LoadScene(NextLevel);
        }
	}

    IEnumerator AutoSkip() {

        yield return new WaitForSeconds(5);
        

    }
}
