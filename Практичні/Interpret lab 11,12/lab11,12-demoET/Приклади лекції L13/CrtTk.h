#ifndef MyScanerForDemoVer1
#define MyScanerForDemoVer1
/*  -------------   �.�.�.  �������� �������������  --------------
    ��������������� ������ �������� ������� ������ ����������� �������
    ------------------------------------------------------------------- */

// ���������� ����� ������� ��� ���������� ������� - ���� CrtTk.h

#include "DataStruct.h"  // ���� �������� �����
#include <string>  // ����������� ����������� ���� string
#include <iostream>
using namespace std;

class TScanner  // ���� �������
{
  protected:		
		Ttoken toklist[maxsizelist];  // ������ ������ ���� �������
    int N;       // ����� �������� ������� � ������ (0<=N<100)
    string formula;    // ����� ������ �������
		int fln; // ������� ������� � ������
    //basic_string<char> formula;  // ����� ����������� � ����� ����
    int ff;        // ����� ������� ����� ������� ��� ������
		char letter;  // ������� ����� �������
		bool isinit;   // �� ���� ��������� ����� �������
		bool isscanning;  // �� ��� ����������� ������ ������
		string tx;  // ����� ���� �������
		bool error; // ������ �������� ������� ��� ���������
		int codeerror;  // ����� ������� �� ������������� �������
		/* ��������� ������� � ��������: 1001-1004,1111 */

		// ������� ������� ��� ����������
		void Init() // ������ ����� ������� �����, ��������� � ������� �������
		{
			while(ff<fln && (formula[ff]==' ' || formula[ff]=='\t' || formula[ff]=='\n')) ff++;
			if(ff<fln) letter=formula[ff]; else letter='\0';
			tx = ""; // ���������� ����� �������
			if(letter == '\0') return; // ����� ������ �������
			if(N < maxsizelist-1) N++;  // ����� �������� �������
			else  // ������� �������� (������ ������ ���������)
			{ codeerror=1111; error=true; tx="Formula too long"; }
		} // void Init()

		void AddLetterToToken() // �������� ������� ����� �� �������
		{
      tx += letter;
		} // void AddLetterToToken()

		void GetNextChar() // ������� �� �������� ����� �������
		{
			if(ff<fln-1) { ff++; letter=formula[ff]; } else { ff=fln; letter='\0'; }
		} // void GetNextChar()

		int FunctionNameCode(string xfn) // ��������� ���� ������� xfn ������
		{
      if(xfn=="SUM") return fsum;
			else if(xfn=="MAX") return fmax;
			else if(xfn=="MIN") return fmin;
			else if(xfn=="ABS") return fmyabs;
			else if(xfn=="INT") return fint;
			else  return 0; // ���� ������� ���� � ������
		} // int FunctionNameCode(string xfn)

