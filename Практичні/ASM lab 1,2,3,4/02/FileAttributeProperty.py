import glob
import os
from datetime import datetime
from tabulate import tabulate

# Відкриття файлу для запису результатів
with open("results.txt", "w") as f:

    found_dat_files = glob.glob('**/*.dat', recursive=True)
    current_path = os.getcwd()

    # Виведення списку .dat файлів
    print("**** 1 ****\n")
    f.write("**** 1 ****\n")
    if found_dat_files:
        print("Знайдено наступні .dat файли:")
        for dat_file in found_dat_files:
            print(dat_file)
            f.write(dat_file + "\n")
    else:
        print("Немає .dat файлів у поточній або вкладеній папці 'problem'.")

    # Виведення кількості знайдених .dat файлів
    print(f"Всього знайдено {len(found_dat_files)} .dat файлів.")
    f.write(f"Всього знайдено {len(found_dat_files)} .dat файлів.\n")


    # Виведення абсолютних шляхів до .dat файлів
    print("\n**** 2 ****\n")
    f.write("\n**** 2 ****\n")
    if found_dat_files:
        print("Повний шлях від кореня логічного диску до .dat файлів:")
        for dat_file in found_dat_files:
            absolute_path = os.path.abspath(dat_file)
            print(absolute_path)
            f.write(absolute_path + "\n")
    else:
        print("Немає .dat файлів у поточній або вкладеній папці 'problem'.")


    # Функція для отримання інформації про .dat файл
    def get_file_info(file_path):
        file_attributes = os.stat(file_path)
        file_name = os.path.basename(file_path)
        file_size = file_attributes.st_size
        file_creation_time = os.path.getctime(file_path)
        file_modification_time = os.path.getmtime(file_path)
        file_access_time = os.path.getatime(file_path)

        creation_time = datetime.fromtimestamp(file_creation_time).strftime('%Y-%m-%d %H:%M:%S')
        modification_time = datetime.fromtimestamp(file_modification_time).strftime('%Y-%m-%d %H:%M:%S')
        access_time = datetime.fromtimestamp(file_access_time).strftime('%Y-%m-%d %H:%M:%S')

        return [file_name, file_size, creation_time, modification_time, access_time]

    # Функція для форматування даних в таблицю
    def format_table(header, data):
        table = tabulate(data, headers=header, tablefmt="grid")
        return table

    # Отримання інформації про файли та форматування її в таблицю
    print("\n**** 3 ****\n")
    header = ["Назва файлу", "Розмір", "Дата створення", "Дата зміни", "Дата відкриття"]
    data = [get_file_info(dat_file) for dat_file in found_dat_files]
    table_result = format_table(header, data)

    # Виведення таблиці у консоль
    print(table_result)

    # Запис таблиці у файл
    f.write("\n**** 3 ****\n")
    f.write(table_result)


    # Виведення списку файлів та читання вмісту обраного файлу
    print('\n**** 4 ****')
    f.write('\n**** 4 ****\n')
    if found_dat_files:
        print("Доступні файли:")
        f.write("\nДоступні файли:\n")
        for i, dat_file in enumerate(found_dat_files, start=1):
            print(f"{i}. {dat_file}")
            f.write(f"{i}. {dat_file}\n")

        file_index = input("Введіть номер файлу для перегляду: ")
        try:
            selected_file = found_dat_files[int(file_index) - 1]
            with open(selected_file, "r", encoding="utf-8") as file:
                text = file.read()
                print(text)
                f.write(text + "\n")
        except (ValueError, IndexError):
            print("Некоректний вибір файла.")
            f.write("Некоректний вибір файла.\n")
    else:
        print("Немає файлів для відображення.")
        f.write("Немає файлів для відображення.\n")


    # Розрахунок суми чисел у файлі, з можливістю корекції некоректних записів
    print('\n**** 5 ****')
    f.write("\n**** 5 ****\n")
    filename = input("Введіть назву файлу для підрахунку суми чисел: ")
    try:
        with open(filename, "r", encoding="utf-8") as file:
            text = file.read()
            numbers = [float(num) if num.replace(".", "", 1).isdigit() else 0.0 for num in text.split()]
            total = sum(numbers)
            if 0.0 in numbers:
                print("Знайдені неправильні записи, виправлено на '0.0'")
                f.write("Знайдені неправильні записи, виправлено на '0.0'\n")
                print(f"Всього виправлено {numbers.count(0.0)} неправильних записів.")
                f.write(f"Всього виправлено {numbers.count(0.0)} неправильних записів.\n")
            else:
                correction = input("Введіть значення для виправлення неправильних записів: ")
                if correction.replace(".", "", 1).isdigit():
                    correction_value = float(correction)
                    numbers = [correction_value if num == 0.0 else num for num in numbers]
                    total = sum(numbers)
                    print(f"Знайдені неправильні записи, виправлено на '{correction}'")
                    f.write(f"Знайдені неправильні записи, виправлено на '{correction}'\n")
                    print(f"Всього виправлено {numbers.count(0.0)} неправильних записів.")
                    f.write(f"Всього виправлено {numbers.count(0.0)} неправильних записів.\n")
                else:
                    print("Виправлення не здійснено через неправильне значення.")
                    f.write("Виправлення не здійснено через неправильне значення.\n")
            print("Загальна сума чисел:", total)
            f.write("Загальна сума чисел: " + str(total) + "\n")
    except FileNotFoundError:
        print(f"Файл '{filename}' не знайдено.")
        f.write(f"Файл '{filename}' не знайдено.\n")
        
# Чек на введення користувача перед завершенням програми
input("Натисніть Enter для завершення програми...")