# Інтерпретація формул загальних алгебраїчних правил
""" --------- синтаксичне визначення формули ------------
arith_expr ::=  term  ( ( "+" | "-" )  term ) *
term ::=  factor  ( ( "*" | "/" )  factor ) *
factor ::=   number  |  "(" arith_expr ")"
number  ::=  cipher  cipher *
cipher  ::=  "0" | "1" | "2" | "3" | "4" | "5" | "6" | "7" | "8" | "9"
------------------------------------------------------ """

# допоміжний клас для збудження винятків
import math

class errorexpr(Exception):
    pass  # можливі помилки

class ArithexprInterpret:
    # common data 1
    empty = 0
    number = 1
    openbracket = 3
    closebracket = 4
    add = 5
    subtract = 6
    multiply = 7
    divide = 8
    # Added new operators
    divideWhole = 9
    remainder = 10
    sin = 11
    pow = 12

    # common data 2
    errscan = 'Недопустима літера в тексті формули:\n'
    errcalc = 'Помилка обчислення виразу:\n'

    def __init__(self, text):  # конструктор
        self.text = text  # копія тексту формули
        self.leks = []  # список лексем
        self.i = 0  # поточна позиція сканування літери
        self.k = 0  # поточна позиція лексеми при обчисленні

    def calc(self):  # виконати повну процедуру обчислення
        self.delblank()  # викреслити пропуски
        print(self.text)
        if not self.scanner():  # перший перегляд - сканування формули
            return (False, self.errscan + self.text[:self.i] + "  '" +
                    self.text[self.i] + "'")  # помилки сканування
        # print(self.leks)
        # другий перегляд - розбір і обчислення формули
        res = None
        try:
            res = self.arithexpr()  # запуск розбору
        except:  # errorexpr та інші   # були помилки
            temp = "".join(map(lambda m: str(m[1]), self.leks[:self.k]))
            return (False, self.errcalc + temp + "  '" +
                    str(self.leks[self.k][1]) + "'")
        if self.k < len(self.leks) - 1:  # не враховано останню частину виразу
            temp = "".join(map(lambda m: str(m[1]), self.leks[:self.k]))
            return (False, self.errcalc + temp + "  '" +
                    str(self.leks[self.k][1]) + "'")
        return True, res

    def delblank(self):  # викреслити пропуски - незначущі літери
        self.text = self.text.replace(' ', '')

    def scanner(self):  # сканувати формулу і поділити на лексеми
        while self.i < len(self.text):
            if self.text[self.i] == '(':
                self.leks.append((self.openbracket, '('))
            elif self.text[self.i] == ')':
                self.leks.append((self.closebracket, ')'))
            elif self.text[self.i] == '+':
                self.leks.append((self.add, '+'))
            elif self.text[self.i] == '-':
                self.leks.append((self.subtract, '-'))
            elif self.text[self.i] == '*':
                # Added check for raising to power
                if self.text[self.i + 1] == '*':
                    self.i += 1
                    self.leks.append((self.pow, '**'))
                else:
                    self.leks.append((self.multiply, '*'))
            elif self.text[self.i] == '/':
                # Added check for whole division
                if self.text[self.i + 1] == '/':
                    self.i += 1
                    self.leks.append((self.divideWhole, '//'))
                else:
                    self.leks.append((self.divide, '/'))
            # Added check for remainder
            elif self.text[self.i] == '%':
                self.leks.append((self.remainder, '%'))
            elif self.text[self.i].isdigit():
                self.onenumber()
                self.i -= 1
            else:
                # Detect function
                if self.funcname() is None:
                    return False  # недопустима літера
                self.i -= 1
            self.i += 1
        self.leks.append((self.empty, '#'))  # обмежувач списку лексем
        return True

    # Added method to detect if number is valid
    def isFloat(self, value):
        try:
            float(value)
            return True
        except ValueError:
            return False

    def onenumber(self):  # читати літери числа - правило  number::= cipher  cipher *
        num = ""
        while self.i < len(self.text):
            num += self.text[self.i]

            # Additional checks for scientific notation
            if self.text[self.i] == 'E':
                num += self.text[self.i + 1]
                self.i += 1

                if self.text[self.i] == '+' or self.text[self.i] == '-':
                    self.i += 1
                    num += self.text[self.i]

            self.i += 1

            if not self.isFloat(num):
                num = num[:-1]
                self.i -= 1
                break

        if len(num) > 0:
            self.leks.append((self.number, float(num)))     # Changed into to float
        else:
            return None

    # Function to get function name from string
    def funcname(self):
        name = ''
        while self.i < len(self.text):
            name += self.text[self.i]
            self.i += 1
            if name == 'sin':
                self.leks.append((self.sin, 'sin'))
                return True
        return None

    def arithexpr(self):
        y = self.term()  # найперший доданок: правило  arith_expr ::= term
        while self.leks[self.k][0] == self.add or self.leks[self.k][0] == self.subtract:
            # наступні доданки: правило arith_expr ::= ( ( "+" | "-" )  term ) *
            opr = self.leks[self.k][0]  # запам'ятати операцію
            self.GetNextToken()  # перейти до наступної лексеми
            if opr == self.add:
                y = y + self.term()
            else:
                y = y - self.term()
        return y

    def term(self):
        z = self.factor()  # найперший множник: правило term ::= factor
        while self.leks[self.k][0] == self.multiply \
                or self.leks[self.k][0] == self.divide \
                or self.leks[self.k][0] == self.remainder \
                or self.leks[self.k][0] == self.divideWhole:
            # наступні множники: правило  term ::=  ( ( "*" | "/" )  factor ) *
            opr = self.leks[self.k][0]  # запам'ятати операцію
            self.GetNextToken()  # перейти до наступної лексеми
            # Added conditions for remainder and whole divisions
            if opr == self.multiply:
                z = z * self.factor()
            elif opr == self.divide:
                z = z / self.factor()
            elif opr == self.remainder:
                z = z % self.factor()
            else:
                z = z // self.factor()
        return z

    def factor(self):
        # Added rule factor ::= [(+ | -)]
        is_negative = False
        if self.leks[self.k][0] == self.add:
            self.GetNextToken()
        elif self.leks[self.k][0] == self.subtract:
            self.GetNextToken()
            is_negative = True
        
        if self.leks[self.k][0] == self.number:  # правило  factor ::= number
            self.GetNextToken()  # перейти до наступної лексеми
            sub_result = self.leks[self.k - 1][1]  # повернути число попередньої лексеми
        elif self.leks[self.k][0] == self.openbracket:
            # правило  factor ::=  "(" arith_expr ")"
            self.GetNextToken()  # перейти до наступної лексеми
            ex = self.arithexpr()  # частина виразу в дужках
            if self.leks[self.k][0] == self.closebracket:
                self.GetNextToken()
            else:
                raise errorexpr  # ? немає закриваючої дужки
            sub_result = ex
        # Added rule factor ::= function
        elif self.leks[self.k][0] == self.sin:
            sub_result = self.functions()
        else:
            return None

        if is_negative:
            sub_result = -sub_result

        # Added rule factor ::= [( "**" factor ) *]
        if self.leks[self.k][0] == self.pow:
            self.GetNextToken()
            return sub_result ** self.factor()
        else:
            return sub_result

    # Added function with rule function ::= "sin(" arith_expr ")"
    def functions(self):
        opr = self.leks[self.k][0]
        if opr == self.sin:
            self.GetNextToken()
            if self.leks[self.k][0] == self.openbracket:
                self.GetNextToken()
                ex = self.arithexpr()
                if self.leks[self.k][0] == self.closebracket:
                    self.GetNextToken()
                else:
                    raise errorexpr
                if opr == self.sin:
                    return math.sin(ex)
        return None

    def GetNextToken(self):  # перейти до наступної лексеми
        if self.k < len(self.leks) - 1:
            self.k += 1
        else:
            raise errorexpr  # ? неможливо продовжити аналіз


