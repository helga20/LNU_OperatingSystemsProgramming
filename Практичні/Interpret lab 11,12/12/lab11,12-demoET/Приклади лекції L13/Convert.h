#ifndef ConvertingFromInfixToPostfix001
#define ConvertingFromInfixToPostfix001
/*  -------------   Ч.В.В.  Системне програмування  --------------
    Клас для перетворення у постфіксну форму формул електронних таблиць
    ------------------------------------------------------------------- */

// визначення класу перетворювача - файл Convert.h

#include "DataStruct.h"  // файл структур даних
#include <string>  // використаємо стандартний клас string
#include <iostream>
using namespace std;


class TConvItoP  // клас перетворювача
{
  protected:
		Ttoken * toklist; // вказівник на масив лексем в інфіксній формі
		int size;  // кількість лексем в інфіксному масиві
		int ff;       // номер поточної лексеми в інфіксному масиві
		Ttoken * token; // вказівник на поточну лексему в інфіксному масиві
		Ttoken postfix[maxsizelist];  // масив лексем в постфіксній формі
    int N;       // номер останньої лексеми в постфіксному масиві (0<=N<100)
		Ttoken * errcontext; // лексема, імовірно до якої стосується помилка
		bool error; // ознака наявності помилки при перетворенні
		int codeerror;  // номер помилки за класифікацією перетворювача
		/* визначено помилки з номерами: 2001-2008 */

		// допоміжні функції для побудови постфіксної форми
		void GetNextTok() // перейти до наступної лексеми інфіксної форми
		{
			if(ff < size-1) { ff++; token= &toklist[ff]; }
			else { token= &NulTok; } // список лексем вичерпаний
		} // void GetNextTok()

		void SaveTok(Ttoken * t) // записати лексему t в постфіксний масив
		{
			N++; postfix[N]= *t;  // копія лексеми t
		} // void SaveTok(Ttoken * t)

		bool CorrectArrang(Ttoken * t1, Ttoken * t2)
		{ // перевірка коректності визначення ряду даних (має бути t1<=t2 в одному рядку або стовпці)
			return (t1->index.col <= t2->index.col && t1->index.row == t2->index.row ||
				      t1->index.row <= t2->index.row && t1->index.col == t2->index.col);
		} // bool CorrectArrang(Ttoken * t1, Ttoken * t2)

		void Formula(); // функція для <формула>
		void Vyraz();   // функція для <вираз>
		void Dodanok(); // функція для <доданок>
		void Mnoznyk(); // функція для <множник>
		void ETFunction(); // функція для <функція>
		void ETRjad();  // функція для <ряд>

	public:
		TConvItoP() { N= -1; ff= -1; } // конструктор
	  ~TConvItoP() { cout << "Converter destroyed\n"; }   // деструктор

		int Converting(Ttoken * t, int c, Ttoken * badtoken, Ttoken * current);
		// перетворити в постфіксну форму

		int GetNumberToken() { return N+1; } // прочитати кількість лексем

		const Ttoken& GetToken(int k) // прочитати лексему номер k
		{			
      if(k>=0 && k<=N) return postfix[k];
			else return NulTok; // фіктивно
		} // const Ttoken& GetToken(int k)
}; // class TConvItoP
// ---------------------------------------------------------------------
// визначення методів класу: стартова функція для перетворення
int TConvItoP::Converting(Ttoken * t, int c, Ttoken * badtoken, Ttoken * current)
{ // перетворити в постфіксну форму
	// t - масив лексем в інфіксній формі; c - кількість лексем
	// badtoken - може означати лексему, до якої стосується помилка
	// current - лексема на момент виявлення помилки
	toklist=t;  size=c; // запам'ятати параметри
	ff = -1; // найперша лексема в інфіксному масиві
	N= -1;  // порожній масив лексем постфіксної форми
	error=false; // ознака наявності помилки
	errcontext = &NulTok; // імовірна позиція помилки

	// побудова дерева синтаксичного розбору з реагуванням на помилки
	try
	{
		// всі функції викликаються при першій лексемі, яка до них належить
		GetNextTok(); // вибрати найпершу лексему
		Formula();  // запуск дерева розбору
	}
	catch (int ne) // номер (код) виявленої помилки
	{
		error=true; codeerror=ne;
	}

	if(!error) return 0; // помилок не було
	else 
	{ // у випадку помилки передається лексема з помилкою і повертається номер помилки
		*badtoken = *errcontext; // лексема, до якої може стосуватися помилка		
		*current = *token; // лексема на момент виявлення помилки
	  return codeerror; // номер знайденої помилки
	}
} // int TConvItoP::Converting(Ttoken * t, int c, Ttoken & badtoken, Ttoken * current)

