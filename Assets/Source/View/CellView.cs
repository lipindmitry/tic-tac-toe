using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class CellView : MonoBehaviour
{
    public event Action<IBoardCell> MarkRequested;

    [SerializeField] private Image _zero;
    [SerializeField] private Image _cross;
    
    private IBoardCell _cell;
    private IDisposable _subscription;

    public void Initialize(IBoardCell cell)
    {
        _cell = cell;
        SetContent(_cell.ContentType.Value);
        _subscription = _cell.ContentType.Subscribe(SetContent);
    }

    private void OnDestroy()
    {
        _subscription?.Dispose();
    }

    private void SetContent(CellContentType cellContentType)
    {
        switch (cellContentType)
        {
            case CellContentType.Empty:
                {
                    _zero.gameObject.SetActive(false);
                    _cross.gameObject.SetActive(false);
                    break;
                }
            case CellContentType.Cross:
                {
                    _zero.gameObject.SetActive(false);
                    _cross.gameObject.SetActive(true);
                    break;
                }
            case CellContentType.Zero:
                {
                    _zero.gameObject.SetActive(true);
                    _cross.gameObject.SetActive(false);
                    break;
                }
            default:
                throw new ArgumentException($"Неверное значение для контента ячейки: {cellContentType}.");
        }
    }

    public void RequestMark()
    {
        MarkRequested?.Invoke(_cell);
    }
}