if __name__ == "__main__":
    formulas = [
        # Testing remainder
        ("17 % 4", 1),
        ("17 % 5", 2),
        ("17 % 6", 5),
        ("17 % 17", 0),
        ("17 % 18", 17),
        # Testing whole division
        ("17 // 4", 4),
        ("17 // 5", 3),
        ("17 // 6", 2),
        ("17 // 17", 1),
        ("17 // 18", 0),
        # Testing raising to power
        ("4 ** 0.5", 2),
        ("(2 + 2) ** (4 % 2)", 1),
        ("(2 + 2) ** (5 % 3)", 16),
        ("5 * 3 ** 2", 45),
        # Testing sin (we can use 3.14 as Pi because they are rounded when compared to test)
        ("sin(3.14)", 0),
        ("sin(3.14 / 6)", 0.5),
        ("sin(3.14 / 3)", 3 ** 0.5 / 2),
        # Testing unary operators + -
        ("-3 * 4", -12),
        ("3 * -4", -12),
        ("-(-3 * -4) / 2", -6),
        ("5--2", 7),
        ("3-+3", 0),
        ("3+-3", 0),
        # Testing using floats
        ("1.45 * 2", 2.9),
        ("1.33 * 3", 3.99),
        # Testing using scientific notation
        ("1E1", 10),
        ("1E2", 100),
        ("10E2", 1000),
        ("1E-3 * 1E+3", 1),
        ("1E4 * 1E3", 1E7),
    ]

    for i in range(0, len(formulas)):
        print('Expression:')
        res = ArithexprInterpret(formulas[i][0]).calc()
        if res[0]:
            print('Result:', res[1])
            if round(res[1], 2) != round(formulas[i][1], 2):
                print("Error here, result should be", formulas[i][1])
        else:
            print("Error symbol:", res[1])
