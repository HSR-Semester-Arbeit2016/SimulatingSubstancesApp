using System;

public class TriedToDeleteDefaultConfigException : Exception
{
	
	public TriedToDeleteDefaultConfigException ()
	{
	}

	public TriedToDeleteDefaultConfigException (string message)
		: base (message)
	{
	}

	public TriedToDeleteDefaultConfigException (string message, Exception inner)
		: base (message, inner)
	{
	}
}
