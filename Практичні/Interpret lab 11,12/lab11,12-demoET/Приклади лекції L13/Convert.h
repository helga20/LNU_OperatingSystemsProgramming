#ifndef ConvertingFromInfixToPostfix001
#define ConvertingFromInfixToPostfix001
/*  -------------   �.�.�.  �������� �������������  --------------
    ���� ��� ������������ � ���������� ����� ������ ����������� �������
    ------------------------------------------------------------------- */

// ���������� ����� ������������� - ���� Convert.h

#include "DataStruct.h"  // ���� �������� �����
#include <string>  // ����������� ����������� ���� string
#include <iostream>
using namespace std;


class TConvItoP  // ���� �������������
{
  protected:
		Ttoken * toklist; // �������� �� ����� ������ � �������� ����
		int size;  // ������� ������ � ���������� �����
		int ff;       // ����� ������� ������� � ���������� �����
		Ttoken * token; // �������� �� ������� ������� � ���������� �����
		Ttoken postfix[maxsizelist];  // ����� ������ � ���������� ����
    int N;       // ����� �������� ������� � ������������ ����� (0<=N<100)
		Ttoken * errcontext; // �������, ������� �� ��� ��������� �������
		bool error; // ������ �������� ������� ��� �����������
		int codeerror;  // ����� ������� �� ������������� �������������
		/* ��������� ������� � ��������: 2001-2008 */

		// ������� ������� ��� �������� ���������� �����
		void GetNextTok() // ������� �� �������� ������� �������� �����
		{
			if(ff < size-1) { ff++; token= &toklist[ff]; }
			else { token= &NulTok; } // ������ ������ ����������
		} // void GetNextTok()

		void SaveTok(Ttoken * t) // �������� ������� t � ����������� �����
		{
			N++; postfix[N]= *t;  // ���� ������� t
		} // void SaveTok(Ttoken * t)

		bool CorrectArrang(Ttoken * t1, Ttoken * t2)
		{ // �������� ���������� ���������� ���� ����� (�� ���� t1<=t2 � ������ ����� ��� �������)
			return (t1->index.col <= t2->index.col && t1->index.row == t2->index.row ||
				      t1->index.row <= t2->index.row && t1->index.col == t2->index.col);
		} // bool CorrectArrang(Ttoken * t1, Ttoken * t2)

		void Formula(); // ������� ��� <�������>
		void Vyraz();   // ������� ��� <�����>
		void Dodanok(); // ������� ��� <�������>
		void Mnoznyk(); // ������� ��� <�������>
		void ETFunction(); // ������� ��� <�������>
		void ETRjad();  // ������� ��� <���>

	public:
		TConvItoP() { N= -1; ff= -1; } // �����������
	  ~TConvItoP() { cout << "Converter destroyed\n"; }   // ����������

		int Converting(Ttoken * t, int c, Ttoken * badtoken, Ttoken * current);
		// ����������� � ���������� �����

		int GetNumberToken() { return N+1; } // ��������� ������� ������

		const Ttoken& GetToken(int k) // ��������� ������� ����� k
		{			
      if(k>=0 && k<=N) return postfix[k];
			else return NulTok; // ��������
		} // const Ttoken& GetToken(int k)
}; // class TConvItoP
// ---------------------------------------------------------------------
// ���������� ������ �����: �������� ������� ��� ������������
int TConvItoP::Converting(Ttoken * t, int c, Ttoken * badtoken, Ttoken * current)
{ // ����������� � ���������� �����
	// t - ����� ������ � �������� ����; c - ������� ������
	// badtoken - ���� �������� �������, �� ��� ��������� �������
	// current - ������� �� ������ ��������� �������
	toklist=t;  size=c; // �����'����� ���������
	ff = -1; // �������� ������� � ���������� �����
	N= -1;  // ������� ����� ������ ���������� �����
	error=false; // ������ �������� �������
	errcontext = &NulTok; // ������� ������� �������

	// �������� ������ ������������� ������� � ����������� �� �������
	try
	{
		// �� ������� ������������ ��� ������ ������, ��� �� ��� ��������
		GetNextTok(); // ������� �������� �������
		Formula();  // ������ ������ �������
	}
	catch (int ne) // ����� (���) �������� �������
	{
		error=true; codeerror=ne;
	}

	if(!error) return 0; // ������� �� ����
	else 
	{ // � ������� ������� ���������� ������� � �������� � ����������� ����� �������
		*badtoken = *errcontext; // �������, �� ��� ���� ����������� �������		
		*current = *token; // ������� �� ������ ��������� �������
	  return codeerror; // ����� �������� �������
	}
} // int TConvItoP::Converting(Ttoken * t, int c, Ttoken & badtoken, Ttoken * current)

