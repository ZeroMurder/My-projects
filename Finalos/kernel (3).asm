ORG 0x1000

print_prompt_again:
    mov di, 160
    mov si, prompt_msg
    jmp print_prompt_loop

start:
    mov ax, 0xB800
    mov es, ax
    mov di, 0x0000
    mov si, hello_msg
print_hello:
    lodsb
    test al, al
    jz print_prompt
    mov ah, 0x0E
    int 0x10
    add di, 2              ; ��ࠢ����: ����������� +2 ���� �� ᨬ���
    jmp print_hello

print_prompt:
    mov di, 160            ; ��砫� ��ன ��ப� (80*2)
    mov si, prompt_msg
print_prompt_loop:
    lodsb
    test al, al
    jz input_loop
    mov ah, 0x0E
    int 0x10
    add di, 2              ; ���४⭮� ᬥ饭��
    jmp print_prompt_loop

input_loop:
    mov ah, 0x00
    int 0x16
    cmp al, 0x1A           ; Ctrl+Z - �믮����� �������
    je execute_command
    cmp al, 0x18           ; Ctrl+X - ����� ��ப�
    je new_line
    cmp al, 0x08           ; Backspace
    je backspace
    cmp al, 0x7F           ; Delete
    je delete_func         ; ��ࠢ���� �� delete_func
    cmp al, 0x20           ; �஢�ઠ �� ����� ᨬ���
    jb input_loop
    mov ah, 0x0E
    int 0x10               ; �뢮� ᨬ����
    add di, 2              ; ���饭�� ����������
    jmp input_loop

new_line:
    mov ah, 0x03           ; ������� ������ �����
    int 0x10
    inc dh                 ; ����� ��ப�
    mov dl, 0              ; ��砫� ��ப�
    mov ah, 0x02
    int 0x10               ; ��⠭����� �����
    mov al, dh
    mov bl, 160
    mul bl                 ; DI = (��ப�) * 160
    mov di, ax
    jmp input_loop

execute_command:
    mov si, input_buffer
    mov cx, 0
    mov di, 160            ; ������ ����� �������
read_cmd:
    mov ax, [es:di]
    cmp al, 0x20           ; �஢�ઠ �� �஡��/�����
    je check_command
    mov [si], al
    inc si
    inc cx
    add di, 2
    jmp read_cmd

check_command:
    mov si, input_buffer
    mov di, clear_cmd
    mov cx, 0x0006
compare_clear:
    cmp cx, 0x0000
    jle clear_screen
    mov al, [si]
    cmp al, [di]
    jne not_clear
    inc si
    inc di
    dec cx
    jmp compare_clear

not_clear:
    mov si, input_buffer
    mov di, pause_cmd
    mov cx, 0x0005
compare_pause:
    cmp cx, 0x0000
    jle pause_mode
    mov al, [si]
    cmp al, [di]
    jne not_pause
    inc si
    inc di
    dec cx
    jmp compare_pause

not_pause:
    mov si, input_buffer
    mov di, start1_cmd
    mov cx, 0x0006
compare_start1:
    cmp cx, 0x0000
    jle start1_mode
    mov al, [si]
    cmp al, [di]
    jne not_start1
    inc si
    inc di
    dec cx
    jmp compare_start1

not_start1:
    mov si, input_buffer
    mov di, install_cmd
    mov cx, 0x0007
compare_install:
    cmp cx, 0x0000
    jle install_mode
    mov al, [si]
    cmp al, [di]
    jne not_install
    inc si
    inc di
    dec cx
    jmp compare_install

not_install:
    mov si, input_buffer
    mov di, ft_cmd
    mov cx, 0x0002
compare_ft:
    cmp cx, 0x0000
    jle ft_mode
    mov al, [si]
    cmp al, [di]
    jne print_prompt_again
    inc si
    inc di
    dec cx
    jmp compare_ft

pause_mode:
    mov byte [pause_flag], 1
    jmp print_prompt_again

