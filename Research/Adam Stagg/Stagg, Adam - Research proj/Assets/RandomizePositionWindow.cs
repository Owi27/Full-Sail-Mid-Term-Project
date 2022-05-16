using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RandomizePositionWindow : EditorWindow
{


    GameObject m_Observer;

    [MenuItem("Window/Randomize Positions of Objects")]
    static void OpenWindow()
    {
        RandomizePositionWindow randomizePositionWindow = (RandomizePositionWindow)GetWindow(typeof(RandomizePositionWindow));

        randomizePositionWindow.minSize = new Vector2(600, 300);
        randomizePositionWindow.maxSize = new Vector2(1000, 500);

        randomizePositionWindow.Show();
    }

    private void OnEnable()
    {

    }

    public void OnGUI()
    {
        EditorGUILayout.Space();

        m_Observer = EditorGUILayout.ObjectField("IRandomizable", m_Observer, typeof(GameObject), false) as GameObject;
        

        if (GUILayout.Button("Randomize Objects"))
        {
            DoRandomize();
        }
    }


    public void DoRandomize()
    {
        if (m_Observer != null)
        {
            m_Observer.GetComponent<IRandomizable>().Randomize();
        } else
        {
            Debug.LogError("You must have an object in the Object Field");
        }
    }
}
