using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class GameManager : SingletonMonobehaviour<GameManager>
{

    #region Header Dungeon Level

    [Space(10)]
    [Header("DUNGEON LEVELS")]
    #endregion Header Dungeon Level
    #region Tooltip
    [Tooltip("Dungeon Level Scriptable Objects")]
    #endregion Tooltip
    [SerializeField] private List<DungeonLevelSO> dungeonLevelList;

    [SerializeField] private int currentDungeonLevelListIndex = 0;
    [HideInInspector] public GameState gameState;
    private void Start()
    {
        gameState = GameState.gameStarted;
    }

    // Update is called once per frame
   private void Update()
    {
        HandleGameState();

        //for test
        if (Input.GetKeyDown(KeyCode.R))
        {
            gameState = GameState.gameStarted;
        }
    }
    private void HandleGameState()
    {
        switch (gameState)
        {
            case GameState.gameStarted:
                //play first level
                PlayDungeonLevel(currentDungeonLevelListIndex);
                gameState = GameState.playinglevel;
                break;        
        }
    }

    private void PlayDungeonLevel(int dungeonLevelListIndex)
    {

    }

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEnumerableValues(this, nameof(dungeonLevelList), dungeonLevelList);
    }
#endif
    #endregion Validation
}
