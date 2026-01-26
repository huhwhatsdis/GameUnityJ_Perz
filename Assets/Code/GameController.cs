using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { FreeRoam, Sign }

public class GameController : MonoBehaviour
{
    [SerializeField] private Movement movement;

    private GameState state;

    private void Start()
{
    if (TextManager.Instance != null)
    {
        TextManager.Instance.OnShowText += () => state = GameState.Sign;
        TextManager.Instance.OnHideText += () => state = GameState.FreeRoam;
    }
}


    private void Update()
    {
        if (state == GameState.FreeRoam)
        {
            movement.HandleUpdate();
        }
        else if (state == GameState.Sign)
        {
            TextManager.Instance.HandleUpdate();
        }
    }
}

