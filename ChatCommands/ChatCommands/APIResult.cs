using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class APIResult
{
	public String msg;
	public DataDeserialiser data;
}

public class DataDeserialiser
{
	public int rating;
	public int rank;
	public int gold;
	public int scrolls;
	public String name;
}