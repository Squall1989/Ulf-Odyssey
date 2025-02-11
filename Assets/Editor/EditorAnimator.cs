using System.Linq;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class SceneGUI : Editor
{
    static Animator selectedAnimator = null;
    static int selectedIndex = 0;
    static float time = 0;
    static bool isFixed = false;

    static SceneGUI()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private static void OnSceneGUI(SceneView sceneView)
    {
        if (Selection.activeGameObject != null && !isFixed)
        {
            if (Selection.activeGameObject.GetComponent<Animator>() != null)
            {
                selectedAnimator = Selection.activeGameObject.GetComponent<Animator>();
            }
        }

        if(selectedAnimator == null)
        {
            return;
        }

        Handles.BeginGUI(); // Начинаем рисовать GUI
        GUILayout.BeginArea(new Rect(10, 10, 250, 150), "Анимация", GUI.skin.window);

        AnimationClip[] clips = null;
        if (selectedAnimator.runtimeAnimatorController != null)
        {
            clips = selectedAnimator.runtimeAnimatorController.animationClips;
        }

        if (clips == null || clips.Length == 0)
        {
            GUILayout.Label("Нет доступных анимаций");
            return;
        }

        string[] clipNames = clips.Select(x => x.name).ToArray();

        selectedIndex = EditorGUILayout.Popup("Выбери анимацию:", selectedIndex, clipNames);
        time = EditorGUILayout.Slider(time, 0f, 1f);
        isFixed = EditorGUILayout.Toggle("Зафиксировать", isFixed);
        PlayAnimationInEditor(clips[selectedIndex], selectedAnimator);
        GUILayout.EndArea();
        Handles.EndGUI(); // Завершаем рисование GUI
    }

    static void PlayAnimationInEditor(AnimationClip clip, Animator animator)
    {
        if (animator == null || clip == null)
            return;

        float timing = clip.averageDuration * time;

        animator.Play(clip.name, 0, timing);
        animator.Update(0); // Принудительное обновление
#if UNITY_EDITOR
        EditorApplication.QueuePlayerLoopUpdate(); // Перерисовать сцену
        SceneView.RepaintAll(); // Обновить отображение
#endif
    }
}

[CustomEditor(typeof(Animator))]
public class AnimatorPreview : Editor
{
    Animator animator;


    private void OnSceneGUI()
    {
        Animator animator = (Animator)target;


        

        Handles.BeginGUI();

    }

    
}