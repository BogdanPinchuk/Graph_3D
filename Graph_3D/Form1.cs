using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;

namespace Graph_3D
{
    public partial class Form1 : Form
    {
        ChartStyle cs;
        //ChartStyle2D cs2d;
        DataSeries ds;
        DrawChart dc;
        ChartFunctions cf;
        ColorMap cm;

        /// <summary>
        /// Кольори для графіків
        /// </summary>
        enum ColorChart
        {
            Jet,
            Parula,
            HVS,
            Hot,
            Cool,
            Spring,
            Summer,
            Autumn,
            Winter,
            Gray,
            Copper,
            Bone,
            Magma,
            Inferno,
            Plasma,
            Viridis,
            Spectrum
        }

        /// <summary>
        /// Вибраний в даний момент колір для графіка
        /// </summary>
        ColorChart tempC = ColorChart.Jet;

        /// <summary>
        /// Позиція курсора від початку нажимання кнопки миші в екранних координатах
        /// </summary>
        internal Point posCursor;
        /// <summary>
        /// Позиція зображення відносно курсора від початку нажимання кнопки миші
        /// </summary>
        internal Point posImg;
        /// <summary>
        /// Дозвід на поворот зображення
        /// </summary>
        internal bool testMoveImg = false;
        /// <summary>
        /// Коефіцієнт чутливості обертання
        /// </summary>
        private float koeS = 2f;

        /// <summary>
        /// Вибір графіка, якщо true - Peack3D, false - SinROverR3D
        /// </summary>
        private bool graph = true;

        public Form1()
        {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            BackColor = Color.White;

            cs = new ChartStyle(this);
            //cs2d = new ChartStyle2D(this);
            ds = new DataSeries();
            dc = new DrawChart(this);
            cf = new ChartFunctions();
            cm = new ColorMap();

            cs.GridStyle.LineColor = Color.LightGray;
            cs.GridStyle.Pattern = DashStyle.Dash;
            cs.Title = "No Title";

            //dc.ChartType = DrawChart.ChartTypeEnum.Surface;

            dc.IsColorMap = true;
            cs.IsColorBar = true;
            dc.IsHiddenLine = true;
            cs.Title = "3D Chart";

            ColorOfChart();

            ds.LineStyle.IsVisible = true;
            dc.IsInterp = false;
            //dc.NumberInterp = 3;
        }

        /// <summary>
        /// Оновлення кольору графіка
        /// </summary>
        private void ColorOfChart()
        {
            switch (tempC)
            {
                case ColorChart.Jet:
                    dc.CMap = cm.Jet();
                    break;
                case ColorChart.Parula:
                    dc.CMap = cm.Parula();
                    break;
                case ColorChart.HVS:
                    dc.CMap = cm.HVS();
                    break;
                case ColorChart.Hot:
                    dc.CMap = cm.Hot();
                    break;
                case ColorChart.Cool:
                    dc.CMap = cm.Cool();
                    break;
                case ColorChart.Spring:
                    dc.CMap = cm.Spring();
                    break;
                case ColorChart.Summer:
                    dc.CMap = cm.Summer();
                    break;
                case ColorChart.Autumn:
                    dc.CMap = cm.Autumn();
                    break;
                case ColorChart.Winter:
                    dc.CMap = cm.Winter();
                    break;
                case ColorChart.Gray:
                    dc.CMap = cm.Gray();
                    break;
                case ColorChart.Copper:
                    dc.CMap = cm.Copper();
                    break;
                case ColorChart.Bone:
                    dc.CMap = cm.Bone();
                    break;
                case ColorChart.Magma:
                    dc.CMap = cm.Magma();
                    break;
                case ColorChart.Inferno:
                    dc.CMap = cm.Inferno();
                    break;
                case ColorChart.Plasma:
                    dc.CMap = cm.Plasma();
                    break;
                case ColorChart.Viridis:
                    dc.CMap = cm.Viridis();
                    break;
                case ColorChart.Spectrum:
                    dc.CMap = cm.Spectrum();
                    break;
                default:
                    dc.CMap = cm.Jet();
                    break;
            }
        }

