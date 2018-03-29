/*using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.Reflection;

namespace Tasharen
{
/// <summary>
/// This class makes it possible to export an entire hierarchy of game objects with their components.
/// You can register a component to be serialized, then bind the properties you wish auto-serialized.
/// You can also optionally specify a custom serialization function to be called in addition to
/// the auto-serialized properties, in case you want to go through an upgrade or conversion path.
/// </summary>

public static class SceneSerializer
{
	public delegate void SerializationFunc (DataNode parent, Component comp, bool write);

	/// <summary>
	/// Slightly expanded property binding with an explicitly specified node name.
	/// When the property gets serialized, the DataNode's name will be the 'nodeName'.
	/// </summary>

	public class PropertyEntry : BoundField
	{
		public string nodeName;

		public PropertyEntry (object target, string propertyName, string nodeName) : base(target, propertyName) { this.nodeName = nodeName; }
		public PropertyEntry (Type type, string propertyName, string nodeName) : base(type, propertyName) { this.nodeName = nodeName; }
	}

	/// <summary>
	/// Serializable component entry containing its type, name of the serialized node,
	/// a list of auto-serialized properties, and an optional serialization function.
	/// </summary>

	public class ComponentEntry
	{
		public Type type;
		public string nodeName;
		public List<PropertyEntry> props = new List<PropertyEntry>();
		public SerializationFunc func;

		/// <summary>
		/// Bind a new property on the current component.
		/// </summary>

		public void Bind (string nodeName, string propertyName)
		{
			PropertyEntry ent = new PropertyEntry(type, propertyName, nodeName);
			if (ent.isValid) props.Add(ent);
			else Debug.LogWarning(type + "." + propertyName + " doesn't exist");
		}
	}

	// List of registered serializable components
	static List<ComponentEntry> mComponents = new List<ComponentEntry>();

	/// <summary>
	/// Begin the serialization by clearing the existing callbacks and registering the common ones.
	/// </summary>

	static public void Init ()
	{
		mComponents.Clear();

		Register<Transform>(OnTransform);

		ComponentEntry ent = Register<Camera>();
		ent.Bind("Clear Flags", "clearFlags");
		ent.Bind("Clear Color", "backgroundColor");
		ent.Bind("Culling", "cullingMask");
		ent.Bind("Orthographic", "orthographic");
		ent.Bind("Size", "orthographicSize");
		ent.Bind("Near", "nearClipPlane");
		ent.Bind("Far", "farClipPlane");
		ent.Bind("Viewport", "rect");
		ent.Bind("Depth", "depth");
		ent.Bind("Occlusion", "useOcclusionCulling");
		ent.Bind("HDR", "hdr");
	}

	/// <summary>
	/// Register a new serializable component.
	/// </summary>

	static public ComponentEntry Register<T> () where T : Component
	{
		return Register<T>(DataNode.GetTypeName(typeof(T)), null);
	}

	/// <summary>
	/// Register a new serializable component.
	/// </summary>

	static public ComponentEntry Register<T> (SerializationFunc func) where T : Component
	{
		return Register<T>(DataNode.GetTypeName(typeof(T)), func);
	}

	/// <summary>
	/// Register a new serializable component.
	/// </summary>

	static public ComponentEntry Register<T> (string name) where T : Component
	{
		return Register<T>(name, null);
	}

	/// <summary>
	/// Register a new serializable component.
	/// </summary>

	static public ComponentEntry Register<T> (string name, SerializationFunc func) where T : Component
	{
		ComponentEntry ent = new ComponentEntry();
		ent.nodeName = name;
		ent.type = typeof(T);
		ent.func = func;
		mComponents.Add(ent);
		return ent;
	}

	/// <summary>
	/// Register a new read-only type. Use this function if you want to be able to read data, but not write it.
	/// Best use case: reading legacy layouts, or converting layouts from one system to another.
	/// </summary>

	static public void Register (string name, SerializationFunc func)
	{
		ComponentEntry ent = new ComponentEntry();
		ent.nodeName = name;
		ent.type = typeof(void);
		ent.func = func;
		mComponents.Add(ent);
	}

	/// <summary>
	/// Export the specified transform into the data node.
	/// </summary>

	static public void Export (DataNode parent, Transform t)
	{
		Component[] comps = t.GetComponents(typeof(Component));
		DataNode node = parent.AddChild("GameObject", t.name);
		node.AddChild("Layer", t.gameObject.layer);

		// Serialize all supported components
		for (int i = 0; i < comps.Length; ++i)
		{
			Component comp = comps[i];
			Type type = comp.GetType();
			ComponentEntry ent = GetSerializableComponent(type);
			if (ent != null) Export(ent, node.AddChild(ent.nodeName), comp);
		}

		// Serialize all children
		if (t.childCount > 0)
		{
			DataNode child = node.AddChild("Children");
			for (int i = 0; i < t.childCount; ++i)
				Export(child, t.GetChild(i));
		}
	}

	/// <summary>
	/// Export the specified component into the data node.
	/// </summary>

	static void Export (ComponentEntry ent, DataNode root, Component comp)
	{
		// Run through all auto-serialized properties
		for (int i = 0; i < ent.props.Count; ++i)
		{
			PropertyEntry pro = ent.props[i];
			
			if (pro.canRead)
			{
				object obj = pro.Get(comp);
				if (obj != null) root.AddChild(pro.nodeName, obj);
			}
		}

		// Call the optional serialization function
		if (ent.func != null)
			ent.func(root, comp, true);
	}

	/// <summary>
	/// Import the previously exported object.
	/// </summary>

	static public void Import (DataNode root) { ImportChildren(root, null); }

	/// <summary>
	/// Helper function to retrieve the reference ID on the specified component.
	/// </summary>

	static public int GetReferenceID (Component comp, string fieldName)
	{
		SerializedObject ob = new SerializedObject(comp);
		SerializedProperty sp = ob.FindProperty(fieldName);
		return (sp != null) ? sp.objectReferenceInstanceIDValue : 0;
	}

	/// <summary>
	/// Helper function to retrieve the reference ID on the specified component.
	/// </summary>

	static public void SetReferenceID (Component comp, string fieldName, int value)
	{
		SerializedObject ob = new SerializedObject(comp);
		SerializedProperty sp = ob.FindProperty(fieldName);

		if (sp != null)
		{
			ob.Update();
			sp.objectReferenceInstanceIDValue = value;
			ob.ApplyModifiedProperties();
		}
	}

#region Private Functions

	/// <summary>
	/// Get a ComponentEntry by type.
	/// </summary>

	static ComponentEntry GetSerializableComponent (Type type)
	{
		for (int i = 0; i < mComponents.Count; ++i)
			if (mComponents[i].type == type)
				return mComponents[i];
		return null;
	}

	/// <summary>
	/// Get a ComponentEntry by name.
	/// </summary>

	static ComponentEntry GetSerializableComponent (string name)
	{
		for (int i = 0; i < mComponents.Count; ++i)
			if (mComponents[i].nodeName == name)
				return mComponents[i];
		return null;
	}
	
	/// <summary>
	/// Find the root transform matching the specified name.
	/// </summary>

	static Transform FindRoot (string name, int index)
	{
		GameObject[] gos = (GameObject[])UnityEngine.Object.FindObjectsOfType(typeof(GameObject));

		int counter = 0;

		for (int i = 0; i < gos.Length; ++i)
		{
			GameObject go = gos[i];
			if (go.transform.parent != null) continue;
			if (go.name == name && index == counter++) return go.transform;
		}
		return null;
	}

	/// <summary>
	/// Import the previously exported object.
	/// </summary>

	static void ImportObject (DataNode root, Transform t)
	{
		for (int i = 0; i < root.children.Count; ++i)
		{
			DataNode node = root.children[i];

			if (node.name == "Layer")
			{
				t.gameObject.layer = node.Get<int>();
			}
			else if (node.name == "Children")
			{
				ImportChildren(node, t);
			}
			else
			{
				for (int b = 0; b < mComponents.Count; ++b)
				{
					ComponentEntry ent = mComponents[b];

					if (ent.nodeName == node.name)
					{
						Component comp = t.GetComponent(ent.type);
						if (comp == null && ent.type.IsSubclassOf(typeof(Component)))
							comp = t.gameObject.AddComponent(ent.type);
						if (comp != null) ImportComponent(node, comp, ent);
						break;
					}
				}
			}
		}
	}

	/// <summary>
	/// Import the specified child node using both auto-serialized properties and the optional serialization function.
	/// </summary>

	static void ImportComponent (DataNode root, Component comp, ComponentEntry ent)
	{
		for (int b = 0; b < root.children.Count; ++b)
		{
			DataNode node = root.children[b];

			for (int i = 0; i < ent.props.Count; ++i)
			{
				PropertyEntry pro = ent.props[i];

				if (pro.nodeName == node.name)
				{
					object val = node.Get(pro.type);

					if (val != null)
					{
						pro.Set(comp, val);
					}
					else
					{
						Debug.LogError("Failed to set " + pro.name + " (" + pro.type + ") from " + node.type);
					}
					break;
				}
			}
		}

		// Optional function
		if (ent.func != null) ent.func(root, comp, false);
	}

	/// <summary>
	/// Import the UI hierarchy from the specified data node.
	/// </summary>

	static void ImportChildren (DataNode root, Transform parent)
	{
		for (int i = 0; i < root.children.Count; ++i)
		{
			DataNode node = root.children[i];

			if (node.name == "GameObject")
			{
				string goName = node.Get<string>();
				Transform trans = (parent != null && i < parent.childCount) ?
					parent.GetChild(i) : FindRoot(goName, i);

				if (trans == null || trans.name != goName)
				{
					GameObject go = new GameObject(goName);
					trans = go.transform;

					if (parent != null)
					{
						go.transform.parent = parent;
						go.transform.localPosition = Vector3.zero;
						go.transform.localRotation = Quaternion.identity;
						go.transform.localScale = Vector3.one;
						go.layer = parent.gameObject.layer;
					}
				}

				// Continue importing the object
				ImportObject(node, trans);
			}
		}
	}

	/// <summary>
	/// Transform component serialization.
	/// </summary>

	static void OnTransform (DataNode root, Component comp, bool write)
	{
		Transform t = comp as Transform;

		if (write)
		{
			root.AddChild("P", t.localPosition);
			root.AddChild("R", t.localRotation.eulerAngles);
			root.AddChild("S", t.localScale);
		}
		else
		{
			DataNode node = root.FindChild("P");
			if (node != null) t.localPosition = node.Get<Vector3>();

			node = root.FindChild("R");
			if (node != null) t.localRotation = Quaternion.Euler(node.Get<Vector3>());

			node = root.FindChild("S");
			if (node != null) t.localScale = node.Get<Vector3>();
		}
	}
#endregion
}
}
*/
