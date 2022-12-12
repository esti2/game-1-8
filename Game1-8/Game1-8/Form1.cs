namespace Game1_8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static readonly int N = 3;
        private static readonly int CARD_SIZE = 100;
        private static readonly int GAP = 1;
        private readonly Color FORGROUND_COLOR = Color.Black;
        private readonly Color BACKGROUND_COLOR = Color.LightSeaGreen;
        private static readonly string WINNING_MESSAGE = "Wow - you are a genius!";


        private static Label[,] cards = new Label[N, N];

        private static readonly List<string> numbers = new List<string>
        {"1","2","3","4","5","6","7","8",""};

        private void Form1_Load(object sender, EventArgs e)
        {
            //Board initialization
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    cards[i, j] = new Label
                    {
                        Name = "card" + i + j,
                        Size = new Size(CARD_SIZE, CARD_SIZE),
                        Location = new Point(4 * GAP + i * CARD_SIZE, j * CARD_SIZE),
                        BackColor = BACKGROUND_COLOR,
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = FORGROUND_COLOR,
                        BorderStyle = BorderStyle.Fixed3D,
                    };
                    Controls.Add(cards[i, j]);
                    cards[i, j].Click += Card_Click;
                }
            }
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AssignIconsToCards();
        }
        private void AssignIconsToCards()
        {
            Random randow = new Random(Environment.TickCount);
            foreach (Control control in Controls)
            {
                Label label = control as Label;
                int rnd = randow.Next(numbers.Count);
                label.Text = numbers[rnd];
                label.ForeColor = FORGROUND_COLOR;
                numbers.RemoveAt(rnd);
            }
        }
        private void Card_Click(Object sender, EventArgs e)
        {
            String temp = "";
            Label label = sender as Label;
            int x = label.Bounds.X;
            int y = label.Bounds.Y;
            int TempX = (x - 4) / 100;
            int TempY = y / 100;
            if (label.Text == "")
                return;
            int i = -1;
            while (i <= 1)
            {
                if (TempX + i >= 0 && TempX + i <= N - 1)
                {
                    if (cards[TempX + i, TempY].Text == "")
                    {
                        temp = cards[TempX, TempY].Text;
                        label.Text = "";
                        cards[TempX + i, TempY].Text = temp;
                        cards[TempX, TempY].Text = "";
                    }
                }
                if (TempY + i >= 0 && TempY + i <= N - 1)
                {
                    if (cards[TempX, TempY + i].Text == "")
                    {
                        temp = cards[TempX, TempY].Text;
                        label.Text = "";
                        cards[TempX, TempY + i].Text = temp;
                        cards[TempX, TempY].Text = "";
                    }
                }
                if (endGame())
                    MessageBox.Show(WINNING_MESSAGE);
                i++;
            }
        }

        private Boolean endGame()
        {
            int num = 1;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (cards[j, i].Text != num.ToString())
                    {
                        return false;
                    }
                    num++;
                    if (num == 9)
                        return true;
                }
            }
            return true;
        }
    }
}
