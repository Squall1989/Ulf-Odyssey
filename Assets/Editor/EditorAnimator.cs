using System.Linq;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class SceneGUI : Editor
{
//    static Animator selectedAnimator = null;
//    static int selectedIndex = 0;
//    static float time = 0;
//    static bool isClosed = false;
//    static bool isFixed = false;

//    static SceneGUI()
//    {
//        SceneView.duringSceneGui += OnSceneGUI;
//    }

//    static void CLear()
//    {
//        selectedIndex = 0;
//        time = 0;
//        isFixed = false;
//    }

//    private static void OnSceneGUI(SceneView sceneView)
//    {
//        if (Selection.activeGameObject != null && !isFixed)
//        {
//            if (Selection.activeGameObject.GetComponent<Animator>() != null)
//            {
//                CLear();
//                selectedAnimator = Selection.activeGameObject.GetComponent<Animator>();
//            }
//        }

//        if(selectedAnimator == null)
//        {
//            CLear();
//            return;
//        }

//        Handles.BeginGUI();

//        if (isClosed)
//        {
//            GUILayout.BeginArea(new Rect(20, 20, 100, 100), "Закрыто", GUI.skin.window);
//            if(GUILayout.Button("Открыть"))
//            {
//                isClosed = false;
//            }
//            GUILayout.EndArea();
//            Handles.EndGUI();
//            return;
//        }

//        GUILayout.BeginArea(new Rect(10, 10, 250, 150), selectedAnimator.name, GUI.skin.window);

//        AnimationClip[] clips = null;
//        if (selectedAnimator.runtimeAnimatorController != null)
//        {
//            clips = selectedAnimator.runtimeAnimatorController.animationClips;
//        }

//        if (clips == null || clips.Length == 0)
//        {
//            GUILayout.Label("Нет доступных анимаций");
//            return;
//        }

//        string[] clipNames = clips.Select(x => x.name).ToArray();

//        selectedIndex = EditorGUILayout.Popup("Выбери анимацию:", selectedIndex, clipNames);
//        var clip = clips[selectedIndex];
//        var duration = clip.averageDuration;
//        time = EditorGUILayout.Slider(time, 0f, duration);
//        isFixed = EditorGUILayout.Toggle("Зафиксировать", isFixed);
//        PlayAnimationInEditor(clips[selectedIndex], selectedAnimator);

//        if (!isClosed && GUILayout.Button("Закрыть"))
//        {
//            isClosed = true;
//        }
//        GUILayout.EndArea();
//        Handles.EndGUI();
//    }

//    static void PlayAnimationInEditor(AnimationClip clip, Animator animator)
//    {
//        if (animator == null || clip == null)
//            return;

//        animator.Play(clip.name, 0, time);
//        animator.Update(0); // Принудительное обновление
//#if UNITY_EDITOR
//        EditorApplication.QueuePlayerLoopUpdate(); // Перерисовать сцену
//        SceneView.RepaintAll(); // Обновить отображение
//#endif
//    }
}
