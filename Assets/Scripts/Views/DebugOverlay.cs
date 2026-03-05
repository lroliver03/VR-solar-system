using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DebugOverlay : MonoBehaviour
{
  public TextMeshProUGUI text;

  readonly Queue<string> lines = new();

  public int maxLines = 15;

  void OnEnable()
  {
    Application.logMessageReceived += HandleLog;
  }

  void OnDisable()
  {
    Application.logMessageReceived -= HandleLog;
  }

  void HandleLog(string logString, string stackTrace, LogType type)
  {
    lines.Enqueue(logString);

    while (lines.Count > maxLines)
      lines.Dequeue();

    text.text = string.Join("\n", lines);
  }
}
