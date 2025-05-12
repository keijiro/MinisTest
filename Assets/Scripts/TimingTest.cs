using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public sealed class TimingTest : MonoBehaviour
{
    [SerializeField] InputAction _action = null;

    double _prevTime;

    Label UILabel
      => GetComponent<UIDocument>().rootVisualElement.Q<Label>("text");

    void Start()
    {
        _action.performed += OnPerformed;
        _action.Enable();
    }

    void OnPerformed(InputAction.CallbackContext ctx)
    {
        var delta = ctx.time - _prevTime;
        var bpm = 60 / delta / 4;
        UILabel.text = $"Calculated BPM: {bpm:.00}";
        _prevTime = ctx.time;
    }
}
