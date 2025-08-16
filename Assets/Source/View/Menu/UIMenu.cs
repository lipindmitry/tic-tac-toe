using System;
using System.Linq;
using UnityEngine;

public class UIMenu : MonoBehaviour
{
    [SerializeField] private UiTabType _startTabType;
    [SerializeField] private Transform _tabsParrent;
    
    private UiTab[] _tabs;

    private void Awake()
    {
        _tabs = _tabsParrent.GetComponentsInChildren<UiTab>(true);
        Switch(_startTabType);
    }

    public void Switch(UiTabType tabType)
    {
        if (tabType == UiTabType.None)
            throw new Exception("Incorrect tab type");

        foreach (var tab in _tabs)
            tab.gameObject.SetActive(tab.TabType == tabType);
    }

    public void Show(UiTabType tabType)
    {
        _tabs.First(x => x.TabType == tabType)
             .gameObject.SetActive(true);
    }
}

