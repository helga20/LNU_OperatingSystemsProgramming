#ifndef DataStructuresForCalcET001
#define DataStructuresForCalcET001
// ��������� ����� ��� ���������� ������ - ���� DataStruct.h
//------------------------------------------------------------
// ��������� ��� �������
struct ETindex // ������ �������
{
  int row,col; // ������� ����� � ������� ���������� �������
}; // struct ETindex

struct Ttoken  // ���������� �������
{
  int token;    // ��� �������
	// ��������� ����� �������
	union // ������ ������ ���'�� (����������)
	{
    ETindex index; // ������� ����� � �������
		double cellsvalue; // ��� �������� ����� � ������
		char * lit;     // ��� ��������� ����� (������)
	};	
}; // struct Ttoken

const Ttoken NulTok = {0,0,0}; // �������� �������

enum { maxsizelist=100 }; // ����������� ���������� ����� ������ ������

enum   // ������ ���� � ���� ������
{ s_equal=101, s_plus=102, s_minus=103, s_mult=104, s_div=105, openbracket=106,
	closebracket=107, colon=108, fsum=201, fmax=202, fmin=203, fmyabs=204, fint=205,
	cell_et=1, number_et=2, lit1=301, lit2=302, end_list=0 }; // ������!  ��� redefinition!
//-----------------------------------------------------------
#endif