void TConvItoP::Formula() // функція для <формула>
{
	if(*token != s_equal)
	{ errcontext = token;
		throw 2001; // немає знака = на початку формули
	}
	GetNextTok(); // знак = не зберігаємо
	Vyraz(); // перехід до аналізу виразу
	// чи всі лексеми були проаналізовані?
	if(*token != end_list)
	{ errcontext=token; // позиція помилки
		throw 2002; // відсутній або неправильний знак операції
	}
} // void TConvItoP::Formula()

void TConvItoP::Vyraz() // функція для <вираз>
{
	Ttoken * opr; // вказівник на лексему зі знаком
	Dodanok();  // найперший доданок виразу	
	while(*token == s_plus || *token == s_minus)
	{
		opr = token; // запам'ятати лексему зі знаком
		GetNextTok(); // вибрати наступну лексему після знака
		Dodanok();  // наступний доданок виразу
		SaveTok(opr); // записати в постфіксну форму знак
	}
} // void TConvItoP::Vyraz()

void TConvItoP::Dodanok() // функція для <доданок>
{
	Ttoken * opr; // вказівник на лексему зі знаком
	Mnoznyk();  // найперший множник доданка	
	while(*token == s_mult || *token == s_div)
	{
		opr = token; // запам'ятати лексему зі знаком
		GetNextTok(); // вибрати наступну лексему після знака
		Mnoznyk();  // наступний множник доданка
		SaveTok(opr); // записати в постфіксну форму знак
	}
} // void TConvItoP::Dodanok()

void TConvItoP::Mnoznyk() // функція для <множник>
{
	if(*token == number_et || *token == cell_et) // це число або комірка
	{ SaveTok(token); // записати в постфіксну форму число або комірку
		GetNextTok(); // вибрати наступну лексему після числа або комірки
	}
	else if
	(*token >= fsum && *token <= fint) // це функція
	{ ETFunction(); // перейти до аналізу функції
	}
	else if
	(*token == openbracket) // це вираз в дужках
	{
		GetNextTok(); // наступна лексема після відкриваючої дужки
		Vyraz(); // аналіз виразу в дужках
		if(*token!=closebracket)
		{ errcontext = token;
			throw 2003; // немає закриваючої дужки для виразу
		}
		GetNextTok(); // наступна лексема після закриваючої дужки
	}
	else
	{ errcontext = token;
		throw 2004; // неправильний запис елемента виразу
	}
} // void TConvItoP::Mnoznyk()

void TConvItoP::ETFunction() // функція для <функція>
{
	Ttoken * opr; // вказівник на лексему з іменем функції
	errcontext = token;
	if(*token >= fsum && *token <= fmin) // це функція ряду
	{
		opr = token; // запам'ятати лексему з іменем функції
		GetNextTok(); // наступна лексема після імені функції
		if(*token != openbracket) throw 2005; // аргументи функції мають бути в дужках
		GetNextTok(); // наступна лексема після відкриваючої дужки
		ETRjad(); // аналіз ряду
		if(*token != closebracket) throw 2005; // аргументи функції мають бути в дужках
		SaveTok(opr); // записати в постфіксну форму ім'я функції
		GetNextTok(); // наступна лексема після закриваючої дужки
	}
	else if(*token >= fmyabs && *token <= fint) // це функція одного параметра
	{
		opr = token; // запам'ятати лексему з іменем функції
		GetNextTok(); // наступна лексема після імені функції
		if(*token != openbracket) throw 2005; // аргументи функції мають бути в дужках
		GetNextTok(); // наступна лексема після відкриваючої дужки
		Vyraz(); // перехід до аналізу виразу
		if(*token != closebracket) throw 2005; // аргументи функції мають бути в дужках
		SaveTok(opr); // записати в постфіксну форму ім'я функції
		GetNextTok(); // наступна лексема після закриваючої дужки
	}
	else throw 2006; // неправильне ім'я функції
} // void TConvItoP::ETFunction()

void TConvItoP::ETRjad() // функція для <ряд>
{
	Ttoken * opr; // вказівник на лексему з двокрапкою
	Ttoken *ETbeg, *ETend;
	if(*token != cell_et) throw 2007; // аргументом функції має бути ряд
	SaveTok(token); // записати в постфіксну форму перший аргумент функції
	ETbeg = token; // перший аргумент
	GetNextTok(); // наступна лексема після початку ряду
	if(*token != colon) throw 2007; // аргументом функції має бути ряд (немає ':')
	opr = token; // запам'ятати лексему з двокрапкою
	GetNextTok(); // наступна лексема після ':'
	if(*token != cell_et) throw 2007; // аргументом функції має бути ряд
	SaveTok(token); // записати в постфіксну форму другий аргумент функції
	ETend = token; // другий аргумент
	if(!CorrectArrang(ETbeg,ETend)) throw 2008; // неправильно визначений ряд для функції
	SaveTok(opr); // записати в постфіксну форму двокрапку
	GetNextTok(); // наступна лексема після ряду
} // void TConvItoP::ETRjad()

#endif