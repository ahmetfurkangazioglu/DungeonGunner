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
    private Room currentRoom;
    private Room previousRoom;
    private PlayerDetailsSO playerDetails;
    private Player player;
    [HideInInspector] public GameState gameState;

    protected override void Awake()
    {
        base.Awake();
        playerDetails = GameResources.Instance.currentPlayer.playerDetails;
        InstantiatePlayer();
    }

    private void InstantiatePlayer()
    {
        GameObject playerGameObject = Instantiate(playerDetails.playerPrefab);

        player = playerGameObject.GetComponent<Player>();
        player.Initialize(playerDetails);
    }

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


    public void SetCurrentRoom(Room room)
    {
        previousRoom = currentRoom;
        currentRoom = room;
    }

    private void PlayDungeonLevel(int dungeonLevelListIndex)
    {
        bool dungeonBuiltSucessfully = DungeonBuilder.Instance.GenerateDungeon(dungeonLevelList[dungeonLevelListIndex]);

        if (!dungeonBuiltSucessfully)
        {
            Debug.LogError("Couldn't build dungeon from specified rooms and node graphs");
        }
        player.gameObject.transform.position = new Vector3((currentRoom.lowerBounds.x + currentRoom.upperBounds.x) / 2f, (currentRoom.lowerBounds.y +
            currentRoom.upperBounds.y) / 2f, 0f);

        player.gameObject.transform.position = HelperUtilities.GetSpawnPositionNearestToPlayer(player.gameObject.transform.position);
    }

    public Player GetPlayer()
    {
        return player;
    }
    public Room GetCurrentRoom()
    {
        return currentRoom;
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
