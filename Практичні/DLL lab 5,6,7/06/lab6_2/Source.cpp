#include "WTypesbase.h"
#include <assert.h>
#include <iostream>
using namespace std;

typedef INT(*TFindMinimum) (LPINT s, INT arrSize);
typedef INT(*TCalculateFactorial) (INT n);
typedef BOOL(*TContainsSub)(LPSTR toCheck, INT checkedSize, LPSTR toFind, INT foundSize, BOOL caseSensitive);
typedef DOUBLE(*TAverageValue)(LPINT numArr, INT arrSize);

int main()
{
	const wchar_t dllPath[] = L"C:\\MyLib\\MyLib.dll";
	HINSTANCE hDLL = LoadLibrary(dllPath);
	if (hDLL == NULL)
	{
		cout << "Error! DLL not founded";
		return 0;
	}

	TFindMinimum findMinimum = (TFindMinimum)GetProcAddress(hDLL, "FindMinimum");
	if (!findMinimum)
	{
		cout << "Function FindMinimum not founded";
		return 0;
	}

	TCalculateFactorial calculateFactorial = (TCalculateFactorial)GetProcAddress(hDLL, "CalculateFactorial");
	if (!calculateFactorial)
	{
		cout << "Function CalculateFactorial not founded";
		return 0;
	}

	TContainsSub containsSub = (TContainsSub)GetProcAddress(hDLL, "ContainsSub");
	if (!containsSub)
	{
		cout << "Function ContainsSub not founded";
		return 0;
	}

	TAverageValue averageValue = (TAverageValue)GetProcAddress(hDLL, "AverageValue");
	if (!averageValue)
	{
		cout << "Function AverageValue not founded";
		return 0;
	}

	// ���������� ������� FindMinimum
	int arrFindMin[5] = { 5, -3, 8, 1, 0 };
	int minResult = findMinimum(arrFindMin, sizeof(arrFindMin));
	assert(minResult == -3);
	cout << "FindMinimum function test passed." << endl;


	// ���������� ������� CalculateFactorial
	// ���� 1: ��������� �����
	int n1 = 5;
	int expected1 = 120;
	assert(calculateFactorial(n1) == expected1);

	// ���� 2: ����
	int n2 = 0;
	int expected2 = 1;
	assert(calculateFactorial(n2) == expected2);

	// ���� 3: ³�'���� �����
	int n3 = -3;
	int expected3 = -1;
	assert(calculateFactorial(n3) == expected3);
	cout << "CalculateFactorial tests passed." << endl;


	// ���������� ������� ContainsSub
	char toCheck[] = "Hello, World!";
	char toFind[] = "World";
	bool containsResult = containsSub(toCheck, sizeof(toCheck), toFind, sizeof(toFind), true);
	assert(containsResult);

	char toCheck2[] = "Hello, World!";
	char toFind2[] = "world";  // �������� 'w', ������� ������ �� �����������
	bool containsResult2 = containsSub(toCheck, sizeof(toCheck), toFind, sizeof(toFind), false);
	assert(containsResult2);
	cout << "ContainsSub function tests passed." << endl;


	// ���������� ������� AverageValue
	int numArr[5] = { 3, 5, 7, 10, 2 }; // Average = 5.4
	double averageResult = averageValue(numArr, sizeof(numArr));
	assert(averageResult == 5.4);
	cout << "AverageValue function test passed." << endl;

	FreeLibrary(hDLL);

	cout << "Press any key to continue . . . ";
	cin.get();

	return 0;
}