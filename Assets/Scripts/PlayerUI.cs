using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
	[SerializeField] TMP_Text _scoreText;
	[SerializeField] TMP_Text _livesText;

	public void SetScoreText(int score) => _scoreText.text = $"Score: {score}";
	public void SetLivesText(int lives) => _livesText.text = $"Lives: {lives}";
}
