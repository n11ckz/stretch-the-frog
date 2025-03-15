using UnityEngine;

namespace Project
{
    public class Level
    {
        private readonly ICompleteCondition _completeCondition;
        private readonly BetweenScenesMediator _mediator;
        private readonly SavedProgressStorage _progressStorage;

        public Level(ICompleteCondition winCondition, BetweenScenesMediator mediator, SavedProgressStorage progressStorage)
        {
            _completeCondition = winCondition;
            _mediator = mediator;
            _progressStorage = progressStorage;
        }

        public void Start()
        {
            _completeCondition.Completed += Complete;
        }

        private void Complete(bool isSuccessfully)
        {
            _completeCondition.Completed -= Complete;
            _mediator.NotifyAboutLevelCompletion(isSuccessfully);
            Debug.Log($"Level has won: <{isSuccessfully}>");

            if (isSuccessfully == true)
                _progressStorage.Save();
        }
    }
}
