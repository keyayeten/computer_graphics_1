using System;
using System.Drawing;
using System.Windows.Forms;

namespace computer_graphics_lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Configure controls
            // ... (same as in the original code)
            // Add TextBoxes and Labels for x, y, r, and s
            string[] labelsText = { "X:", "Y:", "R:" };
            Point labelsLocation = new Point(30, 10);
            Point textBoxesLocation = new Point(40, 10);
            int deltaY = 30;

            for (int i = 0; i < labelsText.Length; i++)
            {
                Label label = new Label();
                label.Text = labelsText[i];
                label.Location = new Point(labelsLocation.X, labelsLocation.Y + deltaY * i);
                label.Size = new Size(40, 20); // Установите размер Label
                this.Controls.Add(label);

                TextBox textBox = new TextBox();
                textBox.Name = labelsText[i].ToLower().Replace(":", "") + "TextBox";
                textBox.Location = new Point(textBoxesLocation.X + 30, textBoxesLocation.Y + deltaY * i);
                this.Controls.Add(textBox);
            }

            // Add PictureBox
            PictureBox pictureBox = new PictureBox();
            pictureBox.Name = "pictureBox";
            pictureBox.Location = new Point(10, 120);
            pictureBox.Size = new Size(400, 400);
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(pictureBox);

            // Add Draw button
            Button drawButton = new Button();
            drawButton.Text = "Draw Circle";
            drawButton.Location = new Point(230, 10);
            drawButton.Click += new EventHandler(DrawButtonClick);
            this.Controls.Add(drawButton);

            Label sLabel = new Label();
            sLabel.Text = "S:";
            sLabel.Location = new Point(180, 10);
            sLabel.Size = new Size(20, 20);
            this.Controls.Add(sLabel);
            TextBox sTextBox = new TextBox();
            sTextBox.Name = "sTextBox";
            sTextBox.Location = new Point(200, 10);
            sTextBox.Size = new Size(40, 80);
            this.Controls.Add(sTextBox);
            drawButton.Text = "Draw Circle";
            drawButton.Location = new Point(310, 10);
            drawButton.Size = new Size(100, 100);
            drawButton.Click += new EventHandler(DrawButtonClick);
            this.Controls.Add(drawButton);
            // Add ComboBox for selecting the color of the circle
            Label colorLabel = new Label();
            colorLabel.Text = "Цвет: ";
            colorLabel.Location = new Point(180, 45);
            this.Controls.Add(colorLabel);
            ComboBox colorComboBox = new ComboBox();
            colorComboBox.Name = "colorComboBox";
            colorComboBox.Location = new Point(180, 70);
            colorComboBox.Items.AddRange(new string[] { "Black", "Red", "Green", "Blue" });
            this.Controls.Add(colorComboBox);
        }

        private void DrawButtonClick(object sender, EventArgs e)
        {
            try
            {
                // Get the values of the parameters x, y, r, and scaling factor s
                int x = int.Parse(((TextBox)this.Controls["xTextBox"]).Text);
                int y = int.Parse(((TextBox)this.Controls["yTextBox"]).Text);
                int r = int.Parse(((TextBox)this.Controls["rTextBox"]).Text);
                float s = float.Parse(((TextBox)this.Controls["sTextBox"]).Text);

                // Create a Bitmap and Graphics object
                Bitmap bitmap = new Bitmap(400, 400);
                Graphics graphics = Graphics.FromImage(bitmap);
                graphics.Clear(Color.White);

                // Draw the circle
                string selectedColor = ((ComboBox)this.Controls["colorComboBox"]).Text;
                Color color = Color.FromName(selectedColor);
                Pen pen = new Pen(color, 2);
                int xCoord = 200 + (int)(x * s);
                int yCoord = 200 - (int)(y * s);
                int radius = (int)(r * s);
                graphics.DrawEllipse(pen, xCoord - radius, yCoord - radius, radius * 2, radius * 2);

                // Set the image in the PictureBox
                PictureBox pictureBox = (PictureBox)this.Controls["pictureBox"];
                pictureBox.Image = bitmap;

                // Label axes and units
                graphics.DrawLine(pen, new Point(0, 200), new Point(400, 200));
                graphics.DrawLine(pen, new Point(200, 0), new Point(200, 400));
                Font font = new Font("Arial", 8);
                Brush brush = new SolidBrush(Color.Black);
                // Determine the step size for each axis based on the scale factor
                float stepSize = 100 * s;
                // Label the x-axis
                for (int i = 1; i <= 10; i++)
                {
                    graphics.DrawString((i * stepSize).ToString(), font, brush, new Point(200 + (int)(i * stepSize), 190));
                    graphics.DrawString((-i * stepSize).ToString(), font, brush, new Point(200 - (int)(i * stepSize), 190));
                }
                // Label the y-axis
                for (int i = 1; i <= 10; i++)
                {
                    graphics.DrawString((i * stepSize).ToString(), font, brush, new Point(210, 200 - (int)(i * stepSize)));
                    graphics.DrawString((-i * stepSize).ToString(), font, brush, new Point(210, 200 + (int)(i * stepSize)));
                }
                graphics.DrawString("0", font, brush, new Point(200, 200));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
    }
}
