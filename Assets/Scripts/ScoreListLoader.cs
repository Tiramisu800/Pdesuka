using Pdesuka.Data;
using Pdesuka.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreListLoader : MonoBehaviour
{
    [SerializeField] private RectTransform _scoreList;
    [SerializeField] private ScoreItem _scoreItem;
    [SerializeField] private TMP_Text _userName;
    private List<ScoreItem> _scoreItems = new List<ScoreItem>();

    private void OnEnable()
    {
        DataController.Instance.LoadedAllData += ResetScoreList;
    }

    public void ShowScoreList()
    {
        SoundManager.Instance.PlaySound("UI");
        var username = MenuManager.Instance._userName;
        _userName.text = username;
        DataController.Instance.LoadData(username);
    }
    private void ResetScoreList(List<DataController.DataToSave> list)
    {
        foreach (var item in _scoreItems)
        {
            Destroy(item.gameObject);
        }

        _scoreItems.Clear();

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].TimeScore > 0)
            {
                var scoreItem = Instantiate(_scoreItem, _scoreList);
                scoreItem.SetData(list[i]);
                _scoreItems.Add(scoreItem);
            }
        }
    }
}
