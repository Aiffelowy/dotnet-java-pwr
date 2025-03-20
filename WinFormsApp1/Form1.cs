using Lab;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();

            int seed = Convert.ToInt32(this.seed_box.Value);
            int n_items = Convert.ToInt32(this.n_items_box.Value);
            int capacity = Convert.ToInt32(this.capacity_box.Value);

            Problem p = new Problem(n_items, seed);
            Result r = p.solve(capacity);

            r.items.ForEach(i => this.listBox1.Items.Add(p.item_list[i].ToString()));
            this.label4.Text = r.ToString();
        }
    }
}
