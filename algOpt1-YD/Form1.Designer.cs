
namespace algOpt1_YD
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);

        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.graphPanel = new System.Windows.Forms.Panel();
            this.vertexList = new System.Windows.Forms.CheckedListBox();
            this.btnShowCycles = new System.Windows.Forms.Button();
            this.btnAddVertex = new System.Windows.Forms.Button();
            this.btnRemoveVertex = new System.Windows.Forms.Button();
            this.txtWeight = new System.Windows.Forms.TextBox();
            this.btnRemoveEdge = new System.Windows.Forms.Button();
            this.btnAddEdge = new System.Windows.Forms.Button();
            this.comboFrom = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.comboTo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtVertexName = new System.Windows.Forms.RichTextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.txtCycles = new System.Windows.Forms.RichTextBox();
            this.btnSimulation = new System.Windows.Forms.Button();
            this.matrixBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // graphPanel
            // 
            this.graphPanel.Location = new System.Drawing.Point(13, 12);
            this.graphPanel.Name = "graphPanel";
            this.graphPanel.Size = new System.Drawing.Size(936, 531);
            this.graphPanel.TabIndex = 0;
            this.graphPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.graphPanel_Paint);
            this.graphPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.graphPanel_MouseMove);
            // 
            // vertexList
            // 
            this.vertexList.FormattingEnabled = true;
            this.vertexList.Location = new System.Drawing.Point(785, 559);
            this.vertexList.Name = "vertexList";
            this.vertexList.Size = new System.Drawing.Size(163, 242);
            this.vertexList.TabIndex = 1;
            this.vertexList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.vertexList_ItemCheck);
            this.vertexList.SelectedIndexChanged += new System.EventHandler(this.vertexList_SelectedIndexChanged);
            // 
            // btnShowCycles
            // 
            this.btnShowCycles.Location = new System.Drawing.Point(331, 549);
            this.btnShowCycles.Name = "btnShowCycles";
            this.btnShowCycles.Size = new System.Drawing.Size(110, 40);
            this.btnShowCycles.TabIndex = 2;
            this.btnShowCycles.Text = "Циклы";
            this.btnShowCycles.UseVisualStyleBackColor = true;
            this.btnShowCycles.Click += new System.EventHandler(this.btnShowCycles_Click);
            // 
            // btnAddVertex
            // 
            this.btnAddVertex.Location = new System.Drawing.Point(669, 559);
            this.btnAddVertex.Name = "btnAddVertex";
            this.btnAddVertex.Size = new System.Drawing.Size(110, 40);
            this.btnAddVertex.TabIndex = 4;
            this.btnAddVertex.Text = "+Вершина";
            this.btnAddVertex.UseVisualStyleBackColor = true;
            this.btnAddVertex.Click += new System.EventHandler(this.btnAddVertex_Click);
            // 
            // btnRemoveVertex
            // 
            this.btnRemoveVertex.Location = new System.Drawing.Point(669, 605);
            this.btnRemoveVertex.Name = "btnRemoveVertex";
            this.btnRemoveVertex.Size = new System.Drawing.Size(110, 40);
            this.btnRemoveVertex.TabIndex = 5;
            this.btnRemoveVertex.Text = "-Вершина";
            this.btnRemoveVertex.UseVisualStyleBackColor = true;
            this.btnRemoveVertex.Click += new System.EventHandler(this.btnRemoveVertex_Click);
            // 
            // txtWeight
            // 
            this.txtWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtWeight.Location = new System.Drawing.Point(669, 651);
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.Size = new System.Drawing.Size(110, 30);
            this.txtWeight.TabIndex = 9;
            // 
            // btnRemoveEdge
            // 
            this.btnRemoveEdge.Location = new System.Drawing.Point(669, 733);
            this.btnRemoveEdge.Name = "btnRemoveEdge";
            this.btnRemoveEdge.Size = new System.Drawing.Size(110, 40);
            this.btnRemoveEdge.TabIndex = 8;
            this.btnRemoveEdge.Text = "-Связь";
            this.btnRemoveEdge.UseVisualStyleBackColor = true;
            this.btnRemoveEdge.Click += new System.EventHandler(this.btnRemoveEdge_Click);
            // 
            // btnAddEdge
            // 
            this.btnAddEdge.Location = new System.Drawing.Point(669, 687);
            this.btnAddEdge.Name = "btnAddEdge";
            this.btnAddEdge.Size = new System.Drawing.Size(110, 40);
            this.btnAddEdge.TabIndex = 7;
            this.btnAddEdge.Text = "+Связь";
            this.btnAddEdge.UseVisualStyleBackColor = true;
            this.btnAddEdge.Click += new System.EventHandler(this.btnAddEdge_Click);
            // 
            // comboFrom
            // 
            this.comboFrom.FormattingEnabled = true;
            this.comboFrom.Location = new System.Drawing.Point(14, 35);
            this.comboFrom.Name = "comboFrom";
            this.comboFrom.Size = new System.Drawing.Size(121, 24);
            this.comboFrom.TabIndex = 10;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.comboTo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.comboFrom);
            this.panel1.Location = new System.Drawing.Point(497, 671);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(150, 130);
            this.panel1.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 17);
            this.label2.TabIndex = 13;
            this.label2.Text = "Куда";
            // 
            // comboTo
            // 
            this.comboTo.FormattingEnabled = true;
            this.comboTo.Location = new System.Drawing.Point(14, 85);
            this.comboTo.Name = "comboTo";
            this.comboTo.Size = new System.Drawing.Size(121, 24);
            this.comboTo.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 11;
            this.label1.Text = "Откуда";
            // 
            // txtVertexName
            // 
            this.txtVertexName.Location = new System.Drawing.Point(497, 559);
            this.txtVertexName.Name = "txtVertexName";
            this.txtVertexName.Size = new System.Drawing.Size(150, 106);
            this.txtVertexName.TabIndex = 12;
            this.txtVertexName.Text = "";
            this.txtVertexName.TextChanged += new System.EventHandler(this.txtVertexName_TextChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 761);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(85, 40);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(220, 761);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(85, 40);
            this.btnLoad.TabIndex = 14;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // txtCycles
            // 
            this.txtCycles.Location = new System.Drawing.Point(12, 549);
            this.txtCycles.Name = "txtCycles";
            this.txtCycles.Size = new System.Drawing.Size(293, 189);
            this.txtCycles.TabIndex = 15;
            this.txtCycles.Text = "";
            // 
            // btnSimulation
            // 
            this.btnSimulation.Location = new System.Drawing.Point(331, 605);
            this.btnSimulation.Name = "btnSimulation";
            this.btnSimulation.Size = new System.Drawing.Size(110, 40);
            this.btnSimulation.TabIndex = 16;
            this.btnSimulation.Text = "Имп. мод";
            this.btnSimulation.UseVisualStyleBackColor = true;
            this.btnSimulation.Click += new System.EventHandler(this.btnSimulation_Click);
            // 
            // matrixBtn
            // 
            this.matrixBtn.Location = new System.Drawing.Point(331, 713);
            this.matrixBtn.Name = "matrixBtn";
            this.matrixBtn.Size = new System.Drawing.Size(110, 40);
            this.matrixBtn.TabIndex = 17;
            this.matrixBtn.Text = "Устойчивость";
            this.matrixBtn.UseVisualStyleBackColor = true;
            this.matrixBtn.Visible = false;
            this.matrixBtn.Click += new System.EventHandler(this.matrixBtn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(331, 664);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 40);
            this.button1.TabIndex = 18;
            this.button1.Text = "Удалить Всё";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(961, 824);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.matrixBtn);
            this.Controls.Add(this.btnSimulation);
            this.Controls.Add(this.txtCycles);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtVertexName);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtWeight);
            this.Controls.Add(this.btnRemoveEdge);
            this.Controls.Add(this.btnAddEdge);
            this.Controls.Add(this.btnRemoveVertex);
            this.Controls.Add(this.btnAddVertex);
            this.Controls.Add(this.btnShowCycles);
            this.Controls.Add(this.vertexList);
            this.Controls.Add(this.graphPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel graphPanel;
        private System.Windows.Forms.CheckedListBox vertexList;
        private System.Windows.Forms.Button btnShowCycles;
        private System.Windows.Forms.Button btnAddVertex;
        private System.Windows.Forms.Button btnRemoveVertex;
        private System.Windows.Forms.TextBox txtWeight;
        private System.Windows.Forms.Button btnRemoveEdge;
        private System.Windows.Forms.Button btnAddEdge;
        private System.Windows.Forms.ComboBox comboFrom;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox txtVertexName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.RichTextBox txtCycles;
        private System.Windows.Forms.Button btnSimulation;
        private System.Windows.Forms.Button matrixBtn;
        private System.Windows.Forms.Button button1;
    }
}

