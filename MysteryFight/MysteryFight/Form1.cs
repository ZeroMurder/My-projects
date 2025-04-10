using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MysteryFight
{
    public partial class Form1 : Form
    {
        private int gold = 100; // Начальное количество золота
        private int currentWeaponDamage = 10; // Начальный урон
        private string currentWeaponName = "Кулак"; //Начальное оружие
        private int playerHealth = 250; // Здоровье игрока
        private Boss currentBoss; // Текущий босс
        private Random random = new Random();

        private List<Weapon> weapons = new List<Weapon>()
        {
            new Weapon { Name = "Меч", Damage = 30, Cost = 15, ImageUrl = "https://i.ibb.co/4pm8n4w/sword.png" },
            new Weapon { Name = "Топор", Damage = 60, Cost = 30, ImageUrl = "https://i.ibb.co/jvbX9Ty/axe.png" },
            new Weapon { Name = "Копье", Damage = 70, Cost = 45, ImageUrl = "https://i.ibb.co/6478Cqf/spear.png" }
        };

        private List<Boss> bosses = new List<Boss>()
        {
            new Boss { Name = "Ассасин", Health = 140, ImageUrl = "https://i.ibb.co/0j34sbh/assassin.png" },
            new Boss { Name = "Солдат", Health = 240, ImageUrl = "https://i.ibb.co/w4dXkJL/soldier.png" },
            new Boss { Name = "Розетта", Health = 500, ImageUrl = "https://i.ibb.co/WnDvvZq/rosetta.png" }
        };

        private List<Card> cards = new List<Card>();

        public Form1()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            // Инициализация карт с боссами
            foreach (var boss in bosses)
            {
                cards.Add(new Card { Name = boss.Name, Boss = boss, ImageUrl = "https://i.ibb.co/zQ9GPyz/fon.jpg" });
            }

            UpdateGoldLabel();
            LoadWeaponsToShop();
            LoadCardImages();
            UpdatePlayerHealthLabel();
        }
        private async void LoadCardImages()
        {
            flowLayoutPanelCards.Controls.Clear();
            foreach (var card in cards)
            {
                Panel cardPanel = new Panel
                {
                    Width = 150,
                    Height = 200
                };

                PictureBox cardImage = new PictureBox
                {
                    Dock = DockStyle.Fill,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                };
                await LoadImageFromUrl(card.ImageUrl, cardImage);

                Button bossButton = new Button
                {
                    Text = card.Name,
                    Dock = DockStyle.Bottom,
                    Height = 30,
                    BackColor = Color.DarkRed,
                    ForeColor = Color.White
                };
                bossButton.Click += (sender, e) => StartFight(card.Boss);

                cardPanel.Controls.Add(cardImage);
                cardPanel.Controls.Add(bossButton);
                flowLayoutPanelCards.Controls.Add(cardPanel);
            }
        }

        private void LoadWeaponsToShop()
        {
            listBoxWeapons.Items.Clear();
            foreach (var weapon in weapons)
            {
                listBoxWeapons.Items.Add(weapon.Name);
            }
        }
        private async Task LoadImageFromUrl(string url, PictureBox pictureBox)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    {
                        pictureBox.Image = Image.FromStream(stream);
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Ошибка при загрузке изображения: {e.Message}");
                    pictureBox.Image = null;
                }
            }
        }
        private void UpdateGoldLabel()
        {
            labelGold.Text = $"Золото: {gold}";
        }
        private void UpdatePlayerHealthLabel()
        {
            labelPlayerHealth.Text = $"Здоровье: {playerHealth}";
        }
        private async void StartFight(Boss boss)
        {
            currentBoss = boss;
            playerHealth = 250;
            currentBoss.Health = boss.Health;
            UpdatePlayerHealthLabel();
            labelBossName.Text = boss.Name;

            await LoadImageFromUrl("https://i.ibb.co/6m9Bqtz/arena.jpg", pictureBoxBattleBackground);
            await LoadImageFromUrl(boss.ImageUrl, pictureBoxBoss);
            await LoadImageFromUrl("https://i.ibb.co/j4T2WKh/player.png", pictureBoxPlayer);

            panelShop.Visible = false;
            panelCards.Visible = false;
            panelBattle.Visible = true;
        }
        private void buttonAttack_Click(object sender, EventArgs e)
        {
            // Логика атаки
            int damage = currentWeaponDamage;
            currentBoss.Health -= damage;
            labelBossHealth.Text = $"Здоровье Босса: {currentBoss.Health}";
            // Босс отвечает
            if (currentBoss.Health > 0)
            {
                int bossDamage = random.Next(10, 30);
                playerHealth -= bossDamage;
                UpdatePlayerHealthLabel();

                if (playerHealth <= 0)
                {
                    MessageBox.Show("Вы проиграли!");
                    panelBattle.Visible = false;
                    panelCards.Visible = true;
                }
            }
            else
            {
                gold += 50;
                UpdateGoldLabel();
                MessageBox.Show($"Вы победили {currentBoss.Name} и получили 50 золота!");
                panelBattle.Visible = false;
                panelCards.Visible = true;
                LoadCardImages();
            }
        }
        private void buttonBuyWeapon_Click(object sender, EventArgs e)
        {
            if (listBoxWeapons.SelectedItem != null)
            {
                string selectedWeaponName = listBoxWeapons.SelectedItem.ToString();
                Weapon selectedWeapon = weapons.FirstOrDefault(w => w.Name == selectedWeaponName);

                if (selectedWeapon != null)
                {
                    if (gold >= selectedWeapon.Cost)
                    {
                        gold -= selectedWeapon.Cost;
                        UpdateGoldLabel();
                        currentWeaponDamage = selectedWeapon.Damage;
                        currentWeaponName = selectedWeapon.Name;
                        MessageBox.Show($"Вы купили {selectedWeapon.Name}!");
                    }
                    else
                    {
                        MessageBox.Show("Недостаточно золота!");
                    }
                }
            }
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0) // Shop tab
            {
                panelShop.Visible = true;
                panelCards.Visible = false;
                panelBattle.Visible = false;
            }
            else if (tabControl1.SelectedIndex == 1) // Cards tab
            {
                panelShop.Visible = false;
                panelCards.Visible = true;
                panelBattle.Visible = false;
            }
        }

        private void labelBossHealth_Click(object sender, EventArgs e)
        {

        }

        private void labelBossName_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxBattleBackground_Click(object sender, EventArgs e)
        {

        }

        private void labelPlayerHealth_Click(object sender, EventArgs e)
        {

        }
    }

    public struct Boss
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public string ImageUrl { get; set; }
    }

    public struct Card
    {
        public string Name { get; set; }
        public Boss Boss { get; set; }
        public string ImageUrl { get; set; }
    }

    public struct Weapon
    {
        public string Name { get; set; }
        public int Damage { get; set; }
        public int Cost { get; set; }
        public string ImageUrl { get; set; }
    }
}

