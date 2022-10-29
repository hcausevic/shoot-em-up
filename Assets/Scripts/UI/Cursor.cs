using UnityEngine;
using UnityEngine.UI;

public class Cursor : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    private RectTransform _cursorRect;
    private int _currentOption;

    private void Awake()
    {
        _cursorRect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangeOption(-1);
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeOption(1);
        }
        
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {
            ClickOption();
        }
    }

    private void ChangeOption(int change)
    {
        _currentOption += change;
        
        if (_currentOption < 0)
        {
            _currentOption = options.Length - 1;
        }
        else if (_currentOption >= options.Length)
        {
            _currentOption = 0;
        }

        _cursorRect.position = new Vector3(_cursorRect.position.x, options[_currentOption].position.y, 0);
    }

    private void ClickOption()
    {
        options[_currentOption].GetComponent<Button>().onClick.Invoke();
    }
}
