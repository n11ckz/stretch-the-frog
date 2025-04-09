namespace Project
{
    public interface IProgressListener
    {
        public void Load(Progress progress);
        public void Save(Progress progress);
    }
}
