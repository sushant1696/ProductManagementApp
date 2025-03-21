namespace ProductManagementAPI.Common
{
	public class CommonUtility
	{
		public int GenerateOrderID(int strPrifix, int CodeSize)
		{
			int strOrderNumber = 0;
			Random rnd = new Random();
			char[] letter = "0123456789".ToArray();

			int iPrifixCount = strPrifix;
			int OrderCount = CodeSize - iPrifixCount;
			int iAlphabetLimit = 0;
			int iNumberLimit = 0;

			if ((OrderCount % 2) == 0)
			{
				if ((OrderCount / 2) != 1)
				{
					iAlphabetLimit = OrderCount / 2;
					iNumberLimit = iAlphabetLimit;
				}
				else
				{
					iAlphabetLimit = 1;
					iNumberLimit = 1;
				}
			}
			else
			{
				int CurrentOC = OrderCount - 1;
				if (CurrentOC == 0)
				{
					iAlphabetLimit = 1;
					iNumberLimit = 0;
				}
				if ((CurrentOC / 2) != 1)
				{
					iAlphabetLimit = CurrentOC / 2;
					iNumberLimit = iAlphabetLimit + 1;
				}
				else
				{
					iAlphabetLimit = 1;
					iNumberLimit = 1;
				}
			}


			int iAlphaCount = 0, iCount = 0, count = 0;

			try
			{
				while (count < OrderCount)
				{
					if (rnd.Next() % 2 == 0 && iAlphaCount < iAlphabetLimit)
					{
						strOrderNumber += letter[rnd.Next(0, letter.Length)];
						iAlphaCount++;
						count++;
					}
					else if (iCount < iNumberLimit)
					{
						strOrderNumber += rnd.Next(0, 9);
						iCount++;
						count++;
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
			strOrderNumber = strPrifix + strOrderNumber;
			return strOrderNumber;
		}

	}
}
