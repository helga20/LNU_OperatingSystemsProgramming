/*  -------------   Ч.В.В.  Системне програмування  --------------
               Перетворення формул до постфіксної форми
    ------------------------------------------------------------------- */

#include "CrtTk.h"
#include "Convert.h"

using namespace std;

// прототипи допоміжних функцій для тестування
string ConvertR1C1toA1(const Ttoken & tok);
void Print1Token(const Ttoken & tok);
void PrintTokenListScanner();
void PrintTokenListConv();
void DiagnosticScanning(int err, string & tok);
void DiagnosticSyntax(int err, Ttoken & w_tok, Ttoken & c_tok);

TScanner Scanner; // екземпляр сканера
TConvItoP ConvItoP; // екземпляр перетворювача

// головна функція
int main()
{
	string errtok; // на випадок помилок у лексемах
	Ttoken wrongtok, currtok;
	char ex1[] = "=b23 + A3 * Int (c8/2 +d8/3) - Sum(c2:c15) / 2.6e1"; // приклад формули
	//char ex1[] = "=b23 * (A3 + f12)"; // приклад формули
	cout << "Formula orig:  " << ex1 << endl; // для контролю
	Scanner.CopyForm(ex1);  // спочатку формулу копіювати
	int retcode = Scanner.Scanning(errtok); // сканувати формулу
	DiagnosticScanning(retcode,errtok); // надрукувати діагностику		
	if(retcode == 0) // при скануванні помилок не було
	{ PrintTokenListScanner();  // надрукувати для контролю список лексем
		// копіювати лексеми в пам'ять для обміну
		for(int k=0; k<Scanner.GetNumberToken(); k++)		
			deftok[k] = Scanner.GetToken(k);
		// ще раз надрукувати для контролю
		for(int k=0; k<Scanner.GetNumberToken(); k++)
		{ Print1Token(deftok[k]); cout << ' '; // лексеми розділяємо пропуском
		}
		cout << endl;
	}

	int retc2;
	// перетворення в постфіксну форму
	if(retcode == 0) // при скануванні помилок не було
	{
		retc2= ConvItoP.Converting(deftok,Scanner.GetNumberToken(),&wrongtok,&currtok);
		DiagnosticSyntax(retc2,wrongtok,currtok);
		if(!retc2)
		{
			PrintTokenListConv(); // список лексем в постфіксній формі
		}
	}
		
	// затримати вікно консолі
  cin.ignore(80,'\n');
  return 0;
} // main()

string ConvertR1C1toA1(const Ttoken & tok) // для тестування
{ // обернене перетворення номера стовпця комірки (col) у формат A1
	string conv;  string c1,c2;
	if(tok.index.col<=26) conv = (char)('A'-1+tok.index.col);
	else // будуть дві букви
	{
    c1 = (char)(tok.index.col / 26 - 1 + 'A');
		c2 = (char)(tok.index.col % 26 - 1 + 'A');
		conv = c1+c2;
	}	
  return conv;
} // string ConvertR1C1toA1(const Ttoken & tok)

void Print1Token(const Ttoken & tok)
{ // декодувати і друкувати одну лексему tok
	switch(tok.token) // і дешифруємо
		{
		case cell_et:      cout << ConvertR1C1toA1(tok) << tok.index.row; break;
		case number_et:    cout << tok.cellsvalue; break;
		case s_equal:      cout << '='; break;
		case s_plus:       cout << '+'; break;
		case s_minus:      cout << '-'; break;
		case s_mult:       cout << '*'; break;
		case s_div:        cout << '/'; break;
		case openbracket:  cout << '('; break;
		case closebracket: cout << ')'; break;
		case colon:        cout << ':'; break;
		case fsum:         cout << "SUM"; break;
		case fmax:         cout << "MAX"; break;
		case fmin:         cout << "MIN"; break;
		case fmyabs:       cout << "ABS"; break;
		case fint:         cout << "INT"; break;
		case lit1:         cout << '\'' << tok.lit << '\''; break;
		case lit2:         cout << '\"' << tok.lit << '\"'; break;
		case 0:    cout << /*"#NULL#"*/ "in formula"; break;
		} // switch(tok.token)	
} // void Print1Token(const Ttoken & tok)

