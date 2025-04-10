// Обратите внимание: для безопасности API-ключа лучше использовать бэкенд
const openaiApiKey = 'Ваш_API_Ключ'; // Не публикуйте ключ в открытом доступе

const configuration = new Configuration({
    apiKey: openaiApiKey,
});

const openai = new OpenAIApi(configuration);

// ... остальной JavaScript-код
