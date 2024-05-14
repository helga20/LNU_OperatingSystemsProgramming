/*  -------------   �.�.�.  �������� �������������  --------------
                      ������� ������������ �������
    ------------------------------------------------------------------- */

#include "CrtTk.h"

using namespace std;

// ��������� ��������� ������� ��� ����������
string ConvertR1C1toA1(const Ttoken & tok);
void Print1Token(const Ttoken & tok);
void PrintTokenList();
void DiagnosticScanning(int err, string & tok);

TScanner Scanner; // ��������� �������

// ������� �������
int main()
{
	string errtok; // �� ������� ������� � ��������
	char ex1[] = "=b23 +  A3 - Sum(C2:c15) / 2.6e1"; // ������� �������
	cout << "Formula orig:  " << ex1 << endl; // ��� ��������
	Scanner.CopyForm(ex1);  // �������� ������� ��������
	cout << "Formula toupper:  " << Scanner.ReadForm() << endl; // �������� ����������
	int retcode = Scanner.Scanning(errtok); // ��������� �������
	DiagnosticScanning(retcode,errtok); // ����������� ����������
		
	if(!retcode)	PrintTokenList();  // ����������� ��� �������� ������ ������	
		
	// ��������� ���� ������
  cin.ignore(80,'\n');
  return 0;
} // main()

string ConvertR1C1toA1(const Ttoken & tok) // ��� ����������
{ // �������� ������������ ������ ������� ������ (col) � ������ A1
	string conv;  string c1,c2;
	if(tok.index.col<=26) conv = (char)('A'-1+tok.index.col);
	else // ������ �� �����
	{
    c1 = (char)(tok.index.col / 26 - 1 + 'A');
		c2 = (char)(tok.index.col % 26 - 1 + 'A');
		conv = c1+c2;
	}	
  return conv;
} // string ConvertR1C1toA1(const Ttoken & tok)

void Print1Token(const Ttoken & tok)
{ // ���������� � ��������� ���� ������� tok
	switch(tok.token) // ���������
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
	cout << ' '; // ������� ��������� ���������
} // void Print1Token(const Ttoken & tok)

void PrintTokenList()
{ // ����������� ������ � ���������� ������������� ������ ������
	cout << "toklist: count tokens " << Scanner.GetNumberToken() << endl;
	int k;
	Ttoken leks;
	for(k=0; k<Scanner.GetNumberToken(); k++)
	{
		leks=Scanner.GetToken(k); // ������ ������� �������
		Print1Token(leks); // � �������
	} // for
	cout << endl;
} // void PrintTokenList()

void DiagnosticScanning(int err, string & tok) // ���������� ����������
{ // err - ��� �������, tok - ������� � ��������
	if(!err) cout << "Scanning succeeded\n"; // ��� �������
	else // ��������� ����������� ��� �������
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