void PrintTokenListScanner()
{ // декодування і друкування списку лексем сканера
	cout << "toklistScanning: count tokens " << Scanner.GetNumberToken() << endl;
	int k;
	Ttoken leks;
	for(k=0; k<Scanner.GetNumberToken(); k++)
	{
		leks=Scanner.GetToken(k); // читаємо чергову лексему
		Print1Token(leks); // і друкуємо
		cout << ' '; // лексеми розділяємо пропуском
	} // for
	cout << endl;
} // void PrintTokenList()

void PrintTokenListConv()
{ // декодування і друкування списку лексем перетворювача
	cout << "toklistConv: count tokens " << ConvItoP.GetNumberToken() << endl;
	int k;
	Ttoken leks;
	for(k=0; k<ConvItoP.GetNumberToken(); k++)
	{
		leks=ConvItoP.GetToken(k); // читаємо чергову лексему
		Print1Token(leks); // і друкуємо
		cout << ' '; // лексеми розділяємо пропуском
	} // for
	cout << endl;
} // void PrintTokenListConv()

void DiagnosticScanning(int err, string & tok) // діагностика сканування
{ // err - код помилки, tok - лексема з помилкою
	if(!err) cout << "Scanning succeeded\n"; // без помилок
	else // друкувати повідомлення про помилку
	{
		cout << "Error S" << err << ": ";
		switch(err)
		{
		case 1001: cout << "letter \'" << tok << "\' not allow"; break;
		case 1002:; case 1003: cout << "wrong number \'" << tok << "\'"; break;
		case 1004: cout << "wrong name \'" << tok << "\' of function or cell"; break;
		case 1111: cout << "Formula too long"; break;
		default:  cout << "indefinite error"; break;
		} // switch(err)
		cout << endl;
	}
} // void DiagnosticScanning(int err, int pos)

void DiagnosticSyntax(int err, Ttoken & w_tok, Ttoken & c_tok) // діагностика синтаксичного аналізу
{ // err - код помилки; w_tok - лексема, до якої може стосуватись помилка;
	// c_tok - лексема на момент виявлення помилки
	if(!err) cout << "Converting succeeded\n"; // без помилок
	else // друкувати повідомлення про помилку
	{
		cout << "Error C" << err << ": ";
		switch(err)
		{
		case 2001: cout << "missing sign \'=\' in formula beginning"; break;
		case 2002: cout << "missing or wrong sign before \'"; Print1Token(w_tok); cout << '\''; break;
		case 2003: cout << "missing closebracket \'"; Print1Token(w_tok); cout << '\''; break;
		case 2004: cout << "incorrect element \'"; Print1Token(w_tok); cout << "\' in expression"; break;
		case 2005: cout << "arguments of function \'"; Print1Token(w_tok);
			                  cout << "\' must be in brackets: \'"; Print1Token(c_tok); cout << '\''; break;
		case 2006: cout << "incorrect functions name \'"; Print1Token(c_tok); cout << '\''; break;
		case 2007: cout << "argument of function \'"; Print1Token(w_tok);
			                  cout << "\' must be a row or a column not \'"; Print1Token(c_tok); cout << '\''; break;
		case 2008: cout << "incorrect definition of row or column for function \'"; Print1Token(w_tok);
			                  cout << "\', syntax error \'"; Print1Token(c_tok); cout << '\''; break;		
		default:  cout << "indefinite error \'"; Print1Token(c_tok); cout << '\''; break;
		} // switch(err)
		cout << endl;		
	}
} // void DiagnosticSyntax(int err, Ttoken & w_tok, Ttoken & c_tok)