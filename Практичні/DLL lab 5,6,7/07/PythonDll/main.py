import os
import ctypes

dirName = os.path.dirname(__file__)
dllPath = os.path.join(dirName, 'MyLib.dll')

hDLL = ctypes.WinDLL(dllPath)

# Оголошення функцій з бібліотеки DLL
FindMinimum = hDLL.FindMinimum
FindMinimum.restype = ctypes.c_int
FindMinimum.argtypes = [ctypes.POINTER(ctypes.c_int), ctypes.c_int]

CalculateFactorial = hDLL.CalculateFactorial
CalculateFactorial.restype = ctypes.c_int
CalculateFactorial.argtypes = [ctypes.c_int]

ContainsSub = hDLL.ContainsSub
ContainsSub.restype = ctypes.c_bool
ContainsSub.argtypes = [ctypes.c_char_p, ctypes.c_int, ctypes.c_char_p, ctypes.c_int, ctypes.c_bool]

AverageValue = hDLL.AverageValue
AverageValue.restype = ctypes.c_double
AverageValue.argtypes = [ctypes.POINTER(ctypes.c_int), ctypes.c_int]


def find_minimum(values):
    length = len(values)
    arr = (ctypes.c_int * length)(*values)
    return FindMinimum(arr, length)

def calculate_factorial(n):
    return CalculateFactorial(n)

def contains_sub(to_check, to_find, case_sensitive=True):
    to_check_bytes = bytes(to_check, 'utf-8')
    to_find_bytes = bytes(to_find, 'utf-8')
    result = ContainsSub(to_check_bytes, len(to_check_bytes), to_find_bytes, len(to_find_bytes), case_sensitive)
    return result

def average_value(num_arr):
    arr = (ctypes.c_int * len(num_arr))(*num_arr)
    return AverageValue(arr, len(num_arr))


if __name__ == '__main__':

    minValue = find_minimum([2, 5, 8, 9])
    if minValue != 2:
        print(f'Error! Test FindMinimum function failed!')
    else:
        print('Test FindMinimum function passed successfully!')


    number = 5
    factorial = calculate_factorial(number)
    if factorial != 120:
        print(f'Error! Test CalculateFactorial function failed!')
    else:
        print('Test CalculateFactorial function passed successfully!')


    to_check = "Hello, World!"
    to_find = "world"
    case_sensitive = False
    result = contains_sub(to_check, to_find, case_sensitive)
    if result:
        print('Test ContainsSub function passed successfully!')
    else:
        print(f'Error! Test ContainsSub function failed!')


    num_arr = [1, 2, 3, 4, 5]
    expected_average = 1
    result = average_value(num_arr)
    if result == expected_average:
        print('Test AverageValue function passed successfully!')
    else:
        print(f'Error! Test AverageValue function failed!')

