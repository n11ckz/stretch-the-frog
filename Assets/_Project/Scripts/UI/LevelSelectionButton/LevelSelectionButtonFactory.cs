using System.Collections.Generic;
using Zenject;

namespace Project
{
    public class LevelSelectionButtonFactory : IInitializable
    {
        private readonly IInstantiator _instantiator;
        private readonly IAssetLoader _assetLoader;
        private readonly LevelSequence _levelSequence;

        private LevelSelectionButtonConfig _buttonConfig;

        public LevelSelectionButtonFactory(IInstantiator instantiator, IAssetLoader assetLoader, LevelSequence levelSequence)
        {
            _instantiator = instantiator;
            _assetLoader = assetLoader;
            _levelSequence = levelSequence;
        }

        public void Initialize() =>
            _buttonConfig = _assetLoader.Load<LevelSelectionButtonConfig>(AssetPaths.LevelSelectionButtonConfigPath);

        public IEnumerable<LevelSelectionButton> CreateButtonsLazy()
        {
            for (int i = 0; i < _levelSequence.LevelReferences.Count; i++)
            {
                LevelSelectionButton button = _instantiator.Instantiate(_buttonConfig.ButtonPrefab);
                button.Initialize(_levelSequence.LevelReferences[i], _buttonConfig, i + 1);

                yield return button;
            }
        }
    }
}
