/*  -------------   �.�.�.  �������� �������������  --------------
               ������������ ������ �� ���������� �����
    ------------------------------------------------------------------- */

#include "CrtTk.h"
#include "Convert.h"

using namespace std;

// ��������� ��������� ������� ��� ����������
string ConvertR1C1toA1(const Ttoken & tok);
void Print1Token(const Ttoken & tok);
void PrintTokenListScanner();
void PrintTokenListConv();
void DiagnosticScanning(int err, string & tok);
void DiagnosticSyntax(int err, Ttoken & w_tok, Ttoken & c_tok);

TScanner Scanner; // ��������� �������
TConvItoP ConvItoP; // ��������� �������������

// ������� �������
int main()
{
	string errtok; // �� ������� ������� � ��������
	Ttoken wrongtok, currtok;
	char ex1[] = "=b23 + A3 * Int (c8/2 +d8/3) - Sum(c2:c15) / 2.6e1"; // ������� �������
	//char ex1[] = "=b23 * (A3 + f12)"; // ������� �������
	cout << "Formula orig:  " << ex1 << endl; // ��� ��������
	Scanner.CopyForm(ex1);  // �������� ������� ��������
	int retcode = Scanner.Scanning(errtok); // ��������� �������
	DiagnosticScanning(retcode,errtok); // ����������� ����������		
	if(retcode == 0) // ��� ��������� ������� �� ����
	{ PrintTokenListScanner();  // ����������� ��� �������� ������ ������
		// �������� ������� � ���'��� ��� �����
		for(int k=0; k<Scanner.GetNumberToken(); k++)		
			deftok[k] = Scanner.GetToken(k);
		// �� ��� ����������� ��� ��������
		for(int k=0; k<Scanner.GetNumberToken(); k++)
		{ Print1Token(deftok[k]); cout << ' '; // ������� ��������� ���������
		}
		cout << endl;
	}

	int retc2;
	// ������������ � ���������� �����
	if(retcode == 0) // ��� ��������� ������� �� ����
	{
		retc2= ConvItoP.Converting(deftok,Scanner.GetNumberToken(),&wrongtok,&currtok);
		DiagnosticSyntax(retc2,wrongtok,currtok);
		if(!retc2)
		{
			PrintTokenListConv(); // ������ ������ � ���������� ����
		}
	}
		
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
	switch(tok.token) // � ���������
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
{ // ����������� � ���������� ������ ������ �������
	cout << "toklistScanning: count tokens " << Scanner.GetNumberToken() << endl;
	int k;
	Ttoken leks;
	for(k=0; k<Scanner.GetNumberToken(); k++)
	{
		leks=Scanner.GetToken(k); // ������ ������� �������
		Print1Token(leks); // � �������
		cout << ' '; // ������� ��������� ���������
	} // for
	cout << endl;
} // void PrintTokenList()

void PrintTokenListConv()
{ // ����������� � ���������� ������ ������ �������������
	cout << "toklistConv: count tokens " << ConvItoP.GetNumberToken() << endl;
	int k;
	Ttoken leks;
	for(k=0; k<ConvItoP.GetNumberToken(); k++)
	{
		leks=ConvItoP.GetToken(k); // ������ ������� �������
		Print1Token(leks); // � �������
		cout << ' '; // ������� ��������� ���������
	} // for
	cout << endl;
} // void PrintTokenListConv()

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

void DiagnosticSyntax(int err, Ttoken & w_tok, Ttoken & c_tok) // ���������� ������������� ������
{ // err - ��� �������; w_tok - �������, �� ��� ���� ����������� �������;
	// c_tok - ������� �� ������ ��������� �������
	if(!err) cout << "Converting succeeded\n"; // ��� �������
	else // ��������� ����������� ��� �������
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