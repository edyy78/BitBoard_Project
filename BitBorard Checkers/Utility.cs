using System;

public class Utility
{
	public Class1()
	{
	}

	public int getBit(uint bitboard, int bitLocation)
	{
		return (bitboard & (1 << bitLocation)) != 0;
	}
}
