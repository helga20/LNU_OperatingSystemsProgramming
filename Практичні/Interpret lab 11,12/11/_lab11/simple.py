# простий приклад інтерпретації
""" ------- синтаксичне визначення формули ----------------
arith_expr  ::= number  ( operator  number ) *
number  ::=  cipher  cipher *
cipher  ::=  "0" | "1" | "2" | "3" | "4" | "5" | "6" | "7" | "8" | "9"
operator  ::=   "+"  |  "-"  |  "*"  |  "/"
-------------------------------------------------------- """
# перший перегляд - сканування формули і будова списку лексем
# другий перегляд - обчислення формули
class SimpleInterpret:

  def __init__(self,text): # конструктор
    self.text=text  # копія тексту формули
    self.leks = [] # список лексем
    self.i = 0 # поточна позиція сканування літери

  def calc(self): # виконати повну процедуру обчислення
    self.delblank()  # викреслити пропуски
    if not self.scanner():  # перший перегляд - сканування формули
      return (False,self.text[self.i]) # помилки сканування
    else:  # другий перегляд - обчислення формули
      #print(self.leks)  # контроль списку лексем
      # додати на початок списку лексем знак "+", буде простіший алгоритм:
      self.leks.insert(0,"+")
      #print(self.leks)  # контроль піля insert(0,"+")
      k = 0  # поточний номер лексеми
      res = 0  # результат обчислення
      while k < len(self.leks) :  # пари [ знак,операнд ]
        oper = self.leks[k];  k+=1  # знак
        n = int(self.leks[k]);  k+=1  # числа вважаємо цілими
        res = res+n if oper=='+' else res-n if oper=='-'   \
              else res*n if oper=='*' else res/n
      return (True,res)

  def delblank(self): # викреслити пропуски - незначущі літери
    self.text = self.text.replace(' ','')

  def scanner(self): # сканувати формулу і поділити на лексеми
    n = self.onenumber() # на першій позиції - число
    if n != None: self.leks.append(n)  # правило  formula::=number 
    else: return None # помилка в найпершому числі
    while self.i < len(self.text) :  # правило  formula::=( operator  number ) *
      sign = self.onesign() # читати знак
      if sign != None: self.leks.append(sign)
      else: return None # помилка знаку операції
      n = self.onenumber() # наступна позиція - число
      if n != None: self.leks.append(n)
      else: return None # помилка в числі
    return "OK" # всі лексеми правильні

  def onenumber(self): # читати літери числа - правило  number::= cipher  cipher *
    num = ""
    while self.i < len(self.text) and self.text[self.i].isdigit():
      num += self.text[self.i];  self.i+=1
    if len(num) > 0: return num
    else: return None

  def onesign(self): # читати знак операції - правило  operator::= "+" | "-" | "*" | "/"
    if self.text[self.i] in [ '+', '-', '*', '/' ]:
      self.i+=1;  return self.text[self.i-1]
    else: return None


if __name__ == "__main__" :
  formula = "65 + 122 - 99 / 6 - 12 * 2 + 1"  # задана формула
  #formula = "65 + A * 4"
  res = SimpleInterpret(formula).calc()
  if res[0]:  print(res[1])
  else:
    print("Error symbol:", res[1])
