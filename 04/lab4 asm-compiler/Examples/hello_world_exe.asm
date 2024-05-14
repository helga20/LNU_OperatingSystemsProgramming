stack 101h
segment main				; main program segment

	lea	ax,text
	mov   ds,ax

	mov   ah, 9
	lea	dx,hello
	int 21h

	mov     ax,4C00h
	int     21h
	ret

segment text

hello db "Hello world!"
	db 13,10,24h
