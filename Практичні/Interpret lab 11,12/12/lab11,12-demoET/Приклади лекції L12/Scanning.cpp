/*  -------------   Ч.В.В.  Системне програмування  --------------
                      Приклад застосування сканера
    ------------------------------------------------------------------- */

#include "CrtTk.h"

using namespace std;

// прототипи допоміжних функцій для тестування
string ConvertR1C1toA1(const Ttoken & tok);
void Print1Token(const Ttoken & tok);
void PrintTokenList();
void DiagnosticScanning(int err, string & tok);

TScanner Scanner; // екземпляр сканера

// головна функція
int main()
{
	string errtok; // на випадок помилок у лексемах
	char ex1[] = "=b23 +  A3 - Sum(C2:c15) / 2.6e1"; // приклад формули
	cout << "Formula orig:  " << ex1 << endl; // для контролю
	Scanner.CopyForm(ex1);  // спочатку формулу копіювати
	cout << "Formula toupper:  " << Scanner.ReadForm() << endl; // перевірка приведення
	int retcode = Scanner.Scanning(errtok); // сканувати формулу
	DiagnosticScanning(retcode,errtok); // надрукувати діагностику
		
	if(!retcode)	PrintTokenList();  // надрукувати для контролю список лексем	
		
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
	switch(tok.token) // дешифруємо
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
		} // switch(tok.token)
	cout << ' '; // лексеми розділяємо пропуском
} // void Print1Token(const Ttoken & tok)

void PrintTokenList()
{ // декодування лексем і друкування дешифрованого списку лексем
	cout << "toklist: count tokens " << Scanner.GetNumberToken() << endl;
	int k;
	Ttoken leks;
	for(k=0; k<Scanner.GetNumberToken(); k++)
	{
		leks=Scanner.GetToken(k); // читаємо чергову лексему
		Print1Token(leks); // і друкуємо
	} // for
	cout << endl;
} // void PrintTokenList()

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