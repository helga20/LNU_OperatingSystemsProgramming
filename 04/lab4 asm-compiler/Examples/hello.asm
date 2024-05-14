org 	100h
mov 	ah,09h
lea 	dx,hello
int 	21h
ret
hello 	db "Hello world!"
	db 13
	db 10
	db 24h
