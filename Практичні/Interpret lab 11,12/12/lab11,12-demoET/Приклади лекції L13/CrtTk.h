#ifndef MyScanerForDemoVer1
#define MyScanerForDemoVer1
/*  -------------   Ч.В.В.  Системне програмування  --------------
    Демонстраційний варіант побудови сканера формул електронних таблиць
    ------------------------------------------------------------------- */

// визначення класу сканера для сканування формули - файл CrtTk.h

#include "DataStruct.h"  // файл структур даних
#include <string>  // використаємо стандартний клас string
#include <iostream>
using namespace std;

class TScanner  // клас сканера
{
  protected:		
		Ttoken toklist[maxsizelist];  // список лексем однієї формули
    int N;       // номер останньої лексеми в списку (0<=N<100)
    string formula;    // текст заданої формули
		int fln; // довжина формули в літерах
    //basic_string<char> formula;  // можна використати і такий клас
    int ff;        // номер поточної літери формули для аналізу
		char letter;  // поточна літера формули
		bool isinit;   // чи було визначено текст формули
		bool isscanning;  // чи був побудований список лексем
		string tx;  // текст однієї лексеми
		bool error; // ознака наявності помилки при скануванні
		int codeerror;  // номер помилки за класифікацією сканера
		/* визначено помилки з номерами: 1001-1004,1111 */

		// допоміжні функції для сканування
		void Init() // знайти першу значущу літеру, починаючи з поточної позиції
		{
			while(ff<fln && (formula[ff]==' ' || formula[ff]=='\t' || formula[ff]=='\n')) ff++;
			if(ff<fln) letter=formula[ff]; else letter='\0';
			tx = ""; // початковий текст лексеми
			if(letter == '\0') return; // кінець тексту формули
			if(N < maxsizelist-1) N++;  // номер наступної лексеми
			else  // формула завелика (список лексем завеликий)
			{ codeerror=1111; error=true; tx="Formula too long"; }
		} // void Init()

		void AddLetterToToken() // дописати чергову літеру до лексеми
		{
      tx += letter;
		} // void AddLetterToToken()

		void GetNextChar() // перейти до наступної літери формули
		{
			if(ff<fln-1) { ff++; letter=formula[ff]; } else { ff=fln; letter='\0'; }
		} // void GetNextChar()

		int FunctionNameCode(string xfn) // кодування імені функції xfn числом
		{
      if(xfn=="SUM") return fsum;
			else if(xfn=="MAX") return fmax;
			else if(xfn=="MIN") return fmin;
			else if(xfn=="ABS") return fmyabs;
			else if(xfn=="INT") return fint;
			else  return 0; // такої функції немає в списку
		} // int FunctionNameCode(string xfn)

		int GetCodeTok1L(char t) // кодування однолітерних розділювачів
		{
			switch(t)
			{
			case '=': return s_equal;
			case '+': return s_plus;
			case '-': return s_minus;
			case '*': return s_mult;
			case '/': return s_div;
			case '(': return openbracket;
			case ')': return closebracket;
			case ':': return colon;
			default:   return 0; // це не розділювач
			}
		} // int GetCodeTok1L(char t)

		int ConvertA1toR1C1(const string & s)
		{ // перетворити буквенний номер s стовпця на числовий
      int num=0;
			string::size_type k;
			for(k=0; k<s.length(); k++) num = num*26 + (s[k]-'A'+1);
			return num;
		} // int ConvertA1toR1C1(const string & s)

  public:
		TScanner() { N= -1; ff=0; fln=0; isinit=false; isscanning=false; } // конструктор
	  ~TScanner() { cout << "Scanner destroyed\n"; }   // деструктор

	  void CopyForm(char * f)
    { // копіювати формулу f в formula
      formula = f;
			fln = formula.length(); // запам'ятати кількість літер формули без нульової
	    // замінити всі малі латинські літери великими (для формул однакові)
	    int i=0;
      while(i<fln) // поки не дійшли до кінця формули
	    {
        if(formula[i] != '"' && formula[i] != '\'') // це не літерал
	    	  {	formula[i]=toupper(formula[i]); i++; }
	    	else  // літерали пропускаємо без зміни
		    	if(formula[i] == '"') // літерал починається з літери "
			    { do { i++; }
			      while(i<fln && formula[i] != '"'); // кінець літерала або формули
			    	if(i<fln) i++;
			    }
		    	else  // formula[i] == '\''
		    	{ do { i++; }
		    	  while(i<fln && formula[i] != '\'');
		    		if(i<fln) i++;
		    	}
	    } // while(i<fln)
			isinit=true;  // текст формули визначений
			isscanning=false;  // але ще не сканований
			N= -1;  // списку лексем ще немає
			error=false; // ознака наявності помилки - немає
    } // void CopyForm(char * f)

		string ReadForm()  // прочитати приведену формулу (для контролю)
		{
      if(isinit) return formula;
			else  return "Formula not defined";
		} // string ReadForm()

		int Scanning(string & badtoken);  // сканувати формулу і побудувати список лексем

		int GetNumberToken() { return N+1; } // прочитати кількість лексем

