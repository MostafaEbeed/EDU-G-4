using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemActionPanel : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;

    public void AddButton(string name, Action onClickAction)
    {
        GameObject button = Instantiate(buttonPrefab, transform);
        button.GetComponent<Button>().onClick.AddListener(() =>
        {
            onClickAction();
            Toggle(false);
        });
        button.GetComponentInChildren<TMP_Text>().text = name;
    }

    public void Toggle(bool val)
    {
        if (val == false)
            RemoveOldButtons();
        gameObject.SetActive(val);

    }

    public void RemoveOldButtons()
    {
        foreach (Transform transformChildObjects in transform)
        {
            Destroy(transformChildObjects.gameObject);
        }
    }
}