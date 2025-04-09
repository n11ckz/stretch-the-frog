using Newtonsoft.Json;
using UnityEngine;

namespace Project
{
    public class SaveInPrefsStrategy : ISaveStrategy
    {
        private const string Key = nameof(Key);   
        
        public void Save(Progress progress)
        {
            string json = JsonConvert.SerializeObject(progress);
            PlayerPrefs.SetString(Key, json);
            Debug.Log(json);
        }

        public Progress Load()
        {
            string json = PlayerPrefs.GetString(Key, string.Empty);

            if (string.IsNullOrEmpty(json) == true)
                return new Progress() { UnlockedSceneBuildIndexes = new() };

            return JsonConvert.DeserializeObject<Progress>(json);
        }
    }
}
