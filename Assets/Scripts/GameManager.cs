using UnityEngine;
using System.Collections;
using InputHandler;
using MenusHandler;

public class GameManager : MonoBehaviour
{
    public int PlayerCount = 4;
    private static GameManager _instance;

    public static GameManager Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        _instance = this;

        /*
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }*/
    }

    void Start()
    {
        for (int i = 0; i < PlayerCount; i++)
        {
            InputManager.Instance.AddCallback(i, HandleMenuInput);
        }


        // play gameplay music
        //MusicManager.Instance.PlayGameplayMusic();
    }

    public void PushMenuContext()
    {
        for (int i = 0; i < PlayerCount; i++)
        {
            InputManager.Instance.PushActiveContext("Menu", i);
        }
    }

    public void PopMenuContext()
    {
        for (int i = 0; i < PlayerCount; i++)
        {
            InputManager.Instance.PopActiveContext(i);
        }
    }

    private void HandleMenuInput(MappedInput input)
    {
        float yAxis = 0f;

        if (input.Ranges.ContainsKey("SelectOptionUp"))
        {
            yAxis = input.Ranges["SelectOptionUp"];
        }
        else if (input.Ranges.ContainsKey("SelectOptionDown"))
        {
            yAxis = -input.Ranges["SelectOptionDown"];
        }

        bool accept = input.Actions.Contains("Accept");

        MenusManager.Instance.SetInputValues(accept, false, 0f, yAxis);
    }
}
