using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Pdesuka.Data;

public class ScoreItem : MonoBehaviour
{
    [SerializeField] private TMP_Text _userLevel;
    [SerializeField] private TMP_Text _userTimeScore;

    public void SetData(DataController.DataToSave user)
    {
        _userLevel.text = user.CurrentLevelIndex.ToString();
        _userTimeScore.text = user.TimeScore.ToString();

        Debug.Log(user.CurrentLevelIndex);
    }
}
