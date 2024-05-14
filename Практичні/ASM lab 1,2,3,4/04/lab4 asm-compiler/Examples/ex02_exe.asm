stack 100h
segment main
	BEGIN:	JMP	FIRSTCM
		PV1	DB	"Enter first number  : $"
		PV2	DB	"Enter second number  : $"
		PV3	DB	"Enter third number  : $"
	LINEFEED  	DB	13	;    
			DB	10
			DB	"$"
	COUNTNB	DW	1
FIRSTCM:  	CMP	COUNTNB,2	;
		JL	PRINTPV1	; 
		JE	PRINTPV2	; 
;  (JG)
		LEA	DX,PV3
		JMP	FUNC

PRINTPV1:  	LEA	DX,PV1
		JMP	FUNC

PRINTPV2:  	LEA	DX,PV2;

FUNC:		MOV	AH,9	;  DOS - 
		INT	21h	;  
        	
		LEA	DX,INBUFFER
		MOV	AH,0AH	;  
		INT	21H	; 
;       	
		MOV	SI,0	;  
		CMP	BUFFX,"-"	;    
		JE	MINUS
		CMP	BUFFX,"+"	;    
		JE	PLUS
		MOV	ZNAK,+1	;    - 
		JMP	CYFRY
	MINUS:	MOV	ZNAK,-1	;  "
		INC	SI	;    
  
		JMP	CYFRY
	PLUS:	MOV	ZNAK,+1	;  
		INC	SI	;     
		JMP	CYFRY
CYFRY:	MOV	AL,REALBYTES
		CBW
		SUB	AX,SI	;  
		MOV	CX,AX	;  
		MOV	BX,10	; 
		MOV	AX,0	;   =0
CONVERT:  	MUL	BX	;   *10
		PUSH	AX
		MOV	AL,BUFFX[SI]	;  
		AND	AL,00001111B	;    
		CBW
		MOV	DX,AX
		POP	AX
		ADD	AX,DX	;  
		INC	SI
		LOOP	CONVERT	;    
		IMUL	ZNAK	;  
; "    AX
		MOV	DI,COUNTNB
		DEC	DI
		SHL	DI,1	;  *2 -  
		MOV	NUMBXX[DI],AX
;    
		LEA	DX,LINEFEED	;    
		MOV	AH,9
		INT	21H
;
		INC	COUNTNB
		CMP	COUNTNB,3
		JG	COMPUT
		JMP	FIRSTCM
;   
INBUFFER  	DB	8	;    
REALBYTES 	DB	?	;   
	BUFFX	DB	"        "	; 8  -    
	
NUMBXX	DW	?	;   
		DW	?
		DW	?
ZNAK		DW	?
;
;   
; 1)   
COMPUT:	MOV	CX,3	;    
		LEA	SI,NUMBXX
ADD3N:	MOV	DX,[SI]
		ADD	SUM,DX
		INC	SI
		INC	SI
		LOOP	ADD3N
; 2)  a*8 + b*2 + c/4    -   
		MOV	AX,NUMBXX	; a
		MOV	CL,3
		SHL	AX,CL	;   3   ->  *8
		MOV	BX,NUMBXX+2	; b
		SHL	BX,1	;   1   ->	 *2
		ADD	AX,BX
		MOV	BX,NUMBXX+4	; c
		MOV	CL,2
		SAR	BX,CL	;   2  ->  /4
		ADD	AX,BX	;    
		MOV	SUM+2,AX
		JMP	FORMPRINT
;  
SUM		DW	0	;   
		DW	?	;   
;
;  ,     
FORMPRINT:  LEA	DX,PVDREZ
		MOV	AH,9
		INT	21H	;  
CKLR:		MOV	SI,CNR
		DEC	SI
		SHL	SI,1	;  *2
		MOV	AX,SUM[SI]	; AX -   
		CMP	AX,0	;  AX < 0 ?
		JGE	DALI
		MOV	OUTBUFF,"-"
		NEG	AX	;  
		JMP	MODUL
	DALI:	MOV	OUTBUFF," "
MODUL:	MOV	BX,10	; 
		XOR	SI,SI	;  SI -    
DEFD:		XOR	DX,DX	;  AX  4-
		DIV	BX
		OR	DL,"0"	; DL -  -  
		PUSH	DX
		INC	SI
		CMP	AX,0	;    
		JNZ	DEFD
		LEA	DI,OUTBUFF+1	;   
		MOV	CX,SI	;    
		CLD		;  DI  STOSB
LOOPC:	POP	AX
		STOSB		;    
		LOOP	LOOPC	;   
		MOV	AL,"$"	;   
		STOSB
		LEA	DX,PRFIX
		MOV	AH,9
		INT	21H
;  
		INC	CNR
		CMP	CNR,2
		JLE	CKLR
;   
		RET
;   
	PVDREZ	DB	":  $"

	PRFIX		DB	"  "	;  2    
	OUTBUFF	DB	"          "  ; 10    
	CNR		DW	1