start1_mode:
    mov byte [pause_flag], 0
    jmp clear_screen

install_mode:
    mov si, install_msg
    jmp print_install_msg

ft_mode:
    call get_time
    call print_time
    jmp print_prompt_again

print_install_msg:
    lodsb
    test al, al
    jz print_prompt_again
    mov ah, 0x0E
    int 0x10
    jmp print_install_msg

clear_screen:
    mov ax, 0x0003
    int 0x10
    mov di, 0x0000
    jmp print_hello

get_time:
    mov ah, 0x02  ; �㭪�� BIOS ��� ����祭�� �६���
    int 0x1A  ; ���뢠��� RTC
    call bcd_to_ascii
    ret

bcd_to_ascii:
    mov al, ch  ; ����
    shr al, 4
    add al, 0x30
    mov [time_buffer], al
    mov al, ch
    and al, 0x0F
    add al, 0x30
    mov [time_buffer+1], al
    mov al, cl  ; ������
    shr al, 4
    add al, 0x30
    mov [time_buffer+3], al
    mov al, cl
    and al, 0x0F
    add al, 0x30
    mov [time_buffer+4], al
    mov al, dh  ; ���㭤�
    shr al, 4
    add al, 0x30
    mov [time_buffer+6], al
    mov al, dh
    and al, 0x0F
    add al, 0x30
    mov [time_buffer+7], al
    ret

print_time:
    push ax
    push bx
    push cx
    push dx
    push si
    push di
    mov ah, 0x03  ; ������� ⥪���� ������ �����
    mov bh, 0x00  ; ����� ��࠭���
    int 0x10
    push dx  ; ���࠭��� ⥪���� ������ �����
    dec dh  ; ��३� �� �।����� ��ப�
    mov dl, 70  ; ������ � ���� ��ப�
    mov ah, 0x02  ; ��⠭����� ������ �����
    mov bh, 0x00  ; ����� ��࠭���
    int 0x10
    mov si, time_msg  ; �뢮� ᮮ�饭�� �६���
    call print_string
    pop dx  ; ����⠭����� ������ �����
    mov ah, 0x02  ; ��⠭����� ������ �����
    mov bh, 0x00  ; ����� ��࠭���
    int 0x10
    pop di
    pop si
    pop dx
    pop cx
    pop bx
    pop ax
    ret

print_string:
    push ax
    push bx
    push cx
    push dx
    push si
    push di
.next_char:
    lodsb
    test al, al
    jz .done
    mov ah, 0x0E
    int 0x10
    jmp .next_char
.done:
    pop di
    pop si
    pop dx
    pop cx
    pop bx
    pop ax
    ret

backspace:
    cmp di, 160            ; ����� 㤠���� �ਣ��襭��
    jle input_loop
    sub di, 2              ; �������� ᨬ����
    mov ax, 0x0E08         ; Backspace
    int 0x10
    mov al, ' '
    int 0x10
    mov al, 0x08
    int 0x10
    jmp input_loop

delete_func:
    cmp di, 160            ; ����� 㤠���� �ਣ��襭��
    jle input_loop
    mov ah, 0x0E
    mov al, ' '
    int 0x10
    mov ah, 0x0E
    mov al, 8
    int 0x10
    sub di, 2              ; �������� ᨬ����
    jmp input_loop

hello_msg db "HELLO WORLD", 0
prompt_msg db "> ", 0
commands_msg db "Available commands: /clear, pause, start1, install, ft", 0
clear_cmd db "/clear", 0
pause_cmd db "pause", 0
start1_cmd db "start1", 0
install_cmd db "install", 0
ft_cmd db "ft", 0
install_msg db "Installing...", 0
time_msg db "[UTC ", 0
time_buffer times 9 db "00:00:00", 0
input_buffer times 256 db 0
pause_flag db 1            ; ����砫쭮 ��㧠 ����祭�

times 0x1000-($-$$) db 0



