document.addEventListener('DOMContentLoaded', function() {
    const messageInput = document.getElementById('message-input');
    const sendButton = document.getElementById('send-button');
    const chatLog = document.getElementById('chat-log');
    const upgradeLink = document.getElementById('upgrade-link');
    const buyButton = document.getElementById('buy-button');

    sendButton.addEventListener('click', function() {
        const message = messageInput.value;
        if (message.trim() !== '') {
            appendMessage('Вы', message);
            // Отправка запроса к ChatGPT API
            fetch('https://api.openai.com/v1/chat/completions', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer sk-5bJzW5BDe-x98r12-ovVnlX_hgVNK'
                },
                body: JSON.stringify({
                    model: 'gpt-3.5-turbo',
                    messages: [
                        { role: 'user', content: message }
                    ]
                })
            })
            .then(response => response.json())
            .then(data => {
                const responseMessage = data.choices[0].message.content;
                appendMessage('ChatGPT', responseMessage);
            })
            .catch(error => console.error('Ошибка:', error));
            messageInput.value = '';
        }
    });

    function appendMessage(sender, message) {
        const messageElement = document.createElement('div');
        messageElement.textContent = `${sender}: ${message}`;
        chatLog.appendChild(messageElement);
        chatLog.scrollTop = chatLog.scrollHeight; // Прокрутка вниз
    }

    upgradeLink.addEventListener('click', function(event) {
        event.preventDefault();
        alert('Переход на страницу Pro версии (функциональность не реализована).');
    });

    // Определение языка
    const userLanguage = navigator.language;

    // Функция для установки текста кнопки в зависимости от языка
    function setBuyButton() {
        if (userLanguage.startsWith('ru')) {
            buyButton.textContent = 'Купить';
        } else if (userLanguage.startsWith('en')) {
            buyButton.textContent = 'Buy';
        } else {
            buyButton.textContent = 'Buy Now'; // По умолчанию для других языков
        }

        // Добавьте логику для обработки клика по кнопке
        buyButton.addEventListener('click', function() {
            alert('Переход на страницу оплаты (функциональность не реализована).');
        });
    }

    setBuyButton();
});


