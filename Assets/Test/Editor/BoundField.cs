using System.Reflection;
using System;
using System.Diagnostics;

namespace Tasharen
{
/// <summary>
/// Property binding is a class that makes it possible to abstractly bind to a property or a field of a class using a single interface.
/// </summary>

public class BoundField
{
	Type mObjectType;
	object mObject;
	FieldInfo mField;
	PropertyInfo mProperty;
	string mName;

	/// <summary>
	/// Name of the field or property that this class is bound to.
	/// </summary>

	[DebuggerHidden]
	public string name { get { return mName; } }

	/// <summary>
	/// Whether the property binding has a valid property to work with.
	/// </summary>

	public bool isValid { get { return (mProperty != null || mField != null); } }

	/// <summary>
	/// Whether the property binding has a valid property to work with and it's readable.
	/// </summary>

	public bool canRead { get { return ((mProperty != null && mProperty.CanRead) || mField != null); } }

	/// <summary>
	/// Whether the property binding has a valid property to work with and it's writable.
	/// </summary>

	public bool canWrite { get { return ((mProperty != null && mProperty.CanWrite) || mField != null); } }

	/// <summary>
	/// Whether the property binding has an object reference to work with.
	/// </summary>

	public bool hasObjectReference { get { return (mObject != null); } }

	/// <summary>
	/// Value data type used by the property binding.
	/// </summary>

	public Type type
	{
		get
		{
			if (mProperty != null) return mProperty.PropertyType;
			if (mField != null) return mField.FieldType;
			return typeof(void);
		}
	}

	/// <summary>
	/// Create a new property binding.
	/// </summary>

	public BoundField (object obj, string name)
	{
		mName = name;
		mObject = obj;
		mObjectType = mObject.GetType();
		mField = mObjectType.GetField(name);
		mProperty = mObjectType.GetProperty(name);
	}

	/// <summary>
	/// Create a new property binding.
	/// </summary>

	public BoundField (Type type, string name)
	{
		mName = name;
		mObjectType = type;
		mField = mObjectType.GetField(name) ;
		mProperty = mObjectType.GetProperty(name);
	}

	/// <summary>
	/// Assign the bound property's value.
	/// </summary>

	[DebuggerHidden]
	[DebuggerStepThrough]
	public bool Set (object value) { return Set(mObject, value); }

	/// <summary>
	/// Assign the bound property's value.
	/// </summary>

	[DebuggerHidden]
	[DebuggerStepThrough]
	public bool Set (object target, object value)
	{
		if (mProperty != null)
		{
			if (mProperty.CanWrite && mProperty.PropertyType.IsAssignableFrom(value.GetType()))
			{
				mProperty.SetValue(target, value, null);
				return true;
			}
		}
		else if (mField != null)
		{
			if (mField.FieldType.IsAssignableFrom(value.GetType()))
			{
				mField.SetValue(target, value);
				return true;
			}
		}
		return false;
	}

	/// <summary>
	/// Retrieve the property's value.
	/// </summary>

	[DebuggerHidden]
	[DebuggerStepThrough]
	public object Get () { return Get(mObject); }

	/// <summary>
	/// Retrieve the property's value.
	/// </summary>

	[DebuggerHidden]
	[DebuggerStepThrough]
	public object Get (object target)
	{
		if (mProperty != null)
		{
			if (mProperty.CanRead)
				return mProperty.GetValue(target, null);
		}
		else if (mField != null)
		{
			return mField.GetValue(target);
		}
		return null;
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public T Get<T> () { return Get<T>(mObject); }

	/// <summary>
	/// Retrieve the property's value of specified type.
	/// </summary>

	[DebuggerHidden]
	[DebuggerStepThrough]
	public T Get<T> (object target)
	{
		if (mProperty != null)
		{
			if (mProperty.CanRead)
				return (T)mProperty.GetValue(target, null);
		}
		else if (mField != null)
		{
			return (T)mField.GetValue(target);
		}
		return default(T);
	}
}
}
