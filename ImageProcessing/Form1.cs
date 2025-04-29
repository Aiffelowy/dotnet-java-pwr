using ImgProcessing;

namespace ImageProcessing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void file_picker_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (file_picker.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            Bitmap img = new Bitmap(file_picker.FileName);
            Bitmap grayscale = (Bitmap)img.Clone();
            Bitmap negative = (Bitmap)img.Clone();
            Bitmap edge = (Bitmap)img.Clone();
            Bitmap? mirror = (Bitmap)img.Clone();

            var threads = new List<Thread>(4);

            threads.Add(new Thread(() => { grayscale = Filter.apply(Filter.grayscale, grayscale); }));
            threads.Add(new Thread(() => { negative = Filter.apply(Filter.negative, negative); }));
            threads.Add(new Thread(() => { edge = Filter.detect_edges(edge); }));
            threads.Add(new Thread(() => { mirror = Filter.apply(Filter.mirror, mirror); }));

            threads.ForEach(thread => thread.Start());
            threads.ForEach(thread => thread.Join());


            imgbox_orig.Image = img;
            img_grey.Image = grayscale;
            img_edge.Image = edge;
            img_mirror.Image = mirror;
            img_negative.Image = negative;
        }
    }
}
