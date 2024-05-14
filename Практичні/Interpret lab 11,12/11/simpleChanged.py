# простий приклад інтерпретації
""" ------- синтаксичне визначення формули ----------------
arith_expr  ::= number  ( operator  number ) *
number  ::=  cipher  cipher *
cipher  ::=  "0" | "1" | "2" | "3" | "4" | "5" | "6" | "7" | "8" | "9"
operator  ::=   "+"  |  "-"  |  "*"  |  "/"
-------------------------------------------------------- """

# перший перегляд - сканування формули і будова списку лексем
# другий перегляд - обчислення формули
import math

class SimpleInterpret:

    def __init__(self, text):  # конструктор
        # Додала оператори як поля класу
        self.unary = ['r', 'p', 's']
        self.binary = ['+', '-', '*', '/', '%', 'm']

        self.operators = self.unary + self.binary

        self.text = text  # копія тексту формули
        self.leks = []  # список лексем
        self.i = 0  # поточна позиція сканування літери
        # Додала ці поля, щоб відслідковувати в якій системі числення робити операції
        self.is_hex = False
        self.is_bin = False

    def calc(self):  # виконати повну процедуру обчислення
        self.delblank()  # викреслити пропуски
        if not self.scanner():  # перший перегляд - сканування формули
            return (False, self.text[self.i])  # помилки сканування
        else:  # другий перегляд - обчислення формули
            # print(self.leks)  # контроль списку лексем
            # додати на початок списку лексем знак "+", буде простіший алгоритм:
            self.leks.insert(0, "+")
            # print(self.leks)  # контроль піля insert(0,"+")
            k = 0  # поточний номер лексеми
            res = 0  # результат обчислення
            while k < len(self.leks):  # пари [ знак,операнд ]
                oper = self.leks[k]
                k += 1  # знак
                if oper in self.unary: # Додала цей рядок для унарних операцій
                    res = res ** 0.5 if oper == 'r' else res * math.pi if oper == 'p' \
                        else round(math.sin(res), 4)
                else:
                    # Додала ці умови для 16 та 2 систем числень
                    if self.is_bin:
                        n = int(self.leks[k], 2)
                    elif self.is_hex:
                        n = int(self.leks[k], 16)
                    else:
                        n = int(self.leks[k])
                    k += 1  # числа вважаємо цілими
                    res = res + n if oper == '+' else res - n if oper == '-' \
                        else res * n if oper == '*' else res / n if oper == '/' \
                        else res % n if oper == '%' else min(res, n)
            return (True, res)

    # Додала нові обгортки для кожної системи обчислення
    def dec_calc(self):
        self.is_bin = False
        self.is_hex = False

        return self.calc()

    def bin_calc(self):
        self.is_bin = True
        self.is_hex = False

        result = self.calc()
        if result[0]:
            return True, bin(result[1])
        return result

    def hex_calc(self):
        self.is_bin = False
        self.is_hex = True

        result = self.calc()
        if result[0]:
            return True, hex(result[1])
        return result

    def delblank(self):  # викреслити пропуски - незначущі літери
        self.text = self.text.replace(' ', '')

    def scanner(self):  # сканувати формулу і поділити на лексеми
        n = self.onenumber()  # на першій позиції - число
        if n != None:
            self.leks.append(n)  # правило  formula::=number
        else:
            return None  # помилка в найпершому числі
        while self.i < len(self.text):  # правило  formula::=( operator  number ) *
            sign = self.onesign()  # читати знак
            if sign != None:
                self.leks.append(sign)
            else:
                return None  # помилка знаку операції
            if sign not in self.unary:   # Додала цей рядок для унарних операцій
                n = self.onenumber()  # наступна позиція - число
                if n != None:
                    self.leks.append(n)
                else:
                    return None  # помилка в числі
        return "OK"  # всі лексеми правильні

    def onenumber(self):  # читати літери числа - правило  number::= cipher  cipher *
        # Цей метод майже повністю переписаний для підтримки двійкової та шістнадцяткової систем числення
        if self.is_bin:
            num = "0b"
        elif self.is_hex:
            num = "0x"
        else:
            num = ""
        while self.i < len(self.text) and self.text[self.i] not in self.operators:  # Тут зміни
            if self.is_bin and self.text[self.i] != '0' and self.text[self.i] != '1':
                return None
            if self.is_hex and not self.text[self.i].isdigit() and \
                    self.text[self.i] != 'A' and self.text[self.i] != 'B' and self.text[self.i] != 'C' and \
                    self.text[self.i] != 'D' and self.text[self.i] != 'E' and self.text[self.i] != 'F':
                return None
            if not self.is_bin and not self.is_hex and not self.text[self.i].isdigit():
                return None
            num += self.text[self.i]
            self.i += 1
        if len(num) > 0:
            return num
        else:
            return None

    def onesign(self):  # читати знак операції - правило  operator::= "+" | "-" | "*" | "/"
        if self.text[self.i] in self.operators:  # Тут зміни
            self.i += 1
            return self.text[self.i - 1]
        else:
            return None


