namespace ClientApplication.Core.Scene
{
    public static class SceneManager
    {
        private static Scene? m_currentScene;
        public static Scene? Scene => SceneManager.m_currentScene;

        public static event Action<Scene>? SceneChanged;

        public static void SetScene(Scene scene)
        {
            SceneManager.m_currentScene = scene;
            SceneChanged?.Invoke(scene);
        }
    }
}
