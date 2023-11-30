using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;

namespace Infornographyx.MaterialEditor {
    public class MaterialEditor : EditorWindow
    {
        VisualElement container;
        
        ObjectField materialObjectField;
        Button materialApplyButton;
        VisualElement displayMaterial;
        public const string path = "Assets/MaterialTool/Editor/Window/";
        [MenuItem("Tools/Material Tool")]
        public static void ShowWindow()
        {
            MaterialEditor window = GetWindow<MaterialEditor>();
            window.titleContent = new GUIContent("Material Tool");
        }
        void CreateGUI()
        {      
            container = rootVisualElement;
            VisualTreeAsset original = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path + "MaterialEditor.uxml");
            container.Add(original.Instantiate());
        
            StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(path + "MaterialEditor.uss");
            container.styleSheets.Add(styleSheet);

            materialObjectField = container.Q<ObjectField>("materialObjectField");
            materialApplyButton = container.Q<Button>("materialApplyButton");
            materialApplyButton.clicked += ApplyMaterial;    
        }
        void ApplyMaterial()
        {
            List<GameObject> selectedObjects = new List<GameObject>();
            selectedObjects.AddRange(Selection.gameObjects);
            foreach (GameObject gameObject in selectedObjects)
            {
                gameObject.GetComponent<Renderer>().material = materialObjectField.value as Material;
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}