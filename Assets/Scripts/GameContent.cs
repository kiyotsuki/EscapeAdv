using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameContent : MonoBehaviour
{
	public void SetActive(bool flag)
	{
		gameObject.SetActive(flag);
	}

	public void AddButtonListener(UnityAction action, int index = 0)
	{
		_buttons[index].onClick.AddListener(action);
	}

	public void SetLabelText(string text, int index = 0)
	{
		_labels[index].text = text;
	}

	public void SetImageSprite(Sprite sprite, int index = 0)
	{
		_images[index].sprite = sprite;
	}
	
	public GameContent GetInnerContent(int index = 0)
	{
		return _contents[index];
	}


	[SerializeField]
	protected Text[] _labels;

	[SerializeField]
	protected Button[] _buttons;

	[SerializeField]
	protected Image[] _images;

	[SerializeField]
	protected GameContent[] _contents;
}
