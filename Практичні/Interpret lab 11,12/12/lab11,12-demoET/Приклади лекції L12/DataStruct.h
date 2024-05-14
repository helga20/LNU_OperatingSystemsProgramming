#ifndef DataStructuresForCalcET001
#define DataStructuresForCalcET001
// структури даних дл€ обчисленн€ формул - файл DataStruct.h
//------------------------------------------------------------
// структури дл€ сканера
struct ETindex // ≥ндекс таблиц≥
{
  int row,col; // ≥ндекси р€дка ≥ стовпц€ електронноњ таблиц≥
}; // struct ETindex

struct Ttoken  // визначенн€ лексеми
{
  int token;    // тип лексеми
	// уточненн€ зм≥сту лексеми
	union // сп≥льна д≥л€нка пам'€т≥ (накладанн€)
	{
    ETindex index; // ≥ндекси р€дка ≥ стовпц€
		double cellsvalue; // або значенн€ числа в ком≥рц≥
		char * lit;     // або текстовий р€док (л≥терал)
	};	
}; // struct Ttoken

const Ttoken NulTok = {0,0,0}; // ф≥ктивна лексема

enum { maxsizelist=100 }; // максимально допустимий розм≥р списку лексем

enum   // список ≥мен ≥ код≥в лексем
{ s_equal=101, s_plus=102, s_minus=103, s_mult=104, s_div=105, openbracket=106,
	closebracket=107, colon=108, fsum=201, fmax=202, fmin=203, fmyabs=204, fint=205,
	cell_et=1, number_et=2, lit1=301, lit2=302, end_list=0 }; // уважно!  без redefinition!
//-----------------------------------------------------------
#endif