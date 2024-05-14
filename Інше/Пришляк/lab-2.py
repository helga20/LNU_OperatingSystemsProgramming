import glob
import os


found_dat_files = glob.glob('**/*.dat', recursive=True)

print('===== 1 =====')
for dat_file in found_dat_files:
    print(dat_file)


print('===== 2 =====')
for dat_file in found_dat_files:
    print(os.path.abspath(dat_file))


print('===== 3 =====')
for dat_file in found_dat_files:
    full_path = os.path.abspath(dat_file)
    size = os.path.getsize(dat_file)
    ctime = os.path.getctime(dat_file)
    
    print(f"Файл: {full_path}")
    print(f"Розмір: {size} байт")
    print(f"Створений: {ctime}")


print('===== 4 =====')
filename = input("Введіть назву файлу для перегляду: ")
try:
    with open(filename, "r", encoding="utf-8") as f:
        text = f.read()
        print(text)
except FileNotFoundError:
    print(f"Файл '{filename}' не знайдено.")


print('===== 5 =====')
filename = input("Введіть назву файлу для підрахунку суми чисел: ")
try:
    with open(filename, "r", encoding="utf-8") as f:
        text = f.read()
        numbers = [float(num) if num.replace(".", "", 1).isdigit() else 0.0 for num in text.split()]
        total = sum(numbers)
        if 0.0 in numbers:
            print("Знайдені неправильні записи, виправлено на '0.0'")
        else:
            correction = input("Введіть значення для виправлення неправильних записів: ")
            if correction.replace(".", "", 1).isdigit():
                numbers = [float(correction) if num == 0.0 else num for num in numbers]
                total = sum(numbers)
                print(f"Знайдені неправильні записи, виправлено на '{correction}'")
            else:
                print("Виправлення не здійснено через неправ")
except FileNotFoundError:
    print(f"Файл '{filename}' не знайдено.")