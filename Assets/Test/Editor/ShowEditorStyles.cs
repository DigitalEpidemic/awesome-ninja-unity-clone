using UnityEditor;
using UnityEngine;

public class ShowEditorStyles : EditorWindow
{
	[MenuItem("Window/ShowEditorStyles")]
	public static void Init ()
	{
//		ShowEditorStyles window = GetWindow<ShowEditorStyles>();
//		window.title = "ShowEditorStyles";
	}

	private static Vector2 scroll = Vector2.zero;
	private static bool isSelected = false;
	private static GUISkin skin;

	private static bool isInspector = true;
	private static bool isScene = false;
	private static bool isGame = false;

	private static float width = 150;
	private static float height = 20;
	private static string text = "Hello World";

	public void OnGUI ()
	{
		width = EditorGUILayout.Slider("Width", width, 0, 200);
		height = EditorGUILayout.Slider("Height", height, 0, 50);
		text = EditorGUILayout.TextField("Text", text);

		PrintHeader();
		scroll = GUILayout.BeginScrollView(scroll);

		isInspector = EditorGUILayout.Foldout(isInspector, "Inspector skin");
		if (isInspector)
		{
			PrintStyles(LoadGUISkin(EditorSkin.Inspector));
		}

		isScene = EditorGUILayout.Foldout(isScene, "Scene skin");
		if (isScene)
		{
			PrintStyles(LoadGUISkin(EditorSkin.Scene));
		}

		isGame = EditorGUILayout.Foldout(isGame, "Game skin");
		if (isGame)
		{
			PrintStyles(LoadGUISkin(EditorSkin.Game));
		}
		GUILayout.EndScrollView();
	}

	private static void PrintHeader ()
	{
		GUILayout.BeginHorizontal();

		GUILayout.Label("Name", GUILayout.Width(200));

		GUILayout.Label("Label", GUILayout.Width(150));

		GUILayout.Label("Button", GUILayout.Width(150));

		GUILayout.Label("Textfield", GUILayout.Width(150));

		GUILayout.Label("Toggle", GUILayout.Width(150));

		EditorGUILayout.EndHorizontal();
	}

	private static void PrintStyles (GUISkin skin)
	{
		EditorGUILayout.BeginVertical();
		foreach (GUIStyle style in skin.customStyles)
		{
			GUILayout.BeginHorizontal();

			GUILayout.Label("[" + style.fixedWidth + "," + style.fixedHeight + "] " + style.name, GUILayout.MinWidth(200), GUILayout.Width(200), GUILayout.MaxHeight(height));

			GUILayout.Label(text, style, GUILayout.MaxWidth(width), GUILayout.MaxHeight(height));

			GUILayout.Space(150 - width);

			GUILayout.Button(text, style, GUILayout.MaxWidth(width), GUILayout.MaxHeight(height));

			GUILayout.Space(150 - width);

			EditorGUILayout.TextField(text, style, GUILayout.MaxWidth(width), GUILayout.MaxHeight(height));

			GUILayout.Space(150 - width);

			isSelected = GUILayout.Toggle(isSelected, text, style, GUILayout.MaxWidth(width), GUILayout.MaxHeight(height));

			GUILayout.Space(150 - width);

			EditorGUILayout.EndHorizontal();
		}
		EditorGUILayout.EndVertical();
	}

	public static GUISkin LoadGUISkin (EditorSkin editorSkin)
	{
		if (skin != null)
		{
			return skin;
		}

		skin = EditorGUIUtility.GetBuiltinSkin(editorSkin);

		return skin;
	}
}