using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextVersion : MonoBehaviour
{
	private const string VersionPrefix = "Version: ";

	private TextMeshProUGUI _textUI;

	private void Awake()
	{
		_textUI = GetComponent<TextMeshProUGUI>();
	}

	private void Start()
	{
		_textUI.text = VersionPrefix + Application.version;
	}
}
