#ifndef DataStructuresForCalcET001
#define DataStructuresForCalcET001
// структури даних для обчислення формул - файл DataStruct.h
//------------------------------------------------------------
// структури для сканера
struct ETindex // індекс таблиці
{
  int row,col; // індекси рядка і стовпця електронної таблиці
}; // struct ETindex

struct Ttoken  // визначення лексеми
{
  int token;    // тип лексеми
	// уточнення змісту лексеми
	union // спільна ділянка пам'яті (накладання)
	{
    ETindex index; // індекси рядка і стовпця
		double cellsvalue; // або значення числа в комірці
		char * lit;     // або текстовий рядок (літерал)
	};
	bool operator==(int t) { return token==t; } // перевірка типу
	bool operator!=(int t) { return token!=t; }
	bool operator<=(int t) { return token<=t; }
	bool operator>=(int t) { return token>=t; }
}; // struct Ttoken

enum { maxsizelist=100 }; // максимально допустимий розмір списку лексем

enum   // список імен і кодів лексем
{ s_equal=101, s_plus=102, s_minus=103, s_mult=104, s_div=105, openbracket=106,
	closebracket=107, colon=108, fsum=201, fmax=202, fmin=203, fmyabs=204, fint=205,
	cell_et=1, number_et=2, lit1=301, lit2=302, end_list=0 }; // уважно!  без redefinition!

//const Ttoken NulTok = {0,0,0}; // фіктивна лексема
Ttoken NulTok = {0,0,0}; // фіктивна лексема

Ttoken deftok[maxsizelist];  // пам'ять для лексем для обміну між функціями
//-----------------------------------------------------------

#endif