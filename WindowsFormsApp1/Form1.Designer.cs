
namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnSync = new System.Windows.Forms.Button();
            this.BtnAsync = new System.Windows.Forms.Button();
            this.BtnAsyncAdvanced = new System.Windows.Forms.Button();
            this.BtnGetResult = new System.Windows.Forms.Button();
            this.BtnMutiple = new System.Windows.Forms.Button();
            this.BtnTaskSafe = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnSync
            // 
            this.BtnSync.Location = new System.Drawing.Point(41, 22);
            this.BtnSync.Name = "BtnSync";
            this.BtnSync.Size = new System.Drawing.Size(75, 23);
            this.BtnSync.TabIndex = 0;
            this.BtnSync.Text = "同步调用";
            this.BtnSync.UseVisualStyleBackColor = true;
            this.BtnSync.Click += new System.EventHandler(this.BtnSync_Click);
            // 
            // BtnAsync
            // 
            this.BtnAsync.Location = new System.Drawing.Point(41, 98);
            this.BtnAsync.Name = "BtnAsync";
            this.BtnAsync.Size = new System.Drawing.Size(75, 23);
            this.BtnAsync.TabIndex = 0;
            this.BtnAsync.Text = "异步调用";
            this.BtnAsync.UseVisualStyleBackColor = true;
            this.BtnAsync.Click += new System.EventHandler(this.BtnAsync_Click);
            // 
            // BtnAsyncAdvanced
            // 
            this.BtnAsyncAdvanced.Location = new System.Drawing.Point(41, 174);
            this.BtnAsyncAdvanced.Name = "BtnAsyncAdvanced";
            this.BtnAsyncAdvanced.Size = new System.Drawing.Size(75, 23);
            this.BtnAsyncAdvanced.TabIndex = 0;
            this.BtnAsyncAdvanced.Text = "异步进阶";
            this.BtnAsyncAdvanced.UseVisualStyleBackColor = true;
            this.BtnAsyncAdvanced.Click += new System.EventHandler(this.BtnAsyncAdvanced_Click);
            // 
            // BtnGetResult
            // 
            this.BtnGetResult.Location = new System.Drawing.Point(41, 240);
            this.BtnGetResult.Name = "BtnGetResult";
            this.BtnGetResult.Size = new System.Drawing.Size(75, 23);
            this.BtnGetResult.TabIndex = 0;
            this.BtnGetResult.Text = "获取异步操作返回值";
            this.BtnGetResult.UseVisualStyleBackColor = true;
            this.BtnGetResult.Click += new System.EventHandler(this.BtnGetResult_Click);
            // 
            // BtnMutiple
            // 
            this.BtnMutiple.Location = new System.Drawing.Point(164, 22);
            this.BtnMutiple.Name = "BtnMutiple";
            this.BtnMutiple.Size = new System.Drawing.Size(75, 23);
            this.BtnMutiple.TabIndex = 0;
            this.BtnMutiple.Text = "对比解读";
            this.BtnMutiple.UseVisualStyleBackColor = true;
            this.BtnMutiple.Click += new System.EventHandler(this.BtnMutiple_Click);
            // 
            // BtnTaskSafe
            // 
            this.BtnTaskSafe.Location = new System.Drawing.Point(164, 98);
            this.BtnTaskSafe.Name = "BtnTask";
            this.BtnTaskSafe.Size = new System.Drawing.Size(75, 23);
            this.BtnTaskSafe.TabIndex = 0;
            this.BtnTaskSafe.Text = "线程安全";
            this.BtnTaskSafe.UseVisualStyleBackColor = true;
            this.BtnTaskSafe.Click += new System.EventHandler(this.BtnTaskSafe_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 450);
            this.Controls.Add(this.BtnGetResult);
            this.Controls.Add(this.BtnAsyncAdvanced);
            this.Controls.Add(this.BtnAsync);
            this.Controls.Add(this.BtnTaskSafe);
            this.Controls.Add(this.BtnMutiple);
            this.Controls.Add(this.BtnSync);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnSync;
        private System.Windows.Forms.Button BtnAsync;
        private System.Windows.Forms.Button BtnAsyncAdvanced;
        private System.Windows.Forms.Button BtnGetResult;
        private System.Windows.Forms.Button BtnMutiple;
        private System.Windows.Forms.Button BtnTaskSafe;
    }
}

