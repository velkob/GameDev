#include <iostream>

//Finds the day of the week using Zellers Rule
int findDayOfWeek(int year, int month, int day)
{
	month == 1 || month == 2 ? year-- : 0;
	int ZelerMonths[12] = { 11,12,1,2,3,4,5,6,7,8,9,10 };
	return (day + ((13 * ZelerMonths[month] - 1) / 5) + year + ((year % 100) / 4) + ((year / 100) / 4) - 2 * (year / 100)) % 7;
}


void fillCalendar(int calendar[28][18], int year)
{
	int monthDays[12] = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
	if (((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0))
	{
		monthDays[1] = 29;
	}
	//find first and last day of a month
	//fill the current month in corresponding spaces
	for (int month = 0; month < 12; month++)
	{
		int fourMonthsCounter = 0;
		int day = 1;
		int firstDay = findDayOfWeek(year, month, 1);
		int lastDay = findDayOfWeek(year, month, monthDays[month]);
		for (int row = month*6; row < month*6 + 6; row++)
		{
			for (int col = fourMonthsCounter * 7; col < fourMonthsCounter * 7 + 7; col++)
			{
				for (int indent = 0; indent < firstDay; indent++)
				{
					calendar[row][col++] = 0;
				}
				calendar[row][col] = day++;
			}
		}

		day = 1;
		fourMonthsCounter == 3 ? fourMonthsCounter = 0 : fourMonthsCounter++;
	}
}

int main()
{
	int calendar[28][18];
	int year;
	do
	{
		std::cin >> year;
	} while (year < 0);


}
