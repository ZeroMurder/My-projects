namespace MysteryFight
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panelShop = new System.Windows.Forms.Panel();
            this.buttonBuyWeapon = new System.Windows.Forms.Button();
            this.listBoxWeapons = new System.Windows.Forms.ListBox();
            this.labelGold = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panelCards = new System.Windows.Forms.Panel();
            this.flowLayoutPanelCards = new System.Windows.Forms.FlowLayoutPanel();
            this.panelBattle = new System.Windows.Forms.Panel();
            this.labelPlayerHealth = new System.Windows.Forms.Label();
            this.labelBossHealth = new System.Windows.Forms.Label();
            this.labelBossName = new System.Windows.Forms.Label();
            this.pictureBoxPlayer = new System.Windows.Forms.PictureBox();
            this.pictureBoxBoss = new System.Windows.Forms.PictureBox();
            this.pictureBoxBattleBackground = new System.Windows.Forms.PictureBox();
            this.buttonAttack = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panelShop.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panelCards.SuspendLayout();
            this.panelBattle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlayer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBoss)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBattleBackground)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(410, 337);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panelShop);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(402, 311);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Магазин";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panelShop
            // 
            this.panelShop.Controls.Add(this.buttonBuyWeapon);
            this.panelShop.Controls.Add(this.listBoxWeapons);
            this.panelShop.Controls.Add(this.labelGold);
            this.panelShop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelShop.Location = new System.Drawing.Point(3, 3);
            this.panelShop.Name = "panelShop";
            this.panelShop.Size = new System.Drawing.Size(396, 305);
            this.panelShop.TabIndex = 0;
            // 
            // buttonBuyWeapon
            // 
            this.buttonBuyWeapon.Location = new System.Drawing.Point(6, 276);
            this.buttonBuyWeapon.Name = "buttonBuyWeapon";
            this.buttonBuyWeapon.Size = new System.Drawing.Size(100, 23);
            this.buttonBuyWeapon.TabIndex = 2;
            this.buttonBuyWeapon.Text = "Купить оружие";
            this.buttonBuyWeapon.UseVisualStyleBackColor = true;
            this.buttonBuyWeapon.Click += new System.EventHandler(this.buttonBuyWeapon_Click);
            // 
            // listBoxWeapons
            // 
            this.listBoxWeapons.FormattingEnabled = true;
            this.listBoxWeapons.Location = new System.Drawing.Point(6, 32);
            this.listBoxWeapons.Name = "listBoxWeapons";
            this.listBoxWeapons.Size = new System.Drawing.Size(154, 238);
            this.listBoxWeapons.TabIndex = 1;
            // 
            // labelGold
            // 
            this.labelGold.AutoSize = true;
            this.labelGold.Location = new System.Drawing.Point(3, 9);
            this.labelGold.Name = "labelGold";
            this.labelGold.Size = new System.Drawing.Size(46, 13);
            this.labelGold.TabIndex = 0;
            this.labelGold.Text = "Золото:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panelCards);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(402, 311);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Карты";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panelCards
            // 
            this.panelCards.Controls.Add(this.flowLayoutPanelCards);
            this.panelCards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCards.Location = new System.Drawing.Point(3, 3);
            this.panelCards.Name = "panelCards";
            this.panelCards.Size = new System.Drawing.Size(396, 305);
            this.panelCards.TabIndex = 0;
            // 
            // flowLayoutPanelCards
            // 
            this.flowLayoutPanelCards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelCards.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelCards.Name = "flowLayoutPanelCards";
            this.flowLayoutPanelCards.Size = new System.Drawing.Size(396, 305);
            this.flowLayoutPanelCards.TabIndex = 0;
            // 
            // panelBattle
            // 
            this.panelBattle.Controls.Add(this.labelPlayerHealth);
            this.panelBattle.Controls.Add(this.labelBossHealth);
            this.panelBattle.Controls.Add(this.labelBossName);
            this.panelBattle.Controls.Add(this.pictureBoxPlayer);
            this.panelBattle.Controls.Add(this.pictureBoxBoss);
            this.panelBattle.Controls.Add(this.pictureBoxBattleBackground);
            this.panelBattle.Controls.Add(this.buttonAttack);
            this.panelBattle.Location = new System.Drawing.Point(428, 34);
            this.panelBattle.Name = "panelBattle";
            this.panelBattle.Size = new System.Drawing.Size(400, 315);
            this.panelBattle.TabIndex = 1;
            // 
            // labelPlayerHealth
            // 
            this.labelPlayerHealth.AutoSize = true;
            this.labelPlayerHealth.BackColor = System.Drawing.Color.White;
            this.labelPlayerHealth.Location = new System.Drawing.Point(3, 19);
            this.labelPlayerHealth.Name = "labelPlayerHealth";
            this.labelPlayerHealth.Size = new System.Drawing.Size(62, 13);
            this.labelPlayerHealth.TabIndex = 6;
            this.labelPlayerHealth.Text = "Здоровье: ";
            this.labelPlayerHealth.Click += new System.EventHandler(this.labelPlayerHealth_Click);
            // 
            // labelBossHealth
            // 
            this.labelBossHealth.AutoSize = true;
            this.labelBossHealth.BackColor = System.Drawing.Color.White;
            this.labelBossHealth.Location = new System.Drawing.Point(284, 12);
            this.labelBossHealth.Name = "labelBossHealth";
            this.labelBossHealth.Size = new System.Drawing.Size(92, 13);
            this.labelBossHealth.TabIndex = 5;
            this.labelBossHealth.Text = "Здоровье босса:";
            this.labelBossHealth.Click += new System.EventHandler(this.labelBossHealth_Click);
            // 
            // labelBossName
            // 
            this.labelBossName.AutoSize = true;
            this.labelBossName.BackColor = System.Drawing.Color.White;
            this.labelBossName.Location = new System.Drawing.Point(293, 0);
            this.labelBossName.Name = "labelBossName";
            this.labelBossName.Size = new System.Drawing.Size(69, 13);
            this.labelBossName.TabIndex = 4;
            this.labelBossName.Text = "Имя Босса: ";
            this.labelBossName.Click += new System.EventHandler(this.labelBossName_Click);
            // 
            // pictureBoxPlayer
            // 
            this.pictureBoxPlayer.BackColor = System.Drawing.Color.White;
            this.pictureBoxPlayer.Location = new System.Drawing.Point(6, 35);
            this.pictureBoxPlayer.Name = "pictureBoxPlayer";
            this.pictureBoxPlayer.Size = new System.Drawing.Size(130, 148);
            this.pictureBoxPlayer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPlayer.TabIndex = 3;
            this.pictureBoxPlayer.TabStop = false;
            // 
            // pictureBoxBoss
            // 
            this.pictureBoxBoss.BackColor = System.Drawing.Color.White;
            this.pictureBoxBoss.Location = new System.Drawing.Point(217, 35);
            this.pictureBoxBoss.Name = "pictureBoxBoss";
            this.pictureBoxBoss.Size = new System.Drawing.Size(145, 148);
            this.pictureBoxBoss.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxBoss.TabIndex = 2;
            this.pictureBoxBoss.TabStop = false;
            // 
            // pictureBoxBattleBackground
            // 
            this.pictureBoxBattleBackground.BackColor = System.Drawing.Color.White;
            this.pictureBoxBattleBackground.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxBattleBackground.Name = "pictureBoxBattleBackground";
            this.pictureBoxBattleBackground.Size = new System.Drawing.Size(400, 315);
            this.pictureBoxBattleBackground.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxBattleBackground.TabIndex = 1;
            this.pictureBoxBattleBackground.TabStop = false;
            this.pictureBoxBattleBackground.Click += new System.EventHandler(this.pictureBoxBattleBackground_Click);
            // 
            // buttonAttack
            // 
            this.buttonAttack.Location = new System.Drawing.Point(3, 286);
            this.buttonAttack.Name = "buttonAttack";
            this.buttonAttack.Size = new System.Drawing.Size(75, 23);
            this.buttonAttack.TabIndex = 0;
            this.buttonAttack.Text = "Атака";
            this.buttonAttack.UseVisualStyleBackColor = true;
            this.buttonAttack.Click += new System.EventHandler(this.buttonAttack_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 361);
            this.Controls.Add(this.panelBattle);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Mystery Fight";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panelShop.ResumeLayout(false);
            this.panelShop.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.panelCards.ResumeLayout(false);
            this.panelBattle.ResumeLayout(false);
            this.panelBattle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlayer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBoss)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBattleBackground)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panelShop;
        private System.Windows.Forms.Button buttonBuyWeapon;
        private System.Windows.Forms.ListBox listBoxWeapons;
        private System.Windows.Forms.Label labelGold;
        private System.Windows.Forms.Panel panelCards;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelCards;
        private System.Windows.Forms.Panel panelBattle;
        private System.Windows.Forms.Button buttonAttack;
        private System.Windows.Forms.PictureBox pictureBoxBattleBackground;
        private System.Windows.Forms.PictureBox pictureBoxBoss;
        private System.Windows.Forms.PictureBox pictureBoxPlayer;
        private System.Windows.Forms.Label labelBossName;
        private System.Windows.Forms.Label labelBossHealth;
        private System.Windows.Forms.Label labelPlayerHealth;
    }
}


