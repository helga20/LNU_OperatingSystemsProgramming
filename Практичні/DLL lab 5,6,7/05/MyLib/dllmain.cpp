// dllmain.cpp : Defines the entry point for the DLL application.
#include "framework.h"
#include "WTypesbase.h"

extern "C" __declspec(dllexport) INT(__stdcall FindMinimum) (LPINT s, INT arrSize);
extern "C" __declspec(dllexport) INT(__stdcall CalculateFactorial) (INT n);
extern "C" __declspec(dllexport) BOOL ContainsSub(LPSTR toCheck, INT checkedSize, LPSTR toFind, INT foundSize, BOOL caseSensitive);
extern "C" __declspec(dllexport) DOUBLE AverageValue(LPINT numArr, INT arrSize);

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

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

