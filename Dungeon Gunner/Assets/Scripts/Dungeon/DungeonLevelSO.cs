using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonLevel_", menuName = "Scriptable Objects/Dungeon/Dungeon Level")]
public class DungeonLevelSO : ScriptableObject
{
    #region Header Basic Level Details
    [Space(10)]
    [Header("BASIV LEVEL DETAILS")]
    #endregion Header Basic Level Details
    #region Tooltip
    [Tooltip("The name for the level")]
    #endregion Tooltip
    public string levelName;

    #region Header Room Templates For Level
    [Space(10)]
    [Header("Room Templates For Level")]
    #endregion Header Room Templates For Level
    #region Tooltip
    [Tooltip("The roomTemplateList for the level")]
    #endregion Tooltip
    public List<RoomTemplateSO> roomTemplateList;

    #region Header Room Node Graphs For Level
    [Space(10)]
    [Header("Room Node Graphs For Level")]
    #endregion Header Room Node Graphs For Level
    #region Tooltip
    [Tooltip("Populate this list with the room node graphs which should be randomly selected from for the level.")]
    #endregion Tooltip
    public List<RoomNodeGraphSO> roomNodeGraphList;

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEmptyString(this, nameof(levelName), levelName);
        if (HelperUtilities.ValidateCheckEnumerableValues(this, nameof(roomTemplateList), roomTemplateList))
            return;
        if (HelperUtilities.ValidateCheckEnumerableValues(this, nameof(roomNodeGraphList), roomNodeGraphList))
            return;

        //first check that north/south corridor, east/west corridor and entrance types have been specified
        bool isEWCorridor = false;
        bool isNSCorridor = false;
        bool isEntrance = false;

        // Loop through all room templates to check that this node type has been specified
        foreach (RoomTemplateSO roomTemplateSO in roomTemplateList)
        {
            if (roomTemplateSO==null)          
                return;
            if (roomTemplateSO.roomNodeType.isCorridorEW)
                isEWCorridor = true;
            if (roomTemplateSO.roomNodeType.isCorridorNS)
                isNSCorridor = true;
            if (roomTemplateSO.roomNodeType.isEntrance)
                isEntrance = true;
        }
        if (isEWCorridor==false)
        {
            Debug.Log("In " + this.name.ToString() + " : No E/W Corridor Room Type Specified");
        }
        if (isNSCorridor == false)
        {
            Debug.Log("In " + this.name.ToString() + " : No N/S Corridor Room Type Specified");
        }
        if (isEntrance == false)
        {
            Debug.Log("In " + this.name.ToString() + " : No Entrance Corridor Room Type Specified");
        }

        //Loop All nodes in graph
        foreach (RoomNodeGraphSO roomNodeGraph in roomNodeGraphList)
        {
            if (roomNodeGraph == null)
                return;
            foreach (RoomNodeSO roomNodeSO in roomNodeGraph.roomNodeList)
            {
                if (roomNodeSO == null)
                    continue;

                //Corrider and entrance already checked
                if (roomNodeSO.roomNodeType.isEntrance || roomNodeSO.roomNodeType.isCorridorEW || roomNodeSO.roomNodeType.isCorridorNS ||
                    roomNodeSO.roomNodeType.isCorridor || roomNodeSO.roomNodeType.isNone)
                     continue;

                bool isRoomNodeTypeFound = false;

                foreach (RoomTemplateSO roomTemplateSO in roomTemplateList)
                {
                    if (roomTemplateSO == null)
                        continue;
                    if (roomTemplateSO.roomNodeType==roomNodeSO.roomNodeType)
                    {
                        isRoomNodeTypeFound = true;
                        break;
                    }
                }

                if (!isRoomNodeTypeFound)
                {
                    Debug.Log("In " + this.name.ToString() + " : No Room Template " + roomNodeSO.roomNodeType.name.ToString() + "Found For node graph " + 
                        roomNodeGraph.name.ToString());
                }
            }
        }
    }
#endif
    #endregion Validation
}
