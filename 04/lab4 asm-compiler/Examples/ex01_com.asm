org 	100h
mov 	AX,AB
mov 	BX,ZN
xchg 	AX,BX
mov 	PRINT,AX
lea 	DX,PRINT
mov 	AH,09h
int 	21h
xchg 	BH,BL
mov 	print,BX
lea 	DX,print
mov 	AH,9
int 	21h
ret

ab 	dw "AB"
zn 	dw "<+"
print 	dw "  "
	db 13
	db 10
	db "$"