using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RectAnglePave
{
    public partial class Form1 : Form
    {
        class Node
        {
            public const bool Fixed    = true;
            public const bool UnFixed = false;

            //位置
            public Point Pos_ { get; private set; }

            //左に壁があるか
            public bool IsFixedLeft { get; set; }

            //右に壁があるか
            public bool IsFixedRight { get; set; }

            //上に壁があるか
            public bool IsFixedTop { get; set; }

            public Node(Point pos, bool isFixedLeft, bool isFixedRight)
            {
                Pos_ = pos;
               // IsFixed_ = isFixed;
                IsFixedLeft = isFixedLeft;
                IsFixedRight = isFixedRight;
            }

            bool IsEqual(Node other)
            {
                return Pos_ == other.Pos_;
            }          
        }

        List<Rectangle> Rectangles_;

        Rectangle MakingRect_;

        List<Node> Vertices;

        //最下段の最も左
        public static Point LowestLeftPosition { get; set; }
        Point DragStart {  get;   set;  }

        
        public Form1()
        {
            InitializeComponent();
            InitializeCanvas();

            InitializeMembers();
            AutoScrollMinSize = new Size(100, 100);
        }

        void InitializeMembers()
        {
            Vertices = new List<Node>();
            Vertices.Add(new Node(new Point(0, 0), Node.Fixed, Node.UnFixed));
            Rectangles_ = new List<Rectangle>();

            LowestLeftPosition = new Point();
        }

        void InitializeCanvas()
        {
            Canvas_.Paint += Canvas__Paint;

            Canvas_.MouseDown += delegate(object sender, MouseEventArgs e)
            {
                DragStart = e.Location;
                MakingRect_ = new Rectangle(DragStart.X, DragStart.Y, 0, 0);
            };


            Canvas_.MouseUp += delegate(object sender, MouseEventArgs e)
            {
                if (MakingRect_.Width <= 0 || MakingRect_.Height <= 0)
                    return;

                MakingRect_.Width  -= MakingRect_.Width  % 10;     // きりが良いように,10で切り下げ
                MakingRect_.Height -= MakingRect_.Height % 10;

                MakeRect(new Rectangle(MakingRect_.X, MakingRect_.Y, MakingRect_.Width, MakingRect_.Height));
                DragStart = e.Location;
                MakingRect_ = new Rectangle(DragStart.X, DragStart.Y, 0, 0);
            };


            Canvas_.MouseMove += delegate(object sender, MouseEventArgs e)
            {
                if (e.Button != System.Windows.Forms.MouseButtons.Left)
                    return;

                MakingRect_ = new Rectangle(DragStart.X, DragStart.Y, e.Location.X - DragStart.X, e.Location.Y - DragStart.Y);

                Canvas_.Invalidate();
            };
        }

        void Canvas__Paint(object sender, PaintEventArgs e)
        {
            var canvas = new Bitmap(Canvas_.Width, Canvas_.Height);
            Graphics g = Graphics.FromImage(canvas);

           // g.DrawRectangle(Pens.Black, 0, 0, Canvas_.Width, Canvas_.Height);

            //矩形描画
            foreach (var rect in Rectangles_)
            {
                g.DrawRectangle(Pens.Green, rect.X, rect.Y, rect.Width, rect.Height);
            }

            int pointSize = 10;
            foreach(var n in Vertices)
            {
                g.FillEllipse(Brushes.Blue, new Rectangle(n.Pos_.X - pointSize / 2, n.Pos_.Y - pointSize / 2, pointSize, pointSize));
            }
            
            //ドラッグで作成中の矩形描画
            if (MouseButtons == System.Windows.Forms.MouseButtons.Left)
            {
                g.DrawRectangle(Pens.Blue, MakingRect_.X, MakingRect_.Y, MakingRect_.Width, MakingRect_.Height);
            }

            g.Dispose();


            if (Canvas_.Image != null)
                Canvas_.Image.Dispose();

            Canvas_.Image = canvas;
        }

        bool CanPave(Rectangle rect)
        {
            //領域の外にでてはいけない
            if (rect.Right > Canvas_.Width)
                return false;

            bool ret = false;
            foreach(var r in Rectangles_)
            {
                ret |= CrossRect(r, rect);//r.IntersectsWith(rect);
            }

            return !ret;
        }

        //２つの矩形が重なっているか(接している場合は重なってないと判定)
        bool CrossRect(Rectangle r1, Rectangle r2)
        {
            bool notCross = r1.Right <= r2.Left || r1.Left >= r2.Right ||
                            r1.Top >= r2.Bottom || r1.Bottom <= r2.Top;

            return !notCross;
        }

        //(x,y) がr内にあるか(境界上はないと判定)
        bool InRegion(Rectangle r, float x, float y)
        {
            return r.Left < x && x < r.Right
                    && r.Top < y && y < r.Bottom;
        }


        //(x,y)がすべてのRectangle内のいずれかにあるか(境界上はないと判定)
        bool InRegion(float x, float y)
        {
            bool ret = false;
            foreach( var r in Rectangles_)
            {
                ret |= InRegion(r, x, y);
            }

            return ret;
        }

        //頂点を追加
        //戻り値 : 追加できたかどうか
        bool AddVertex(Point p)
        {            
            //同じ頂点があるか確認
            int index = Vertices.FindIndex(n => n.Pos_ == p);

            if (index < 0)
            {
                bool isFixedLeft = p.X == 0 || InRegion(p.X - 0.5f, p.Y);
                bool isFixedRight = p.X == Canvas_.Width || InRegion(p.X + 0.5f, p.Y);
                Vertices.Add(new Node(p,  isFixedLeft, isFixedRight));
                return true;
            }
            return false;
        }


        void MakeRect(Rectangle rect)
        {
            if(rect.Width > Canvas_.Width)
            {
                Canvas_.Width = Canvas_.Width * 2;
            }

            var SortedVertices = Vertices.OrderBy(n => n.Pos_.Y).ToList();

            int buildVertexIndex = -1;
            foreach (var n in SortedVertices.Select((v, i) => new { v, i }))
            {
                //左に壁がない
                if( !n.v.IsFixedRight )
                {
                    rect.X = n.v.Pos_.X;
                    rect.Y = n.v.Pos_.Y;

                    if (CanPave(rect))
                    {
                        buildVertexIndex = n.i;

                        //左には壁がある場合,もう置けない
                        if(n.v.IsFixedLeft)
                        {
                            Vertices.RemoveAt(Vertices.FindIndex(v => v.Pos_ == n.v.Pos_));
                        }

                        break;
                    }
                }

                if( !n.v.IsFixedLeft)
                {
                    rect.X = n.v.Pos_.X - rect.Width;
                    rect.Y = n.v.Pos_.Y;

                    if( rect.X >= 0 && CanPave(rect))
                    {
                        buildVertexIndex = n.i;

                        //右に壁がある場合はもう置けない
                        if (n.v.IsFixedRight)
                        {
                            Vertices.RemoveAt(Vertices.FindIndex(v => v.Pos_ == n.v.Pos_));
                        }
                        break;
                    }
                }
            }

            //横にものすごい長い矩形とかが来たらあり得る.
            // 最初に,矩形をソートして,横長順に配置していけば防げそう
            if (buildVertexIndex < 0)
            {
                rect.X = 0;
                rect.Y = LowestLeftPosition.Y;
            }

            //一番下の高さを保存しておく
            if (rect.Bottom > LowestLeftPosition.Y){
                LowestLeftPosition = new Point(0, rect.Bottom);
            }

            //下段を超えると長さを倍にする
            if(rect.Bottom > Canvas_.Height)
            {
                Canvas_.Height = Canvas_.Height * 2;
            }

            //矩形を追加
            Rectangles_.Add(rect);

            // 追加した矩形の端点を追加.
            {
                Point leftBottom = new Point(rect.Left, rect.Bottom);
                AddVertex(leftBottom);
            }

            {
                Point rightBottom = new Point(rect.Right, rect.Bottom);
                AddVertex(rightBottom);
            }

            {
                Point rightTop = new Point(rect.Right, rect.Top);
                AddVertex(rightTop);
            }

            //追加した矩形により、頂点の情報を更新

            for (int i = 0; i < Vertices.Count; )
            {
                var n = Vertices[i];

                //この頂点に置けなくなっているかチェック
                // TODO : 反対にも置けるようにした場合は,両方チェックする必要がある
                //int index = Rectangles_.FindIndex(r => InRegion(r, n.Pos_.X + 0.5f, n.Pos_.Y + 0.5f));

                bool isFixedLeft  = n.Pos_.X <= 0              || InRegion(rect, n.Pos_.X + 0.5f, n.Pos_.Y + 0.5f);
                bool isFixedRight = n.Pos_.Y >= Canvas_.Width  || InRegion(rect, n.Pos_.X - 0.5f, n.Pos_.Y + 0.5f);
                if( isFixedLeft &&
                    isFixedRight)
                {
                    Vertices.RemoveAt(i);
                }
                else
                {
                    n.IsFixedLeft = isFixedLeft;
                    n.IsFixedRight = isFixedRight;
                    i++;
                }
            }

            Canvas_.Invalidate();
        }

        private void recalcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Rectangle> backUp = new List<Rectangle>(Rectangles_);
            InitializeMembers();

            backUp = backUp.OrderBy(n => n.Width).ToList();

            foreach( var item in backUp)
            {
                MakeRect(item);
            }
        }

        private void recalc2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Rectangle> backUp = new List<Rectangle>(Rectangles_);
            InitializeMembers();

            backUp = backUp.OrderByDescending(n => n.Width).ToList();

            foreach (var item in backUp)
            {
                MakeRect(item);
            }
        }

        private void squareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitializeMembers();

            List<Rectangle> squares = new List<Rectangle>();

            Random r = new Random();
            for(int i=0; i<200; i++)
            {

                MakeRect(new Rectangle(0, 0, (r.Next() % 2 + 1) * 50, (r.Next() % 2 + 1) * 50));
            }
        }
    }
}
