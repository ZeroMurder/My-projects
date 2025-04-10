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
    add di, 2              ; Исправлено: видеопамять +2 байта на символ
    jmp print_hello

print_prompt:
    mov di, 160            ; Начало второй строки (80*2)
    mov si, prompt_msg
print_prompt_loop:
    lodsb
    test al, al
    jz input_loop
    mov ah, 0x0E
    int 0x10
    add di, 2              ; Корректное смещение
    jmp print_prompt_loop

input_loop:
    mov ah, 0x00
    int 0x16
    cmp al, 0x1A           ; Ctrl+Z - выполнить команду
    je execute_command
    cmp al, 0x18           ; Ctrl+X - новая строка
    je new_line
    cmp al, 0x08           ; Backspace
    je backspace
    cmp al, 0x7F           ; Delete
    je delete_func         ; Исправлено на delete_func
    cmp al, 0x20           ; Проверка на печатный символ
    jb input_loop
    mov ah, 0x0E
    int 0x10               ; Вывод символа
    add di, 2              ; Смещение видеопамяти
    jmp input_loop

new_line:
    mov ah, 0x03           ; Получить позицию курсора
    int 0x10
    inc dh                 ; Новая строка
    mov dl, 0              ; Начало строки
    mov ah, 0x02
    int 0x10               ; Установить курсор
    mov al, dh
    mov bl, 160
    mul bl                 ; DI = (строка) * 160
    mov di, ax
    jmp input_loop

execute_command:
    mov si, input_buffer
    mov cx, 0
    mov di, 160            ; Позиция ввода команды
read_cmd:
    mov ax, [es:di]
    cmp al, 0x20           ; Проверка на пробел/конец
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
    mov ah, 0x02  ; Функция BIOS для получения времени
    int 0x1A  ; Прерывание RTC
    call bcd_to_ascii
    ret

bcd_to_ascii:
    mov al, ch  ; Часы
    shr al, 4
    add al, 0x30
    mov [time_buffer], al
    mov al, ch
    and al, 0x0F
    add al, 0x30
    mov [time_buffer+1], al
    mov al, cl  ; Минуты
    shr al, 4
    add al, 0x30
    mov [time_buffer+3], al
    mov al, cl
    and al, 0x0F
    add al, 0x30
    mov [time_buffer+4], al
    mov al, dh  ; Секунды
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
    mov ah, 0x03  ; Получить текущую позицию курсора
    mov bh, 0x00  ; Номер страницы
    int 0x10
    push dx  ; Сохранить текущую позицию курсора
    dec dh  ; Перейти на предыдущую строку
    mov dl, 70  ; Позиция в конце строки
    mov ah, 0x02  ; Установить позицию курсора
    mov bh, 0x00  ; Номер страницы
    int 0x10
    mov si, time_msg  ; Вывод сообщения времени
    call print_string
    pop dx  ; Восстановить позицию курсора
    mov ah, 0x02  ; Установить позицию курсора
    mov bh, 0x00  ; Номер страницы
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
    cmp di, 160            ; Нельзя удалить приглашение
    jle input_loop
    sub di, 2              ; Удаление символа
    mov ax, 0x0E08         ; Backspace
    int 0x10
    mov al, ' '
    int 0x10
    mov al, 0x08
    int 0x10
    jmp input_loop

delete_func:
    cmp di, 160            ; Нельзя удалить приглашение
    jle input_loop
    mov ah, 0x0E
    mov al, ' '
    int 0x10
    mov ah, 0x0E
    mov al, 8
    int 0x10
    sub di, 2              ; Удаление символа
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
pause_flag db 1            ; Изначально пауза включена

times 0x1000-($-$$) db 0



