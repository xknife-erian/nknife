package jknife.fakes;

import java.util.List;

import org.joda.time.DateTime;

public class Bike
{
	private String id;
	private String name;
	private boolean isBlack;
	private int weight;
	private DateTime buyTime;
	private String brand;
	private List<Short> gears;

	public String getId()
	{
		return id;
	}

	public void setId(final String id)
	{
		this.id = id;
	}

	public String getName()
	{
		return name;
	}

	public void setName(final String name)
	{
		this.name = name;
	}

	public boolean isBlack()
	{
		return isBlack;
	}

	public void setIsBlack(final boolean isBlack)
	{
		this.isBlack = isBlack;
	}

	public int getWeight()
	{
		return weight;
	}

	public void setWeight(final int weight)
	{
		this.weight = weight;
	}

	public DateTime getBuyTime()
	{
		return buyTime;
	}

	public void setBuyTime(final DateTime buyTime)
	{
		this.buyTime = buyTime;
	}

	public String getBrand()
	{
		return brand;
	}

	public void setBrand(final String brand)
	{
		this.brand = brand;
	}

	public List<Short> getGears()
	{
		return gears;
	}

	public void setGears(final List<Short> gears)
	{
		this.gears = gears;
	}
}
