	.386P
	.MODEL	SMALL	; єдиний сегмент (модель SMALL)
CODESG	SEGMENT  DWORD PUBLIC USE16 'CODE'	;;;USE16 !
	ASSUME	CS:CODESG,DS:CODESG,SS:CODESG,ES:CODESG
	ORG	100H	;   резерв для PSP-префікса прогр. сегмента
BEGIN:	JMP	MAINPROG	;   обхід даних
MESS1	DB	'Testing: print a line:'
	DB	'$'
MAINPROG	PROC	NEAR	;  процедура ближнього типу
	MOV	AH,09	;  вивести на екран рядок до '$'
	LEA	DX,MESS1	;  адреса початку рядка запрошення
	INT	21H
	CALL	READLINE
	CALL	WRITELN
	JMP	EXIT	;  вихід по 4c/Int 21h
MAINPROG	ENDP
; процедура читання рядка
READLINE	PROC	NEAR
	MOV	AH,0AH	; прочитати з клавіатури рядок до Enter
	LEA	DX,MAXLEN	; адреса списку параметрів
	INT	21H
	RET	;
READLINE	ENDP
;  пам'ять та дані  для процедури читання рядка
MAXLEN	DB	30	;  максимальна довжина введеного рядка
REALLEN	DB	?	;  кількість фактично введених символів
POLE	DB	30  DUP ('_')	;  поле ( 30 літер ) для введених символів
;  процедура виведення на екран
WRITELN	PROC	NEAR
	MOV	AH,09
	LEA	DX,MESS2	;  рядок-повідомлення
	INT	21H
; друкуємо рядок, який прочитали
	MOV	AH,40H	;  вивести рядок вказаної довжини
	MOV	BX,1	;  на екран
; CX = довжина рядка
	MOV	CH,0	;  старший байт CX =0
	MOV	CL,REALLEN	;  кількість введених символів
	LEA	DX,POLE	;  адреса початку рядка
	INT	21H
; тепер ще надрукуємо літери для переходу до нового рядка
	MOV	AH,09
	LEA	DX,NEWLINE
	INT	21H
	RET
MESS2	DB	10,13,'This line:'
NEWLINE	DB	10,13	; (10,13) - новий рядок
	DB	'$'
WRITELN	ENDP
EXIT:   MOV     AX,4C00h  ;  функція 4C (76)
        INT     21h    ;  "припинити програму"
CODESG	ENDS
	END	BEGIN
