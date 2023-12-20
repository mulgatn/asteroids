using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverSceneController : MonoBehaviour
{
	[SerializeField] TMP_Text _scoreText;

	void Start()
	{
		_scoreText.text = $"You've scored {PlayerPrefs.GetInt("Score")}";
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
			SceneManager.LoadScene(1);
	}
}
