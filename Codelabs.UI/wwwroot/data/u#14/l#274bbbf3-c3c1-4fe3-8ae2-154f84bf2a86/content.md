# Заголовок для очень интересного текста
Очень интересный текст
# Как вывести Hello World на ассемблере:
```
.model small 
.stack 100h
.data
message db 'Hello World',13,10,'$' ;
.code
start:
    mov ax, @data
    mov ds, ax

    mov dx, offset message
    mov ah, 09h
    int 21h

    mov ah, 4Ch
    int 21h
end start
```