void TConvItoP::Formula() // ������� ��� <�������>
{
	if(*token != s_equal)
	{ errcontext = token;
		throw 2001; // ���� ����� = �� ������� �������
	}
	GetNextTok(); // ���� = �� ��������
	Vyraz(); // ������� �� ������ ������
	// �� �� ������� ���� ������������?
	if(*token != end_list)
	{ errcontext=token; // ������� �������
		throw 2002; // ������� ��� ������������ ���� ��������
	}
} // void TConvItoP::Formula()

void TConvItoP::Vyraz() // ������� ��� <�����>
{
	Ttoken * opr; // �������� �� ������� � ������
	Dodanok();  // ��������� ������� ������	
	while(*token == s_plus || *token == s_minus)
	{
		opr = token; // �����'����� ������� � ������
		GetNextTok(); // ������� �������� ������� ���� �����
		Dodanok();  // ��������� ������� ������
		SaveTok(opr); // �������� � ���������� ����� ����
	}
} // void TConvItoP::Vyraz()

void TConvItoP::Dodanok() // ������� ��� <�������>
{
	Ttoken * opr; // �������� �� ������� � ������
	Mnoznyk();  // ��������� ������� �������	
	while(*token == s_mult || *token == s_div)
	{
		opr = token; // �����'����� ������� � ������
		GetNextTok(); // ������� �������� ������� ���� �����
		Mnoznyk();  // ��������� ������� �������
		SaveTok(opr); // �������� � ���������� ����� ����
	}
} // void TConvItoP::Dodanok()

void TConvItoP::Mnoznyk() // ������� ��� <�������>
{
	if(*token == number_et || *token == cell_et) // �� ����� ��� ������
	{ SaveTok(token); // �������� � ���������� ����� ����� ��� ������
		GetNextTok(); // ������� �������� ������� ���� ����� ��� ������
	}
	else if
	(*token >= fsum && *token <= fint) // �� �������
	{ ETFunction(); // ������� �� ������ �������
	}
	else if
	(*token == openbracket) // �� ����� � ������
	{
		GetNextTok(); // �������� ������� ���� ���������� �����
		Vyraz(); // ����� ������ � ������
		if(*token!=closebracket)
		{ errcontext = token;
			throw 2003; // ���� ���������� ����� ��� ������
		}
		GetNextTok(); // �������� ������� ���� ���������� �����
	}
	else
	{ errcontext = token;
		throw 2004; // ������������ ����� �������� ������
	}
} // void TConvItoP::Mnoznyk()

void TConvItoP::ETFunction() // ������� ��� <�������>
{
	Ttoken * opr; // �������� �� ������� � ������ �������
	errcontext = token;
	if(*token >= fsum && *token <= fmin) // �� ������� ����
	{
		opr = token; // �����'����� ������� � ������ �������
		GetNextTok(); // �������� ������� ���� ���� �������
		if(*token != openbracket) throw 2005; // ��������� ������� ����� ���� � ������
		GetNextTok(); // �������� ������� ���� ���������� �����
		ETRjad(); // ����� ����
		if(*token != closebracket) throw 2005; // ��������� ������� ����� ���� � ������
		SaveTok(opr); // �������� � ���������� ����� ��'� �������
		GetNextTok(); // �������� ������� ���� ���������� �����
	}
	else if(*token >= fmyabs && *token <= fint) // �� ������� ������ ���������
	{
		opr = token; // �����'����� ������� � ������ �������
		GetNextTok(); // �������� ������� ���� ���� �������
		if(*token != openbracket) throw 2005; // ��������� ������� ����� ���� � ������
		GetNextTok(); // �������� ������� ���� ���������� �����
		Vyraz(); // ������� �� ������ ������
		if(*token != closebracket) throw 2005; // ��������� ������� ����� ���� � ������
		SaveTok(opr); // �������� � ���������� ����� ��'� �������
		GetNextTok(); // �������� ������� ���� ���������� �����
	}
	else throw 2006; // ����������� ��'� �������
} // void TConvItoP::ETFunction()

void TConvItoP::ETRjad() // ������� ��� <���>
{
	Ttoken * opr; // �������� �� ������� � ����������
	Ttoken *ETbeg, *ETend;
	if(*token != cell_et) throw 2007; // ���������� ������� �� ���� ���
	SaveTok(token); // �������� � ���������� ����� ������ �������� �������
	ETbeg = token; // ������ ��������
	GetNextTok(); // �������� ������� ���� ������� ����
	if(*token != colon) throw 2007; // ���������� ������� �� ���� ��� (���� ':')
	opr = token; // �����'����� ������� � ����������
	GetNextTok(); // �������� ������� ���� ':'
	if(*token != cell_et) throw 2007; // ���������� ������� �� ���� ���
	SaveTok(token); // �������� � ���������� ����� ������ �������� �������
	ETend = token; // ������ ��������
	if(!CorrectArrang(ETbeg,ETend)) throw 2008; // ����������� ���������� ��� ��� �������
	SaveTok(opr); // �������� � ���������� ����� ���������
	GetNextTok(); // �������� ������� ���� ����
} // void TConvItoP::ETRjad()

#endif