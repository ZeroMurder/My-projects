ORG 0x7C00

start:
    mov ax, 0x0003
    int 0x10
    mov ax, 0xB800
    mov es, ax
    mov di, 0x0000
    mov si, hello_msg
print_loop:
    lodsb
    test al, al
    jz load_kernel
    mov ah, 0x0E
    int 0x10
    jmp print_loop

load_kernel:
    mov ah, 0x02
    mov al, 1
    mov ch, 0
    mov cl, 2
    mov dh, 0
    mov dl, 0x80
    mov bx, 0x1000
    mov es, bx
    xor bx, bx
    int 0x13
    jc error
    jmp 0x1000:0x0000

error:
    mov si, error_msg
    jmp print_loop

hello_msg db "HELLO WORLD", 0
error_msg db "Disk Error!", 0

times 510-($-$$) db 0
dw 0xAA55



