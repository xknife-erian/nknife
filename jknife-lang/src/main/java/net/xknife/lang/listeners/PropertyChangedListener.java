package net.xknife.lang.listeners;

import java.util.EventListener;
import java.util.UUID;

public abstract class PropertyChangedListener<T> implements EventListener
{
	private final String _Id = UUID.randomUUID().toString();

	public abstract void onPropertyChanged(T sender, String propertyName);

	public String getId()
	{
		return _Id;
	}

	@Override
	public int hashCode()
	{
		final int prime = 31;
		int result = 1;
		result = (prime * result) + ((_Id == null) ? 0 : _Id.hashCode());
		return result;
	}

	@Override
	public boolean equals(final Object obj)
	{
		if (this == obj)
		{
			return true;
		}
		if (obj == null)
		{
			return false;
		}
		if (getClass() != obj.getClass())
		{
			return false;
		}
		PropertyChangedListener<?> other = (PropertyChangedListener<?>) obj;
		if (_Id == null)
		{
			if (other._Id != null)
			{
				return false;
			}
		}
		else if (!_Id.equals(other._Id))
		{
			return false;
		}
		return true;
	}

}
