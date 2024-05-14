# Інтерпретація формул загальних алгебраїчних правил
""" --------- синтаксичне визначення формули ------------
arith_expr ::=  term  ( ( "+" | "-" )  term ) *
term ::=  factor  ( ( "*" | "/" )  factor ) *
factor ::=   number  |  "(" arith_expr ")"
number  ::=  cipher  cipher *
cipher  ::=  "0" | "1" | "2" | "3" | "4" | "5" | "6" | "7" | "8" | "9"
------------------------------------------------------ """

# допоміжний клас для збудження винятків
class errorexpr(Exception): pass  # можливі помилки

class ArithexprInterpret:

  # common data 1
  empty = 0
  number =1
  openbracket = 3
  closebracket = 4
  add = 5
  subtract = 6
  multiply = 7
  divide = 8

  # common data 2
  errscan = 'Недопустима літера в тексті формули:\n'
  errcalc = 'Помилка обчислення виразу:\n'

  def __init__(self,text): # конструктор
    self.text=text  # копія тексту формули
    self.leks = [] # список лексем
    self.i = 0 # поточна позиція сканування літери
    self.k = 0 # поточна позиція лексеми при обчисленні

  def calc(self): # виконати повну процедуру обчислення
    self.delblank()  # викреслити пропуски
    print(self.text)
    if not self.scanner():  # перший перегляд - сканування формули
      return (False,self.errscan + self.text[:self.i] + "  '" +
              self.text[self.i] + "'")  # помилки сканування
    print(self.leks)
    # другий перегляд - розбір і обчислення формули
    res = None
    try:
      res = self.arithexpr()  # запуск розбору
    except : # errorexpr та інші   # були помилки
      temp = "".join(map(lambda m: str(m[1]),self.leks[:self.k]))
      return (False,self.errcalc + temp + "  '" +  \
              str(self.leks[self.k][1]) + "'")
    if self.k < len(self.leks)-1 : # не враховано останню частину виразу
      temp = "".join(map(lambda m: str(m[1]),self.leks[:self.k]))
      return (False,self.errcalc + temp + "  '" +  \
              str(self.leks[self.k][1]) + "'")
    return (True,res)


  def delblank(self): # викреслити пропуски - незначущі літери
    self.text = self.text.replace(' ','')

  def scanner(self): # сканувати формулу і поділити на лексеми
    while self.i < len(self.text) :
      if self.text[self.i] == '(' : self.leks.append( (self.openbracket,'(') )
      elif self.text[self.i] == ')' : self.leks.append( (self.closebracket,')') )
      elif self.text[self.i] == '+' : self.leks.append( (self.add,'+') )
      elif self.text[self.i] == '-' : self.leks.append( (self.subtract,'-') )
      elif self.text[self.i] == '*' : self.leks.append( (self.multiply,'*') )
      elif self.text[self.i] == '/' : self.leks.append( (self.divide,'/') )
      elif self.text[self.i].isdigit(): self.onenumber(); self.i -=1
      else: return False # недопустима літера
      self.i += 1
    self.leks.append( (self.empty,'#') ) # обмежувач списку лексем
    return True

  def onenumber(self): # читати літери числа - правило  number::= cipher  cipher *
    num = ""
    while self.i < len(self.text) and self.text[self.i].isdigit():
      num += self.text[self.i];  self.i+=1
    if len(num) > 0: self.leks.append( (self.number,int(num)) )
    else: return None

  def arithexpr(self):
    y = self.term()  # найперший доданок: правило  arith_expr ::= term
    while self.leks[self.k][0]==self.add or self.leks[self.k][0]==self.subtract:
      # наступні доданки: правило arith_expr ::= ( ( "+" | "-" )  term ) *
      opr = self.leks[self.k][0]  # запам'ятати операцію
      self.GetNextToken()  # перейти до наступної лексеми
      if opr==self.add : y = y + self.term()
      else : y = y - self.term()
    return y

  def term(self):
    z = self.factor()  # найперший множник: правило term ::= factor
    while self.leks[self.k][0]==self.multiply or self.leks[self.k][0]==self.divide:
      # наступні множники: правило  term ::=  ( ( "*" | "/" )  factor ) *
      opr = self.leks[self.k][0]  # запам'ятати операцію
      self.GetNextToken()  # перейти до наступної лексеми
      if opr == self.multiply : z = z * self.factor()
      else : z = z / self.factor()
    return z

  def factor(self):
    if self.leks[self.k][0]==self.number : # правило  factor ::= number
      self.GetNextToken()  # перейти до наступної лексеми
      return self.leks[self.k-1][1]  # повернути число попередньої лексеми
    elif  self.leks[self.k][0]==self.openbracket :
      # правило  factor ::=  "(" arith_expr ")"
      self.GetNextToken()  # перейти до наступної лексеми
      ex = self.arithexpr()  # частина виразу в дужках
      if self.leks[self.k][0]==self.closebracket :  self.GetNextToken()
      else: raise errorexpr  # ? немає закриваючої дужки
      return ex
    else : return None

  def GetNextToken(self):  # перейти до наступної лексеми
    if self.k < len(self.leks)-1: self.k+=1
    else: raise errorexpr  # ? неможливо продовжити аналіз


if __name__ == "__main__" :
  formula = "49 - 108 / (6 + 11) * (100 - 94)"
  res = ArithexprInterpret(formula).calc()
  if res[0]:  print(res[1])
  else:
    print("Error :", res[1])
