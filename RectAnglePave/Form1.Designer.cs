namespace RectAnglePave
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Canvas_ = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.recalcToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recalc2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.squareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.Canvas_)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Canvas_
            // 
            this.Canvas_.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Canvas_.BackgroundImage")));
            this.Canvas_.Location = new System.Drawing.Point(3, 25);
            this.Canvas_.Name = "Canvas_";
            this.Canvas_.Size = new System.Drawing.Size(758, 569);
            this.Canvas_.TabIndex = 0;
            this.Canvas_.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.recalcToolStripMenuItem,
            this.recalc2ToolStripMenuItem,
            this.squareToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(764, 26);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // recalcToolStripMenuItem
            // 
            this.recalcToolStripMenuItem.Name = "recalcToolStripMenuItem";
            this.recalcToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.recalcToolStripMenuItem.Text = "幅の短い順に並べる";
            this.recalcToolStripMenuItem.Click += new System.EventHandler(this.recalcToolStripMenuItem_Click);
            // 
            // recalc2ToolStripMenuItem
            // 
            this.recalc2ToolStripMenuItem.Name = "recalc2ToolStripMenuItem";
            this.recalc2ToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.recalc2ToolStripMenuItem.Text = "幅の長い順に並べる";
            this.recalc2ToolStripMenuItem.Click += new System.EventHandler(this.recalc2ToolStripMenuItem_Click);
            // 
            // squareToolStripMenuItem
            // 
            this.squareToolStripMenuItem.Name = "squareToolStripMenuItem";
            this.squareToolStripMenuItem.Size = new System.Drawing.Size(68, 22);
            this.squareToolStripMenuItem.Text = "テスト用";
            this.squareToolStripMenuItem.Click += new System.EventHandler(this.squareToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 594);
            this.Controls.Add(this.Canvas_);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.Canvas_)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Canvas_;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem recalcToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recalc2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem squareToolStripMenuItem;
    }
}