        /// <summary>
        /// Визивається кожен раз при необхідності перемалювати графік
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlotPicBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            cs.Elevation = trkElevation.Value;
            cs.Azimuth = trkAzimuth.Value;
            if (graph)
                cf.Peack3D(ds, cs);
            else
                cf.SinROverR3D(ds, cs);

            cs.AddChartStyle(g);
            dc.AddChart(g, ds, cs);//, cs2d);
        }

        private void TrkElevation_Scroll(object sender, EventArgs e)
        {
            tbElevation.Text = trkElevation.Value.ToString();
            ChooseUpdate();
        }

        private void TrkAzimuth_Scroll(object sender, EventArgs e)
        {
            tbAzimuth.Text = trkAzimuth.Value.ToString();
            ChooseUpdate();
        }

        private void TrkElevation_KeyUp(object sender, KeyEventArgs e)
        {
            ReadData(tbElevation, trkElevation);
            ChooseUpdate();
        }

        private void TrkAzimuth_KeyUp(object sender, KeyEventArgs e)
        {
            ReadData(tbAzimuth, trkAzimuth);
            ChooseUpdate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            ChooseUpdate();
        }

        private void ReadData(TextBox tb, TrackBar trk)
        {
            int value;

            bool result = int.TryParse(tb.Text, out value);

            int znach;

            if (tb.Name == "tbElevation")
                znach = 90;
            else
                znach = 180;

            if (result)
            {
                if (value <= -znach)
                {
                    value = -znach;
                    tb.Text = value.ToString();
                }
                else if (value >= znach)
                {
                    value = znach;
                    tb.Text = value.ToString();
                }

                trk.Value = value;
            }
        }

