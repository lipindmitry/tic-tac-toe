using UnityEngine;

public class UiTab : MonoBehaviour
{
    [SerializeField] private UiTabType _tabType;

    public UiTabType TabType => _tabType;
}
