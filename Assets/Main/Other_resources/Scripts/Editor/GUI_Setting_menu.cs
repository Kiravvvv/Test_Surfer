//Добавляет кнопку в инспектор настроек
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Setting_menu))]
public class GUI_Setting_menu : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Setting_menu SM = (Setting_menu)target;

        if(GUILayout.Button("Очистить сохранения"))
        {
            SM.Clear_save();
        }
    }

}
#endif