        private void TbElevation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ReadData(tbElevation, trkElevation);
                ChooseUpdate(); ;
            }
        }

        private void TbAzimuth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ReadData(tbAzimuth, trkAzimuth);
                ChooseUpdate();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // загрузка налаштувань
            Properties.Settings.Default.Reload();

            Location = Properties.Settings.Default.formLocat;
            Size = Properties.Settings.Default.formSize;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // збереження налаштувань
            Properties.Settings.Default.formLocat = Location;
            Properties.Settings.Default.formSize = Size;

            Properties.Settings.Default.Save();
            Properties.Settings.Default.Upgrade();
        }

        /// <summary>
        /// Save the picture in boxpicture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SavePictureAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (plotPicBox.InitialImage != null)
            {
                // create the user dialog...
                Bitmap bitmap = new Bitmap(plotPicBox.Width, plotPicBox.Height);
                plotPicBox.DrawToBitmap(bitmap, new Rectangle(new Point(0, 0),
                    plotPicBox.Size));

                // save in jpeg
                long compr = 100;
                EncoderParameters ep = new EncoderParameters(1);
                ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, compr);
                ImageCodecInfo ici = GetEncoder(ImageFormat.Jpeg);
                bitmap.Save("savepicture.jpg", ici, ep);

                bitmap.Save("savepicture.wmf",
                        ImageFormat.Wmf);



                return;
                // first,
                try
                {
                    plotPicBox.InitialImage.Save("savepicture.jpg",
                        ImageFormat.Jpeg);

                    MessageBox.Show("You saved the image", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("You are the error in save image.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                    return codec;
            }
            return null;
        }

        private void BtUpdate_Click(object sender, EventArgs e)
        {
            dc.SlowShow = true;

            ChooseUpdate();

            dc.SlowShow = false;
        }

        internal void ChooseUpdate()
        {
            ColorOfChart();

            if (tabControl1.SelectedIndex == 0)
            {
                plotPicBox.Refresh();
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                plotPanel.Refresh();
            }
            else
            {
                plotPicBox.Refresh();
                plotPanel.Refresh();
            }
        }

        /// <summary>
        /// Переміщення мишки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlotPicBox_MouseMove(object sender, MouseEventArgs e)
        {
            // Керуємо відображенням курсора миші
            MouseForm(plotPicBox);

            // Поворот зображення, якщо є дозвіл
            if (testMoveImg && (e.Button == MouseButtons.Right))
            {
                MouseMove(e, trkElevation, trkAzimuth);

                // оновлення області для відобрадення маркера
                plotPicBox.Refresh();
            }
        }

        /// <summary>
        /// Зміна форми вказівника мишки в залежності від того де вона знаходиться
        /// </summary>
        /// <param name="picB">Область зображенння над якою знаходиться мишка</param>
        private void MouseForm(PictureBox picB)
        {
            if (MouseButtons == MouseButtons.Right)
            {
                // віддаємо перевагу нажаттю правої кнопки миші
                picB.Cursor = Cursors.SizeAll;
            }
            else
            {
                // нічого не нажато, то вигляд - стандартний
                picB.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Збереження позиції курсора
        /// </summary>
        /// <param name="e">Параметри мишки</param>
        /// <param name="pos">Позиція автоскролів</param>
        private void MouseSave(MouseEventArgs e, TrackBar elevation, TrackBar azimuth)
        {
            // права кнопка миши
            if (e.Button == MouseButtons.Right)
            {
                // Зберігаємо позицію місця курсора миші
                posCursor = MousePosition;

                // Зберігаємо позицію повзунків
                posImg.X = azimuth.Value;
                posImg.Y = elevation.Value;
            }
        }

        /// <summary>
        /// Переміщення зображення при нажатій правій клавіші мишки
        /// </summary>
        /// <param name="e">Параметри мишки</param>
        /// <param name="pos">Позиція автоскролів</param>
        private new void MouseMove(MouseEventArgs e, TrackBar elevation, TrackBar azimuth)
        {
            // Різниця переміщення курсора
            Point deltaCur = new Point
            {
                // визначаємо різницю переменіщення
                X = MousePosition.X - posCursor.X,
                Y = MousePosition.Y - posCursor.Y
            };

            //Point pos = new Point(posImg.X - deltaCur.X, posImg.Y - deltaCur.Y);
            int dx, // = posImg.X - deltaCur.X,
                dy; // = posImg.Y - deltaCur.Y;

            // ділення керує чутливістю переміщення
            dx = azimuth.Value + (int)(deltaCur.X / koeS);
            dy = elevation.Value + (int)(deltaCur.Y / koeS);

            if (dx < -180)
                dx = -180;
            else if (dx > 180)
                dx = 180;

            if (dy < -90)
                dy = -90;
            else if (dy > 90)
                dy = 90;

            azimuth.Value = dx;
            elevation.Value = dy;

            // збереження координат мишки
            MouseSave(e, trkElevation, trkAzimuth);

            // записуємо дані
            tbAzimuth.Text = azimuth.Value.ToString();
            tbElevation.Text = elevation.Value.ToString();
        }

        /// <summary>
        /// Змініємо курсор, якщо мишка входить в область елемента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlotPicBox_MouseEnter(object sender, EventArgs e)
        {
            // якщо входить в межі, то міняємо змінну
            testMoveImg = true;

            // Керуємо відображенням курсора миші
            MouseForm(plotPicBox);
        }

        /// <summary>
        /// Міняємо курсор, якщо він виходить за область елемента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlotPicBox_MouseLeave(object sender, EventArgs e)
        {
            // якщо виходить в межі, то міняємо змінну
            testMoveImg = false;

            // виходим за межі, то міняємо курсор
            plotPicBox.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Відбувається коли відпускається кнопка миші
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlotPicBox_MouseUp(object sender, MouseEventArgs e)
        {
            // Керуємо відображенням курсора миші
            MouseForm(plotPicBox);
        }

        /// <summary>
        /// Відбувається коли нажимається кнопка миши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlotPicBox_MouseDown(object sender, MouseEventArgs e)
        {
            // Керуємо відображенням курсора миші
            MouseForm(plotPicBox);

            // збереження координат мишки
            MouseSave(e, trkElevation, trkAzimuth);
        }

        /// <summary>
        /// Подія при прокрутці колеса мишки над даним елементом
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlotPicBox_MouseWheel(object sender, MouseEventArgs e)
        {
            #region help
            // https://docs.microsoft.com/ru-ru/dotnet/api/system.windows.forms.control.mousewheel?view=netcore-3.1 
            #endregion

            switch (ModifierKeys)
            {
                case Keys.Shift:
                    MouseScroll(trkElevation, e, 180);
                    
                    // записуємо дані
                    tbElevation.Text = trkElevation.Value.ToString();
                    break;
                case Keys.Control:
                    MouseScroll(trkAzimuth, e, 90);

                    // записуємо дані
                    tbAzimuth.Text = trkAzimuth.Value.ToString();
                    break;
            }

            // Примітка. Використаня Alt визиває ссилку на меню (воно зазвичай виділяється і деякі букви підсвічуються),
            // тому після її затиснення, і натискання іншої клавіші (наприклад Shift) поворот може не спрацьовувати
            // доки не буде зроблення скидання виклику меню
            // Рекомендують використання "OnMouseWheel(MouseEventArgs)", але недолык втому, що воно працюэ для
            // всієї форми, а в даному випадку можна детально вручну налаштувати під відповідний елемент форми

            // оновлення області для відобрадення маркера
            plotPicBox.Refresh();
        }

        /// <summary>
        /// Обробка обертання колеса мишки
        /// </summary>
        /// <param name="angle">Кут за яким відбуватиметься оберт</param>
        /// <param name="e">Доступ до параметрів мишки</param>
        /// <param name="limit">Обмеженя на відповідний кут</param>
        private void MouseScroll(TrackBar angle, MouseEventArgs e, int limit)
        {
            // коефіцієнт прокрутки колеса
            int k_w = 120 * 3;

            // кількість ліній прокрутки
            int lines = e.Delta * SystemInformation.MouseWheelScrollLines / k_w;
            
            // ділення керує чутливістю переміщення
            int dz = angle.Value + lines;

            if (dz < -Math.Abs(limit))
                dz = -Math.Abs(limit);
            else if (dz > Math.Abs(limit))
                dz = Math.Abs(limit);

            angle.Value = dz;
        }

        private void Graph0ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graph = true;
            ChooseUpdate();
        }

        private void Graph1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graph = false;
            ChooseUpdate();
        }

        private void JetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tempC = ColorChart.Jet;
            ChooseUpdate();
        }

        private void HotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tempC = ColorChart.Hot;
            ChooseUpdate();
        }

        private void CoolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tempC = ColorChart.Cool;
            ChooseUpdate();
        }

        private void SpringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tempC = ColorChart.Spring;
            ChooseUpdate();
        }

        private void SummerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tempC = ColorChart.Summer;
            ChooseUpdate();
        }

        private void AutumnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tempC = ColorChart.Autumn;
            ChooseUpdate();
        }

        private void WinterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tempC = ColorChart.Winter;
            ChooseUpdate();
        }

        private void GrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tempC = ColorChart.Gray;
            ChooseUpdate();
        }

        private void ParulaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tempC = ColorChart.Parula;
            ChooseUpdate();
        }

        private void HvsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tempC = ColorChart.HVS;
            ChooseUpdate();
        }

        private void CopperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tempC = ColorChart.Copper;
            ChooseUpdate();
        }

        private void BoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tempC = ColorChart.Bone;
            ChooseUpdate();
        }

        private void MagmaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tempC = ColorChart.Magma;
            ChooseUpdate();
        }

        private void InfernoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tempC = ColorChart.Inferno;
            ChooseUpdate();
        }

        private void PlasmaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tempC = ColorChart.Plasma;
            ChooseUpdate();
        }

        private void ViridisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tempC = ColorChart.Viridis;
            ChooseUpdate();
        }

        private void SpectrumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tempC = ColorChart.Spectrum;
            ChooseUpdate();
        }

    }
}
