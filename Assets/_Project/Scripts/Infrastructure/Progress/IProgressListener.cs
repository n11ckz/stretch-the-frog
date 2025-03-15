namespace Project
{
    public interface IProgressListener
    {
        public void Load(SavedProgress progress);
        public void Save(SavedProgress progress);
    }
}
