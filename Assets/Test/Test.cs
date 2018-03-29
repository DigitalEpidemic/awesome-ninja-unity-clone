using UnityEngine;
using System.Threading;

public class Test : MonoBehaviour
{
	Thread mThread;
	int mVal0 = 0;
	int mVal1 = 0;
	int mVal2 = 0;
	int mVal3 = 0;
	float mNext = 0f;
	long mNext2 = 0;

	void OnEnable ()
	{
		mThread = new Thread(Increm0);
		mThread.Start();
		InvokeRepeating("Increm1", 0.001f, 1f);
	}

	void OnDisable ()
	{
		mThread.Abort();
		mThread = null;
	}

	void Update ()
	{
		float time = Time.time;
		long mMyTime = System.DateTime.UtcNow.Ticks / 10000;

		if (time > mNext)
		{
			mNext = time + 1f;
			++mVal2;
		}

		if (mMyTime > mNext2)
		{
			mNext2 = mMyTime + 1000;
			++mVal3;
		}
	}

	void Increm0 ()
	{
		for (; ; )
		{
			++mVal0;
			Thread.Sleep(1000);
		}
	}

	void Increm1 () { ++mVal1; }

	void OnGUI ()
	{
		GUILayout.Label(mVal0 + " " + mVal1 + " " + mVal2 + " " + mVal3);
	}
}
