#include <iostream>
#include <cassert>
#include <string>
#include <Windows.h>

using namespace std;

INT FindMinimum(LPINT s, INT arrSize);
INT CalculateFactorial(INT n);
BOOL ContainsSub(LPSTR toCheck, INT checkedSize, LPSTR toFind, INT foundSize, BOOL caseSensitive);
DOUBLE AverageValue(LPINT numArr, INT arrSize);

// Функція знаходження мінімального елементу
INT FindMinimum(LPINT s, INT arrSize) 
{
	if (arrSize == 0)
	{
		return 0;
	}

	INT min = s[0];

	for (INT i = 1; i < arrSize / sizeof(min); ++i)
	{
		if (s[i] < min)
		{
			min = s[i];
		}
	}

	return min;
}

// Функція для обчислення факторіалу числа.
INT CalculateFactorial(INT n)
{
	if (n < 0) {
		return -1;
	}
	else if (n == 0 || n == 1) {
		return 1;
	}
	else {
		int factorial = 1;
		for (int i = 2; i <= n; ++i) {
			factorial *= i;
		}
		return factorial;
	}
}

// Функція, яка перевіряє, чи містить заданий LPSTR певний підрядок LPSTR, 
// з можливістю ввімкнення або вимкнення чутливості до регістру символів за допомогою параметру BOOL.
BOOL ContainsSub(LPSTR toCheck, INT checkedSize, LPSTR toFind, INT foundSize, BOOL caseSensitive)
{
	INT checkedLen = checkedSize - 1;
	INT foundLen = foundSize - 1;

	for (INT i = 0; i <= checkedLen - foundLen; ++i)
	{
		if (checkedLen < foundLen + i)
		{
			return false;
		}

		BOOL result = true;

		for (INT j = 0; j < foundLen; ++j)
		{
			CHAR left = toCheck[i + j];
			CHAR right = toFind[j];

			if (!caseSensitive)
			{
				left = tolower(left);
				right = tolower(right);
			}

			if (left != right)
			{
				result = false;
				break;
			}
		}
		if (result)
		{
			return true;
		}
	}
	return false;
}

// Функція знаходження середнього значення
DOUBLE AverageValue(LPINT numArr, INT arrSize)
{
	if (arrSize == 0)
	{
		return 0;
	}

	INT arrLen = arrSize / sizeof(*numArr);
	DOUBLE sum = 0;

	for (INT i = 0; i < arrLen; ++i)
	{
		sum += numArr[i];
	}

	return sum / arrLen;
}


int main()
{
	// Тестування функції FindMinimum
	int arrFindMin[5] = { 5, -3, 8, 1, 0 };
	int minResult = FindMinimum(arrFindMin, sizeof(arrFindMin));
	assert(minResult == -3);
	cout << "FindMinimum function test passed." << endl;


	// Тестування функції CalculateFactorial
	// Тест 1: Позитивне число
	int n1 = 5;
	int expected1 = 120;
	assert(CalculateFactorial(n1) == expected1);

	// Тест 2: Нуль
	int n2 = 0;
	int expected2 = 1;
	assert(CalculateFactorial(n2) == expected2);

	// Тест 3: Від'ємне число
	int n3 = -3;
	int expected3 = -1;
	assert(CalculateFactorial(n3) == expected3);
	cout << "CalculateFactorial tests passed." << endl;


	// Тестування функції ContainsSub
	char toCheck[] = "Hello, World!";
	char toFind[] = "World";
	bool containsResult = ContainsSub(toCheck, sizeof(toCheck), toFind, sizeof(toFind), true);
	assert(containsResult);

	char toCheck2[] = "Hello, World!";
	char toFind2[] = "world";  // Маленька 'w', оскільки регістр не враховується
	bool containsResult2 = ContainsSub(toCheck, sizeof(toCheck), toFind, sizeof(toFind), false);
	assert(containsResult2);
	cout << "ContainsSub function tests passed." << endl;


	// Тестування функції AverageValue
	int numArr[5] = { 3, 5, 7, 10, 2 }; // Average = 5.4
	double averageResult = AverageValue(numArr, sizeof(numArr));
	assert(averageResult == 5.4);
	cout << "AverageValue function test passed." << endl;

	return 0;
}