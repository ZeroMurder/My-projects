const express = require('express');
const { Configuration, OpenAIApi } = require('openai');

const app = express();
app.use(express.json());

const configuration = new Configuration({
    apiKey: process.env.OPENAI_API_KEY,
});

const openai = new OpenAIApi(configuration);

app.post('/api/openai', async (req, res) => {
    try {
        const response = await openai.createChatCompletion({
            model: "gpt-3.5-turbo",
            messages: [{role: "user", content: req.body.question}],
            temperature: 0.7,
            max_tokens: 500
        });

        const answer = response.data.choices[0].message.content;
        res.json({ answer });
    } catch (error) {
        console.error('Ошибка API:', error);
        res.status(500).json({ error: 'Ошибка обработки запроса' });
    }
});

app.listen(3000, () => {
    console.log('Сервер запущен на порту 3000');
});
