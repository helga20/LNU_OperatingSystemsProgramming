#include "WTypesbase.h"
#include <assert.h>
#include <iostream>
using namespace std;

extern "C" __declspec(dllexport) INT(__stdcall FindMinimum) (LPINT s, INT arrSize);
extern "C" __declspec(dllexport) INT(__stdcall CalculateFactorial) (INT n);
extern "C" __declspec(dllexport) BOOL ContainsSub(LPSTR toCheck, INT checkedSize, LPSTR toFind, INT foundSize, BOOL caseSensitive);
extern "C" __declspec(dllexport) DOUBLE AverageValue(LPINT numArr, INT arrSize);

int main()
{
	// ���������� ������� FindMinimum
	int arrFindMin[5] = { 5, -3, 8, 1, 0 };
	int minResult = FindMinimum(arrFindMin, sizeof(arrFindMin));
	assert(minResult == -3);
	cout << "FindMinimum function test passed." << endl;


	// ���������� ������� CalculateFactorial
	// ���� 1: ��������� �����
	int n1 = 5;
	int expected1 = 120;
	assert(CalculateFactorial(n1) == expected1);

	// ���� 2: ����
	int n2 = 0;
	int expected2 = 1;
	assert(CalculateFactorial(n2) == expected2);

	// ���� 3: ³�'���� �����
	int n3 = -3;
	int expected3 = -1;
	assert(CalculateFactorial(n3) == expected3);
	cout << "CalculateFactorial tests passed." << endl;


	// ���������� ������� ContainsSub
	char toCheck[] = "Hello, World!";
	char toFind[] = "World";
	bool containsResult = ContainsSub(toCheck, sizeof(toCheck), toFind, sizeof(toFind), true);
	assert(containsResult);

	char toCheck2[] = "Hello, World!";
	char toFind2[] = "world";  // �������� 'w', ������� ������ �� �����������
	bool containsResult2 = ContainsSub(toCheck, sizeof(toCheck), toFind, sizeof(toFind), false);
	assert(containsResult2);
	cout << "ContainsSub function tests passed." << endl;


	// ���������� ������� AverageValue
	int numArr[5] = { 3, 5, 7, 10, 2 }; // Average = 5.4
	double averageResult = AverageValue(numArr, sizeof(numArr));
	assert(averageResult == 5.4);
	cout << "AverageValue function test passed." << endl;

	cout << "Press any key to continue . . . ";
	cin.get();


	return 0;
}