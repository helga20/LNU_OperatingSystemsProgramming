stack 101h
segment main				; main program segment
	lea ax,text
	mov ds,ax
	MOV AX, AB
	MOV BX, ZN
	XCHG AX, BX 
	MOV PRINT, AX
	LEA DX, PRINT
	MOV AH,9
	int 21h
	xchg bh,bl
	mov print,bx
	lea dx, print
	mov ah,9
	int 21h
	mov     ax,4C00h
	int     21h
 	ret

segment text
	ab dw 4142h
	zn dw 3c2bh
	print dw 00H
	db 13
	db 10
	db "$"