		int GetCodeTok1L(char t) // ��������� ����������� �����������
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
			default:   return 0; // �� �� ���������
			}
		} // int GetCodeTok1L(char t)

		int ConvertA1toR1C1(const string & s)
		{ // ����������� ��������� ����� s ������� �� ��������
      int num=0;
			string::size_type k;
			for(k=0; k<s.length(); k++) num = num*26 + (s[k]-'A'+1);
			return num;
		} // int ConvertA1toR1C1(const string & s)

  public:
		TScanner() { N= -1; ff=0; fln=0; isinit=false; isscanning=false; } // �����������
	  ~TScanner() { cout << "Scanner destroyed\n"; }   // ����������

	  void CopyForm(char * f)
    { // �������� ������� f � formula
      formula = f;
			fln = formula.length(); // �����'����� ������� ���� ������� ��� �������
	    // ������� �� ��� �������� ����� �������� (��� ������ �������)
	    int i=0;
      while(i<fln) // ���� �� ����� �� ���� �������
	    {
        if(formula[i] != '"' && formula[i] != '\'') // �� �� ������
	    	  {	formula[i]=toupper(formula[i]); i++; }
	    	else  // ������� ���������� ��� ����
		    	if(formula[i] == '"') // ������ ���������� � ����� "
			    { do { i++; }
			      while(i<fln && formula[i] != '"'); // ����� ������� ��� �������
			    	if(i<fln) i++;
			    }
		    	else  // formula[i] == '\''
		    	{ do { i++; }
		    	  while(i<fln && formula[i] != '\'');
		    		if(i<fln) i++;
		    	}
	    } // while(i<fln)
			isinit=true;  // ����� ������� ����������
			isscanning=false;  // ��� �� �� ����������
			N= -1;  // ������ ������ �� ����
			error=false; // ������ �������� ������� - ����
    } // void CopyForm(char * f)

		string ReadForm()  // ��������� ��������� ������� (��� ��������)
		{
      if(isinit) return formula;
			else  return "Formula not defined";
		} // string ReadForm()

		int Scanning(string & badtoken);  // ��������� ������� � ���������� ������ ������

		int GetNumberToken() { return N+1; } // ��������� ������� ������

		const Ttoken& GetToken(int k) // ��������� ������� ����� k
		{			
      if(isscanning && k>=0 && k<=N) return toklist[k];
			else return NulTok; // ��������
		} // const Ttoken& GetToken(int k)		
}; // class TScanner
// -------------------------------------------------------------------------
// ���������� ������ �����: ������� ������� ��� ����������
int TScanner::Scanning(string & badtoken) // ���������� �������
{   // badtoken ���� �������� �������, �� ���� �������
  if(!isinit) return 0;  // ���� ����� ������� �� ��� ����������
	ff = 0; // ����� �������� ����� �������
	letter=formula[0]; // �������� ����� �������
	N= -1;  // ������� ������ ������
	error=false; // ������ �������� �������

	// ���� ���������� � ��������� ������; ����� �������� �� �������� ������ letter
	while (letter != '\0' && !error)
	{
    Init(); // ������ ����� ������� ����� �� ������� �������
		if ( error || letter == '\0') break; // ������� ������� Init() ��� ����� �������
		// �� ������ ������ ��������� �����, �� ��� �������� �������
		if(letter>='0' && letter<='9') // �� � �����
		{
			// ����� ����� �������
			while(isdigit(letter)) { AddLetterToToken(); GetNextChar(); }
			if(letter == '.') // ���� ���� ������
			{
				AddLetterToToken(); GetNextChar(); // ���� ������
				// ����� ���� ������
				if( ! isdigit(letter)) // ���� ������ ���� �����
				{
					codeerror=1002; error=true; break; // ���� ������ ���� �����
				}
				else // ��������� ����� ���� ������
					while(isdigit(letter)) { AddLetterToToken(); GetNextChar(); }
			} // if(letter == '.')
			// ��� �� ������� ���� ���� ��������� ������� �����
			if(letter=='E') // ������� ��������
			{
				AddLetterToToken(); GetNextChar(); // �������� ����� E
				if(letter=='+' || letter=='-') // ���� ���� ���� �������
				{ AddLetterToToken(); GetNextChar(); } // �������� ����
				// ��� ����� ���� ����� �������
				if( ! isdigit(letter)) // ���� ����� E ���� �����
				{
					codeerror=1003; error=true; break; // ���� ����� E ���� �����
				}
				// �������� ����� �������
				while(isdigit(letter)) { AddLetterToToken(); GetNextChar(); }
			} // ������� ��������

			// ����������� ����� � �������� ����� � double � �������� �������
			double x = atof(tx.c_str());
			toklist[N].token=number_et;  toklist[N].cellsvalue=x;
		} // �� � �����
		else
			if(letter>='A' && letter<='Z') // �� ������ ��� �������
			{
				// �������� ��������� �� �����
				while(letter>='A' && letter<='Z') { AddLetterToToken(); GetNextChar(); }
				// ��� ������ ���� ����� � ����� � ��������� �������
				while(isdigit(letter) || letter>='A' && letter<='Z')
				  { AddLetterToToken(); GetNextChar(); }
				// ���������, �� �� � ������ �������
				int fnc = FunctionNameCode(tx);
				if(fnc) { toklist[N].token=fnc; } // ������� ��'� �������
				else // ���������, �� �� ��������� ��'� ������
				{
					// �������� ������� �� ����� � �����
					unsigned int i=0;
					string tempcol="";
					for(; i<tx.length() && tx[i]>='A' && tx[i]<='Z'; i++)  tempcol+= tx[i];
					string temprow="";
					for(; i<tx.length() && isdigit(tx[i]); i++)  temprow+= tx[i];
					int numrow=atoi(temprow.c_str());
					int numcol=ConvertA1toR1C1(tempcol);
					if(numrow>=1 && numrow<=99 && numcol>=1 && numcol<=256 && i>=tx.length())
					{ // ��������� �������
						toklist[N].token=cell_et;
						toklist[N].index.col=numcol; toklist[N].index.row=numrow;
					}
					else // �������
					{
						codeerror=1004; error=true; break; // ����������� ��'� ������ ��� �������
					}
				} // �������� ����������� ���� ������
			} // �� ������ ��� �������
			else
				if(GetCodeTok1L(letter)>0) // �� ����������� ���������
				{
					toklist[N].token=GetCodeTok1L(letter);
					GetNextChar(); // �� �������� ����� �������
				} // �� ����������� ���������
				else
					if(letter=='\'') // �� ������ � ���������� �����������
					{ // ������ �� ���� �������
						GetNextChar(); // �������� ����� ���� ���������
		    	  while(letter != '\'' && letter != '\0')
						  { AddLetterToToken(); GetNextChar(); }
						toklist[N].token=lit1;
						toklist[N].lit= new char[tx.length()+1]; // ������ ���'���						
						strcpy_s(toklist[N].lit,tx.length()+1,tx.c_str()); // �������� ������
						GetNextChar(); // ������� �� �������� ����� �� ����������
					} // �� ������ � ���������� �����������
					else
						if(letter=='\"') // ������ � ��������� �����������
						{ // ������ �� ���� �������
							GetNextChar(); // �������� ����� ���� ���������
		    			while(letter != '\"' && letter != '\0')
								{ AddLetterToToken(); GetNextChar(); }
							toklist[N].token=lit2;
							toklist[N].lit= new char[tx.length()+1]; // ������ ���'���							
							strcpy_s(toklist[N].lit,tx.length()+1,tx.c_str()); // �������� ������
							GetNextChar(); // ������� �� �������� ����� �� ����������
						} // ������ � ��������� �����������
						else // ������� - ����������� �������
						{
							AddLetterToToken(); // �����'����� ����������� ����� � ������
							codeerror=1001; // ����������� ������� (����������� �����)
							error=true;
						} // ������� - ����������� �������
	} // while (letter != '\0' && !error)

	isscanning=true;  // ������ ������ �����������
	if(!error) return 0; // ������� �� ����
	else 
	{ // � ������� ������� ��������� ������� � �������� � ����������� ����� �������
		badtoken = tx; // ������� � ��������� ��������
	  return codeerror; // ����� �������� �������
	}
} // int TScanner::Scanning(string & badtoken)

#endif