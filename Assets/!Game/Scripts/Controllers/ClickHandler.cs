using TMPro;
using UnityEngine;

public interface IClickable
{
    string Name { get; }
    void OnClick();
}

public class ClickHandler : MonoBehaviour
{
    [SerializeField] private LayerMask _clickableLayers = ~0;
    [SerializeField] private GameObject _panel;
    [SerializeField] private TMP_Text _text;

    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        HandleClick();
    }

    private void HandleClick()
    {
        var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        _panel.SetActive(false);

        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, _clickableLayers))
        {
            var clickable = hit.collider.GetComponent<IClickable>();
            if(clickable == null) return;

            _panel.SetActive(true);
            _text.text = clickable.Name;
            if (Input.GetMouseButtonDown(0))
            {
                clickable.OnClick();
            }
        }
    }
}