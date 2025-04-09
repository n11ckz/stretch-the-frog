namespace Project
{
    public interface ISaveStrategy
    {
        public void Save(Progress progress);
        public Progress Load();
    }
}
