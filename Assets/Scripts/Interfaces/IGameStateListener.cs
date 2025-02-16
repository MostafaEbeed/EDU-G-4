using UnityEngine;

public interface IGameStateListener
{
    void GameStateChangedCallback(GameState gameState);
}