if __name__ == "__main__":
    # formula = "65 + 122 - 99 / 6 - 12 * 2 + 1"  # задана формула
    # formula = "65 + A * 4"
    formulas = [
        # Testing getting remainder
        ("5 % 1", 0),
        ("5 % 2", 1),
        ("17 % 3", 2),
        ("7 % 4", 1),
        ("4 % 4", 0),
        ("3 % 4", 3),
        # Testing getting min from res or given
        ("2 m 1", 1),
        ("1 - 2 m 1", -1),
        ("1 + 1 + 1 m 1", 1),
        ("3 - 3 m 1", 0),
        # Testing unary operator square root
        ("64 r", 8),
        ("16 r", 4),
        ("4 r + 2", 4),
        ("3 + 6 r * 2", 6),
        # Testing unary operator p (Pi)
        ("2 p", 2 * math.pi),
        ("3 - 10 * 2 p", -14 * math.pi),
        # Testing unary operator s (sin)
        ("0 s", 0),
        ("1 / 6 p s", 0.5),
        ("1 / 4 p s", round(1 / (2 ** 0.5), 4)),
        ("1 / 3 p s", round((3 ** 0.5) / 2, 4)),
        ("1 / 2 p s", 1),
        ("1 p s", 0),
        ("2 p s", 0),
    ]
    for i in range(0, len(formulas)):
        res = SimpleInterpret(formulas[i][0]).dec_calc()
        if res[0]:
            print(res[1])
            if res[1] != formulas[i][1]:
                print("Error here, result should be", formulas[i][1])
        else:
            print("Error symbol:", res[1])

    binary_calcs = [
        ("101 + 11", 0b1000),
        ("101 * 10", 0b1010),
        ("4 * 10", 0),    # Error here
        ("01 + 01 + 01", 0b11),
    ]

    for i in range(0, len(binary_calcs)):
        res = SimpleInterpret(binary_calcs[i][0]).bin_calc()
        if res[0]:
            print(res[1])
            if int(res[1], 2) != binary_calcs[i][1]:
                print("Error here, result should be", binary_calcs[i][1])
        else:
            print("Error symbol:", res[1])

    hex_calcs = [
        ("AB % 1", 0),  # AB = 171
        ("AB % 2", 1),
        ("AB % 3", 0),
        ("AB % 4", 3),
        ("AB % 5", 1),
        ("AB % AB", 0),
        ("AB % AC", 0xAB),
        ("AB % AC m 10 - F", 0x1),
        ("AB % AC m 10 - G", 0),    # Error here
    ]

    for i in range(0, len(hex_calcs)):
        res = SimpleInterpret(hex_calcs[i][0]).hex_calc()
        if res[0]:
            print(res[1])
            if int(res[1], 16) != hex_calcs[i][1]:
                print("Error here, result should be", hex_calcs[i][1])
        else:
            print("Error symbol:", res[1])
