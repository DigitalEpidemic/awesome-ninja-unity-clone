/*using UnityEngine;
using UnityEditor;
using Tasharen;
using System.IO;
using System.Collections.Generic;
using System;

public class ExportUI : EditorWindow
{
	[MenuItem("NGUI/Open/Export Tool")]
	static public void OpenExporter ()
	{
		EditorWindow.GetWindow<ExportUI>(false, "Export Tool", true);
	}

	void OnGUI ()
	{
		EditorGUILayout.Space();
		NGUIEditorTools.SetLabelWidth(80f);
		string text = EditorPrefs.GetString("Export Name", "MyUI.txt");

		GUI.changed = false;

		GUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Export As");
		text = GUILayout.TextArea(text);
		bool export = GUILayout.Button("Export", GUILayout.Width(80f));
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Import As");
		text = GUILayout.TextArea(text);
		bool import = GUILayout.Button("Import", GUILayout.Width(80f));
		GUILayout.EndHorizontal();

		if (GUI.changed) EditorPrefs.SetString("Export Name", text);

		if (export) Export(text);
		if (import) Import(text);
	}

	/// <summary>
	/// Export the UI hierarchy, saving it as the specified filename.
	/// </summary>

	void Export (string path)
	{
		FileStream fs = File.Open(path, FileMode.Create, FileAccess.Write);

		if (fs != null && fs.CanWrite)
		{
			DataNode root = new DataNode();
			root.name = "Export";

			// TODO: Wouldn't it be better to simply specify the root object to export?
			UIRoot[] canvases = (UIRoot[])FindObjectsOfType(typeof(UIRoot));

			if (canvases.Length > 0)
			{
				for (int i = 0; i < canvases.Length; ++i)
				{
					UIRoot canvas = canvases[i];
					SceneSerializer.Export(root, canvas.transform);
				}
			}
			else Debug.LogWarning("No UI hierarchies found");

			StreamWriter writer = new StreamWriter(fs);
			root.Write(writer);
			writer.Flush();
			writer.Dispose();
			
			Debug.Log("Saved as " + path);
		}
		else Debug.LogError("Unable to write to " + path);
	}

	/// <summary>
	/// Import the UI hierarchy from the specified path.
	/// </summary>

	void Import (string path)
	{
		FileStream fs = File.OpenRead(path);

		if (fs != null && fs.CanRead)
		{
			StreamReader reader = new StreamReader(fs);
			DataNode root = new DataNode();
			root.Read(reader);
			reader.Dispose();
			fs.Close();
			SceneSerializer.Import(root);
			Debug.Log("Imported from " + path);
		}
		else Debug.LogError("Unable to read " + path);
	}

	/// <summary>
	/// Register common types.
	/// </summary>

	void OnEnable ()
	{
		SceneSerializer.Init();

		RegisterWidget<UIWidget>("Widget", null);

		SceneSerializer.ComponentEntry ent = RegisterWidget<UISprite>("Sprite", OnSprite);
		ent.Bind("Texture", "spriteName");
		ent.Bind("Type", "type");
		ent.Bind("Fill Direction", "fillDirection");
		ent.Bind("Fill Amount", "fillAmount");
		ent.Bind("Fill Invert", "invert");

		ent = RegisterWidget<UILabel>("Label", OnLabel);
		ent.Bind("Text", "text");
		ent.Bind("Font Size", "fontSize");
		ent.Bind("Font Style", "fontStyle");
		ent.Bind("Overflow", "overflowMethod");
		ent.Bind("Rich Text", "supportEncoding");
		ent.Bind("Max Lines", "maxLineCount");
		ent.Bind("Effect Style", "effectStyle");
		ent.Bind("Effect Color", "effectColor");
		ent.Bind("Effect Distance", "effectDistance");
		ent.Bind("Symbols", "symbolStyle");
		ent.Bind("Crisp", "keepCrispWhenShrunk");

		ent = SceneSerializer.Register<UIRoot>("Root");
		ent.Bind("Style", "scalingStyle");
		ent.Bind("Height", "manualHeight");
		ent.Bind("Minimum", "minimumHeight");
		ent.Bind("Maximum", "maximumHeight");
	}

	/// <summary>
	/// Register a component type that derives from UIWidget.
	/// </summary>

	SceneSerializer.ComponentEntry RegisterWidget<T> (string name, SceneSerializer.SerializationFunc func) where T : UIWidget
	{
		SceneSerializer.ComponentEntry ent = SceneSerializer.Register<T>(name, func);
		ent.Bind("Width", "width");
		ent.Bind("Height", "height");
		ent.Bind("Depth", "depth");
		ent.Bind("Pivot", "rawPivot");
		ent.Bind("Color", "color");
		return ent;
	}

	/// <summary>
	/// UISprite component serialization.
	/// </summary>

	static void OnSprite (DataNode root, Component comp, bool write)
	{
		UISprite sp = comp as UISprite;

		if (write)
		{
			root.AddChild("Atlas", SceneSerializer.GetReferenceID(sp, "mAtlas"));
		}
		else
		{
			DataNode node = root.FindChild("Atlas");
			if (node != null) SceneSerializer.SetReferenceID(sp, "mAtlas", node.Get<int>());
		}
	}

	/// <summary>
	/// UILabel component serialization.
	/// </summary>

	static void OnLabel (DataNode root, Component comp, bool write)
	{
		UILabel lbl = comp as UILabel;

		if (write)
		{
			// TODO: These references are useless. They lose their references as soon as Unity gets restarted.
			if (lbl.trueTypeFont != null) root.AddChild("Font", SceneSerializer.GetReferenceID(lbl, "mTrueTypeFont"));
			else if (lbl.bitmapFont != null) root.AddChild("Bitmap", SceneSerializer.GetReferenceID(lbl, "mFont"));
		}
		else
		{
			DataNode node = root.FindChild("Font");
			if (node != null) SceneSerializer.SetReferenceID(lbl, "mTrueTypeFont", node.Get<int>());

			node = root.FindChild("Bitmap");
			if (node != null) SceneSerializer.SetReferenceID(lbl, "mFont", node.Get<int>());
		}
	}
}
*/
