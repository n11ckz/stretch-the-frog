using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Project
{
    public class ToolMenuItems
    {
        [MenuItem("Tools/Play From Bootstrap Scene")]
        private static void PlayFromBootstrapScene()
        {
            if (CanPlay() == false)
                return;

            string scenePath = SceneUtility.GetScenePathByBuildIndex(0);
            EditorSceneManager.OpenScene(scenePath);
            EditorApplication.isPlaying = true;
        }

        private static bool CanPlay() =>
            EditorApplication.isPlaying == false || EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo() == true;
    }
}
