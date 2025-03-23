using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project
{
    [SelectionBase]
    public class LevelProgressBar : MonoBehaviour, IPauseListener
    {
        [SerializeField] private Image _barMaskImage;
        [SerializeField] private TMP_Text _progressText;

        [SerializeField, Range(0.0f, 0.5f)] float _fillDuration;
        [SerializeField, Range(0.0f, 0.5f)] float _hideAndShowDuration;
        [SerializeField] private Vector2 _anchoredOffset;
        [SerializeField] private Ease _ease;

        private CellMap _cellMap;
        private PauseHandler _pauseHandler;
        private RectTransform _rectTransform;
        private Vector2 _initialAnchoredPosition;

        [Inject]
        private void Construct(CellMap cellMap, PauseHandler pauseHandler)
        {
            _cellMap = cellMap;
            _pauseHandler = pauseHandler;
        }

        private void Awake()
        {
            _cellMap.CellOccupied += FillProgressBar;
            _pauseHandler.AddListener(this);
            _rectTransform = transform as RectTransform;

            InitializeValues();
        }

        private void OnDestroy()
        {
            _cellMap.CellOccupied -= FillProgressBar;
            _pauseHandler.RemoveListener(this);
        }

        public void Pause() =>
            _rectTransform.DOAnchorPos(_anchoredOffset, _hideAndShowDuration).
            SetEase(_ease).
            SetUpdate(true);

        public void Resume() =>
            _rectTransform.DOAnchorPos(_initialAnchoredPosition, _hideAndShowDuration).
            SetEase(_ease).
            SetUpdate(true);

        private void FillProgressBar()
        {
            if (_cellMap.OccupiedCells.Count == 0)
                return;

            float endValue = (float)_cellMap.OccupiedCells.Count / _cellMap.TotalCellCount;
            DOTween.To(() => _barMaskImage.fillAmount, (progress) => UpdateProgressValues(progress), endValue, _fillDuration).
                SetEase(_ease);
        }

        private void InitializeValues()
        {
            UpdateProgressValues(0.0f);
            _initialAnchoredPosition = _rectTransform.anchoredPosition;
        }

        private void UpdateProgressValues(float progress)
        {
            _barMaskImage.fillAmount = progress;
            _progressText.text = $"{Mathf.RoundToInt(progress * 100)}%";
        }
    }
}
