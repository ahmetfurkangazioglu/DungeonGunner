using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDetails_", menuName = "Scriptable Objects/Player/Player Details")]
public class PlayerDetailsSO : ScriptableObject
{
    #region Header Player Base Details
    [Space(10)]
    [Header("Player Base Details")]
    #endregion
    #region Tooltip
    [Tooltip("Player character name")]
    #endregion
    public string playerCharacterName;

    #region Tooltip
    [Tooltip("Prefab gameobject for the player")]
    #endregion
    public GameObject playerPrefab;

    #region Tooltip
    [Tooltip("Player runtime animator controller")]
    #endregion
    public RuntimeAnimatorController runtimeAnimatorController;

    #region Header Health
    [Space(10)]
    [Header("Healtg")]
    #endregion
    #region Tooltip
    [Tooltip("Player starting health amount")]
    #endregion
    public int playerHealthAmount;

    #region Header Other
    [Space(10)]
    [Header("Header")]
    #endregion
    #region Tooltip
    [Tooltip("Player icon sprite to be used in the minimap")]
    #endregion
    public Sprite playerMiniMapIcon;

    #region Tooltip
    [Tooltip("Player hand sprite")]
    #endregion
    public Sprite playerHandSprite;

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEmptyString(this, nameof(playerCharacterName), playerCharacterName);
        HelperUtilities.ValidateCheckNullValue(this, nameof(playerPrefab), playerPrefab);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(playerHealthAmount), playerHealthAmount, false);
        HelperUtilities.ValidateCheckNullValue(this, nameof(playerMiniMapIcon), playerMiniMapIcon);
        HelperUtilities.ValidateCheckNullValue(this, nameof(playerHandSprite), playerHandSprite);
        HelperUtilities.ValidateCheckNullValue(this, nameof(runtimeAnimatorController), runtimeAnimatorController);
    }
#endif
    #endregion
}