		const Ttoken& GetToken(int k) // прочитати лексему номер k
		{			
      if(isscanning && k>=0 && k<=N) return toklist[k];
			else return NulTok; // фіктивно
		} // const Ttoken& GetToken(int k)		
}; // class TScanner
// -------------------------------------------------------------------------
// визначення методів класу: основна функція для сканування
int TScanner::Scanning(string & badtoken) // сканування формули
{   // badtoken може означати лексему, де була помилка
  if(!isinit) return 0;  // якщо текст формули не був визначений
	ff = 0; // номер найпершої літери формули
	letter=formula[0]; // найперша літера формули
	N= -1;  // порожній список лексем
	error=false; // ознака наявності помилки

	// цикл сканування і кодування лексем; аналіз виконуємо за поточною літерою letter
	while (letter != '\0' && !error)
	{
    Init(); // знайти першу значущу літеру від поточної позиції
		if ( error || letter == '\0') break; // помилка функції Init() або кінець формули
		// за першою літерою розпізнаємо групу, до якої належить лексема
		if(letter>='0' && letter<='9') // це є число
		{
			// цифри перед крапкою
			while(isdigit(letter)) { AddLetterToToken(); GetNextChar(); }
			if(letter == '.') // може бути крапка
			{
				AddLetterToToken(); GetNextChar(); // сама крапка
				// цифри після крапки
				if( ! isdigit(letter)) // після крапки немає цифри
				{
					codeerror=1002; error=true; break; // після крапки немає цифри
				}
				else // прочитати цифри після крапки
					while(isdigit(letter)) { AddLetterToToken(); GetNextChar(); }
			} // if(letter == '.')
			// далі за текстом може бути записаний порядок числа
			if(letter=='E') // порядок присутній
			{
				AddLetterToToken(); GetNextChar(); // дописати букву E
				if(letter=='+' || letter=='-') // може бути знак порядку
				{ AddLetterToToken(); GetNextChar(); } // дописати знак
				// далі мають бути цифри порядку
				if( ! isdigit(letter)) // після букви E немає цифри
				{
					codeerror=1003; error=true; break; // після букви E немає цифри
				}
				// дописати цифри порядку
				while(isdigit(letter)) { AddLetterToToken(); GetNextChar(); }
			} // порядок присутній

			// перетворити число з текстової форми в double і записати лексему
			double x = atof(tx.c_str());
			toklist[N].token=number_et;  toklist[N].cellsvalue=x;
		} // це є число
		else
			if(letter>='A' && letter<='Z') // це комірка або функція
			{
				// спочатку прочитати всі букви
				while(letter>='A' && letter<='Z') { AddLetterToToken(); GetNextChar(); }
				// далі можуть бути цифри і букви в довільному порядку
				while(isdigit(letter) || letter>='A' && letter<='Z')
				  { AddLetterToToken(); GetNextChar(); }
				// перевіримо, чи це є іменем функції
				int fnc = FunctionNameCode(tx);
				if(fnc) { toklist[N].token=fnc; } // знайшли ім'я функції
				else // перевіримо, чи це правильне ім'я комірки
				{
					// розділити лексему на букви і цифри
					unsigned int i=0;
					string tempcol="";
					for(; i<tx.length() && tx[i]>='A' && tx[i]<='Z'; i++)  tempcol+= tx[i];
					string temprow="";
					for(; i<tx.length() && isdigit(tx[i]); i++)  temprow+= tx[i];
					int numrow=atoi(temprow.c_str());
					int numcol=ConvertA1toR1C1(tempcol);
					if(numrow>=1 && numrow<=99 && numcol>=1 && numcol<=256 && i>=tx.length())
					{ // правильна лексема
						toklist[N].token=cell_et;
						toklist[N].index.col=numcol; toklist[N].index.row=numrow;
					}
					else // помилка
					{
						codeerror=1004; error=true; break; // неправильне ім'я комірки або функції
					}
				} // перевірка правильності імені комірки
			} // це комірка або функція
			else
				if(GetCodeTok1L(letter)>0) // це однолітерний розділювач
				{
					toklist[N].token=GetCodeTok1L(letter);
					GetNextChar(); // до наступної літери формули
				} // це однолітерний розділювач
				else
					if(letter=='\'') // це літерал з одинарними апострофами
					{ // читати до кінця літерала
						GetNextChar(); // наступна літера після апострофа
		    	  while(letter != '\'' && letter != '\0')
						  { AddLetterToToken(); GetNextChar(); }
						toklist[N].token=lit1;
						toklist[N].lit= new char[tx.length()+1]; // надати пам'ять						
						strcpy_s(toklist[N].lit,tx.length()+1,tx.c_str()); // копіювати літерал
						GetNextChar(); // перейти до наступної літери за апострофом
					} // це літерал з одинарними апострофами
					else
						if(letter=='\"') // літерал з подвійними апострофами
						{ // читати до кінця літерала
							GetNextChar(); // наступна літера після апострофа
		    			while(letter != '\"' && letter != '\0')
								{ AddLetterToToken(); GetNextChar(); }
							toklist[N].token=lit2;
							toklist[N].lit= new char[tx.length()+1]; // надати пам'ять							
							strcpy_s(toklist[N].lit,tx.length()+1,tx.c_str()); // копіювати літерал
							GetNextChar(); // перейти до наступної літери за апострофом
						} // літерал з подвійними апострофами
						else // помилка - невизначена лексема
						{
							AddLetterToToken(); // запам'ятати недопустиму літеру в лексемі
							codeerror=1001; // невизначена лексема (недопустима літера)
							error=true;
						} // помилка - невизначена лексема
	} // while (letter != '\0' && !error)

	isscanning=true;  // список лексем побудований
	if(!error) return 0; // помилок не було
	else 
	{ // у випадку помилки копіюється лексема з помилкою і повертається номер помилки
		badtoken = tx; // лексема з виявленою помилкою
	  return codeerror; // номер знайденої помилки
	}
} // int TScanner::Scanning(string & badtoken)

#endif