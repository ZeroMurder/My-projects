bits 32

section .text
global start

start:
    cli
    mov esp, stack_space
    call kmain
    hlt

; Функция kmain
kmain:
    ; Очистка экрана
    push 0x07 ; атрибут
    push video_memory ; адрес видеопамяти
    call clear_screen
    add esp, 8

    ; Вывод сообщения
    push message ; указатель на сообщение
    push video_memory ; адрес видеопамяти
    call print_string
    add esp, 8

    ; Вывод времени (пример)
    push 0x07 ; атрибут
    push video_memory + 160 ; адрес для вывода времени
    call show_utc_time
    add esp, 8

    ; Ожидание ввода
    call wait_for_input

    ret

; Функция очистки экрана
clear_screen:
    push ebp
    mov ebp, esp
    push esi
    push edi

    mov edi, [ebp + 8] ; адрес видеопамяти
    mov esi, 0 ; счетчик

clear_loop:
    cmp esi, 80 * 25 ; количество символов на экране
    je clear_done

    mov byte [edi + esi * 2], 0x20 ; пробел
    mov byte [edi + esi * 2 + 1], [ebp + 12] ; атрибут

    inc esi
    jmp clear_loop

clear_done:
    pop edi
    pop esi
    pop ebp
    ret

; Функция вывода строки
print_string:
    push ebp
    mov ebp, esp
    push esi
    push edi
    push eax
    push ecx

    mov edi, [ebp + 8] ; адрес видеопамяти
    mov esi, [ebp + 12] ; указатель на строку
    mov ecx, 0 ; счетчик

print_loop:
    mov al, byte [esi + ecx]
    cmp al, 0
    je print_done

    mov byte [edi + ecx * 2], al ; символ
    mov byte [edi + ecx * 2 + 1], 0x07 ; атрибут

    inc ecx
    jmp print_loop

print_done:
    pop ecx
    pop eax
    pop edi
    pop esi
    pop ebp
    ret

; Функция отображения времени UTC
show_utc_time:
    push ebp
    mov ebp, esp
    push eax
    push ebx
    push ecx
    push edx
    push esi
    push edi

    ; Чтение секунд
    mov al, 0x00
    out 0x70, al
    in al, 0x71
    mov bl, al ; секунды

    ; Чтение минут
    mov al, 0x02
    out 0x70, al
    in al, 0x71
    mov cl, al ; минуты

    ; Чтение часов
    mov al, 0x04
    out 0x70, al
    in al, 0x71
    mov dl, al ; часы

    ; Преобразование BCD в ASCII
    push edx ; сохраняем часы
    push ecx ; сохраняем минуты
    push ebx ; сохраняем секунды

    mov al, bl  ; секунды
    call bcd_to_ascii
    mov [seconds_ascii], eax

    mov al, cl  ; минуты
    call bcd_to_ascii
    mov [minutes_ascii], eax

    mov al, dl  ; часы
    call bcd_to_ascii
    mov [hours_ascii], eax

    pop ebx ; восстанавливаем секунды
    pop ecx ; восстанавливаем минуты
    pop edx ; восстанавливаем часы

    ; Вывод времени
    mov edi, [ebp + 8] ; адрес видеопамяти

    mov eax, [hours_ascii]
    mov byte [edi + 0], ah  ; часы (старший байт)
    mov byte [edi + 1], 0x07 ; атрибут
    mov byte [edi + 2], al  ; часы (младший байт)
    mov byte [edi + 3], 0x07 ; атрибут
    mov byte [edi + 4], ':'
    mov byte [edi + 5], 0x07
    mov eax, [minutes_ascii]
    mov byte [edi + 6], ah  ; минуты (старший байт)
    mov byte [edi + 7], 0x07 ; атрибут
    mov byte [edi + 8], al  ; минуты (младший байт)
    mov byte [edi + 9], 0x07 ; атрибут
    mov byte [edi + 10], ':'
    mov byte [edi + 11], 0x07
    mov eax, [seconds_ascii]
    mov byte [edi + 12], ah ; секунды (старший байт)
    mov byte [edi + 13], 0x07 ; атрибут
    mov byte [edi + 14], al ; секунды (младший байт)
    mov byte [edi + 15], 0x07 ; атрибут

    pop edi
    pop esi
    pop edx
    pop ecx
    pop ebx
    pop eax
    pop ebp
    ret

; BCD to ASCII conversion
bcd_to_ascii:
    push ebp
    mov ebp, esp
    push eax
    push ebx

    ; Старшая цифра
    mov bl, al
    shr bl, 4
    and bl, 0x0F
    add bl, '0'

    ; Младшая цифра
    and al, 0x0F
    add al, '0'

    movzx ebx, bl ; расширяем bl до 32 бит
    shl ebx, 8   ; сдвигаем старшую цифру на 8 бит влево
    or eax, ebx   ; объединяем старшую и младшую цифры

    pop ebx
    pop eax
    pop ebp
    ret

; Функция ожидания ввода с клавиатуры
wait_for_input:
    push eax

wait_loop:
    in al, 0x64 ; Читаем статус клавиатуры
    test al, 1  ; Проверяем, есть ли данные
    jz wait_loop ; Если нет, ждем

    in al, 0x60 ; Читаем скан-код клавиши

    pop eax
    ret

; Функции inb и outb
inb:
    mov edx, [esp + 4]
    in al, dx
    ret

outb:
    mov edx, [esp + 4]
    mov al, [esp + 8]
    out dx, al
    ret

section .data
message db "Hello, Kernel!", 0

hours_ascii   dw 0
minutes_ascii dw 0
seconds_ascii dw 0
video_memory  equ 0xB8000

section .bss
resb 8192
stack_space:
