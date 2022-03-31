using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Dan
{
    public enum LogType
    {
        Default,
        Error,
        Warning,
        Success
    }
    
    public class TextLogger : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _logTextPrefab;

        private static TextMeshProUGUI _log;
        private static Transform _canvas;
        
        private void Awake()
        {
            _log = _logTextPrefab;
            _canvas = FindObjectOfType<Canvas>().transform;
        }

        public static void Log(string message, LogType logType, float duration = 2f)
        {
            var tempLog = Instantiate(_log, _canvas);
            tempLog.text = message.ToUpper();

            tempLog.color = logType switch
            {
                LogType.Default => Color.white,
                LogType.Error   => Color.red,
                LogType.Warning => Color.yellow,
                LogType.Success => Color.green,
                _ => throw new ArgumentOutOfRangeException(nameof(logType), logType, null)
            };

            tempLog.DOFade(1f, 0.5f).OnComplete(delegate
            {
                tempLog.DOFade(1f, duration).OnComplete(() =>
                    tempLog.DOFade(0f, 0.5f).OnComplete(() => Destroy(tempLog.gameObject)));
                tempLog.transform.DOLocalMoveY(tempLog.transform.localPosition.y + 100f, duration + 0.25f);
            });
        }
    }
}