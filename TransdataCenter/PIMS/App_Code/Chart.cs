using System;
using System.IO;//用于文件存取
using System.Data;//用于数据访问
using System.Drawing;//提供画GDI+图形的基本功能
using System.Drawing.Text;//提供画GDI+图形的高级功能
using System.Drawing.Drawing2D;//提供画高级二维，矢量图形功能
using System.Drawing.Imaging;//提供画GDI+图形的高级功能

    public abstract class Chart
    {
        //Render是图形大标题，图开小标题，图形宽度，图形长度，饼图的数据集和饼图的数据集要表示出来的数据
        public static Image PieChart(string title, string subTitle, int width, int height, DataSet chartData, int DataLine)
        {
            const int SIDE_LENGTH = 400;
            const int PIE_DIAMETER = 200;
            DataTable dt = chartData.Tables[0];



            //通过输入参数，取得饼图中的总基数
            float sumData = 0;
            foreach (DataRow dr in dt.Rows)
            {
                sumData += Convert.ToSingle(dr[DataLine]);
            }
            //产生一个image对象，并由此产生一个Graphics对象
            Bitmap bm = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bm);
            //设置对象g的属性
            g.ScaleTransform((Convert.ToSingle(width)) / SIDE_LENGTH, (Convert.ToSingle(height)) / SIDE_LENGTH);
            g.SmoothingMode = SmoothingMode.Default;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            //画布和边的设定
            g.Clear(Color.White);
            g.DrawRectangle(Pens.Black, 0, 0, SIDE_LENGTH - 1, SIDE_LENGTH - 1);
            //画饼图标题
            g.DrawString(title, new Font("Tahoma", 14), Brushes.Black, new PointF(5, 5));
            //画饼图的图例
            g.DrawString(subTitle, new Font("Tahoma", 12), Brushes.Black, new PointF(7, 35));
            //画饼图
            float curAngle = 0;
            float totalAngle = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                curAngle = Convert.ToSingle(dt.Rows[i][DataLine]) / sumData * 360;




                g.FillPie(new SolidBrush(ChartUtil.GetChartItemColor(i)), 100, 65, PIE_DIAMETER, PIE_DIAMETER, totalAngle, curAngle);
                g.DrawPie(Pens.Black, 100, 65, PIE_DIAMETER, PIE_DIAMETER, totalAngle, curAngle);
                totalAngle += curAngle;
            }
            //画图例框及其文字
            g.DrawRectangle(Pens.Black, 200, 300, 199, 99);
            g.DrawString("图表说明", new Font("Tahoma", 12, FontStyle.Bold), Brushes.Black, new PointF(200, 300));




            //画图例各项
            PointF boxOrigin = new PointF(210, 330);
            PointF textOrigin = new PointF(235, 326);
            float percent = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                g.FillRectangle(new SolidBrush(ChartUtil.GetChartItemColor(i)), boxOrigin.X, boxOrigin.Y, 20, 10);
                g.DrawRectangle(Pens.Black, boxOrigin.X, boxOrigin.Y, 20, 10);
                percent = Convert.ToSingle(dt.Rows[i][DataLine]) / sumData * 100;
                g.DrawString(dt.Rows[i][1].ToString() + " - " + dt.Rows[i][0].ToString() + " (" + percent.ToString("0") + "%)", new Font("Tahoma", 10), Brushes.Black, textOrigin);
                boxOrigin.Y += 15;
                textOrigin.Y += 15;
            }
            //回收资源
            g.Dispose();
            return (Image)bm;


        }


        //画条形图
        public static Image BarChart(string title, int width, int height, DataTable dt)
        {
            const int Title_LEFTTOP = 5;
            const int TitleFontSize = 14;
            const int ChartFontSize = 10;
            const int CHART_LEFT = 10;
            //计算最高的点
            float highPoint = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (highPoint < Convert.ToSingle(dr["yName"]))
                {
                    highPoint = Convert.ToSingle(dr["yName"]);
                }
            }
            int CHART_TOP = Title_LEFTTOP + TitleFontSize + 5 + ChartFontSize + 5 + ChartFontSize * highPoint.ToString().Length;
            int CHART_HEIGHT = height - CHART_TOP - (ChartFontSize + 5) * 2 - CHART_LEFT;
            int CHART_WIDTH = width - CHART_LEFT - 10;

            //建立一个Graphics对象实例
            Bitmap bm = new Bitmap(width, height);
            try
            {
                Graphics g = Graphics.FromImage(bm);
                //设置条图图形和文字属性
                //g.ScaleTransform((Convert.ToSingle(width)) / SIDE_LENGTH, (Convert.ToSingle(height)) / SIDE_LENGTH);
                //g.SmoothingMode = SmoothingMode.Default;
                g.TextRenderingHint = TextRenderingHint.AntiAlias;

                //设定画布和边
                g.Clear(Color.White);
                //g.DrawRectangle(Pens.Black, 0, 0, width - 1, height - 1);
                //画大标题
                g.DrawString(title, new Font("黑体", TitleFontSize), Brushes.Black, new PointF(Title_LEFTTOP, Title_LEFTTOP));
                //画条形图
                float barWidth = CHART_WIDTH / (dt.Rows.Count * 2), barHeight;
                PointF barOrigin = new PointF(CHART_LEFT + barWidth, 0);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    barHeight = Convert.ToSingle(dt.Rows[i]["yName"]) * CHART_HEIGHT / highPoint;
                    barOrigin.Y = CHART_TOP + CHART_HEIGHT - barHeight;
                    g.FillRectangle(new SolidBrush(ChartUtil.GetChartItemColor(i)), barOrigin.X, barOrigin.Y, barWidth, barHeight);

                    StringFormat SFormat = new StringFormat();
                    SFormat.FormatFlags = StringFormatFlags.DirectionVertical;

                    float TextWidth = ChartFontSize * dt.Rows[i]["xName"].ToString().Length;
                    g.DrawString(dt.Rows[i]["xName"].ToString(), new Font("Tahoma", ChartFontSize), Brushes.Black, new PointF(barOrigin.X - (TextWidth - barWidth) / 2, CHART_TOP + CHART_HEIGHT + 5));//每个条图x轴显示的

                    float TextHeight = ChartFontSize * dt.Rows[i]["yName"].ToString().Length;
                    g.DrawString(dt.Rows[i]["yName"].ToString(), new Font("Tahoma", ChartFontSize), Brushes.Black, new PointF(barOrigin.X - (ChartFontSize * 2 - barWidth) / 2, barOrigin.Y - TextHeight), SFormat);//每个条图上方显示的

                    barOrigin.X = barOrigin.X + (barWidth * 2);
                }
                //画坐标轴
                g.DrawLine(new Pen(Color.Black, 2), new Point(CHART_LEFT, CHART_TOP), new Point(CHART_LEFT, CHART_TOP + CHART_HEIGHT));
                g.DrawLine(new Pen(Color.Black, 2), new Point(CHART_LEFT, CHART_TOP + CHART_HEIGHT), new Point(CHART_LEFT + CHART_WIDTH, CHART_TOP + CHART_HEIGHT));

                //输出图形
                g.Dispose();
                return bm;
            }
            catch
            {
                return bm;
            }
        }
    }
    public class ChartUtil
    {
        public ChartUtil()
        {
        }
        public static Color GetChartItemColor(int itemIndex)
        {
            Color selectedColor;
            switch (itemIndex % 10)
            {
                case 1:
                    selectedColor = Color.Blue;
                    break;
                case 2:
                    selectedColor = Color.Red;
                    break;
                case 3:
                    selectedColor = Color.Yellow;
                    break;
                case 4:
                    selectedColor = Color.Purple;
                    break;
                case 5:
                    selectedColor = Color.Aqua;
                    break;
                case 6:
                    selectedColor = Color.Brown;
                    break;
                case 7:
                    selectedColor = Color.BurlyWood;
                    break;
                case 8:
                    selectedColor = Color.Cyan;
                    break;
                case 9:
                    selectedColor = Color.LightBlue;
                    break;
                default:
                    selectedColor = Color.Green;
                    break;
            }
            return selectedColor;


        }
    }