namespace ClientApplication.Core.Scene
{
    public static class SceneManager
    {
        private static Scene? m_currentScene;
        private static Scene? m_nextScene;
        public static Scene? Scene
        {
            get
            {
                TrySwitchScene();
                return SceneManager.m_currentScene;
            }
        }

        public static event Action<Scene>? SceneChanged;

        public static void SetScene(Scene scene)
        {
            SceneManager.m_nextScene = scene;
        }

        public static void TrySwitchScene()
        {
            if (SceneManager.m_nextScene != null)
            {
                SceneManager.m_currentScene?.Dispose();
                SceneManager.m_currentScene = m_nextScene;
                SceneManager.SceneChanged?.Invoke(SceneManager.m_currentScene);
                SceneManager.m_nextScene = null;
            }
        }
    }
}
