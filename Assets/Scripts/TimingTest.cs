using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using System.Collections.Generic;

public sealed class TimingTest : MonoBehaviour
{
    [SerializeField] InputAction _action = null;

    Queue<string> _logLines = new Queue<string>();

    string LogText => string.Join("\n", _logLines);

    void AddLog(string line)
    {
        _logLines.Enqueue(line);
        while (_logLines.Count > 40) _logLines.Dequeue();
    }

    double _prevTime;

    Label UILabel
      => GetComponent<UIDocument>().rootVisualElement.Q<Label>("text");

    void Start()
    {
        _action.performed += OnPerformed;
        _action.Enable();
        AddLog("Waiting for input...");
    }

    void OnPerformed(InputAction.CallbackContext ctx)
    {
        var delta = ctx.time - _prevTime;
        var bpm = 60 / delta / 4;
        AddLog($"Interval: {delta * 1000:.00} ms / Calculated BPM: {bpm:.00}");
        _prevTime = ctx.time;
    }

    void Update()
      => UILabel.text = LogText;
}
