using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Net;


namespace crack
{
    public partial class Form1 : Form
    {
        string file;
        string[] words;
        string[] chars = {" ", "о", "е", "а", "и", "н", "т", "с", "р", "в", "л", "к"};
        string[] alf = { "а", "б", "в", "г", "д", "е", "ё", "ж", "з", "и", "й", "к", "л", "м", "н", "о", "п", "р", "с", "т", "у", "ф", "х", "ц", "ч", "ш", "щ", "ъ", "ы", "ь", "э", "ю", "я", "А", "Б", "В", "Г", "Д", "Е", "Ё", "Ж", "З", "И", "Й", "К", "Л", "М", "Н", "О", "П", "Р", "С", "Т", "У", "Ф", "Х", "Ц", "Ч", "Ш", "Щ", "Ъ", "Ы", "Ь", "Э", "Ю", "Я", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", " ", ",", ".", "-", "!", "?", ":", "[", "]" };
        int kol;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "D:\\";
            openFileDialog1.Filter = "TXT (*.txt)|*.txt";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
                file = openFileDialog1.FileName;

            }

            words = search();

        }

        private string[] open ()
        {

          

            System.IO.StreamReader read = new System.IO.StreamReader(file);
            string s = read.ReadToEnd();

            String pattern = @"\s-\s?[+*]?\s?-\s., ";
            String[] res = Regex.Split(s, pattern);


            return res;
        }

        private string[] search ()
        {
            string[] res = new string[alf.Length] ;


            

            System.IO.StreamReader read = new System.IO.StreamReader(textBox1.Text);
            string s = read.ReadToEnd();
            int[] r = new int[s.Length];
            kol = s.Length;

            for (int h = 0; h<s.Length;h++)
            {
                r[h] = 0;
            }

            for(int i = 0; i<s.Length;i++)
            {
                for (int j = 0; j<alf.Length; j++)
                {
                    if(s[i].Equals(alf[j]))
                    {

                        if (r[j] == 0)
                            r[j] = 1;
                        else r[j]++;

                    }
                }
            }

            //сорировка
            int max = 0, k=0;
            int[] t = new int[r.Length];
            for (int i = 0; i<r.Length;i++)
            {
                for (int j = 1; j<r.Length;j++)
                {
                    if (max < r[j])
                    {
                        max = r[j];
                        k = j;
                    }
                }
                t[i] = k;
            }

            for (int i = 0; i<alf.Length;i++ )
            {
                res[i] = alf[t[i]];
            }


            return res;

        }

        private string[] translate()
        {
            string[] res = new string[kol];
            

            //переводит новым алфавитом

            return res;
        }

        private bool endtest(XmlDocument xml)
        {
            bool res = false;

            
            XmlElement xRoot = xml.DocumentElement;
            
            foreach (XmlNode xnode in xRoot)
            {
                if (xnode.Attributes.Count > 0)
                {
                    XmlNode attr = xnode.Attributes.GetNamedItem("error");
                    if (attr == null)
                        res = true;
                }
            }


                return res;
        } //проверить атрибут

        private XmlDocument post(string word)
        {
          
            //отправить post 
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://speller.yandex.net/services/spellservice/checkText?text=%20text="+word);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(new StreamReader(response.GetResponseStream(), Encoding.UTF8));
                return doc;
            }

            
        }

        private string[] ser (string[] tr)
        {
            System.IO.StreamReader read = new System.IO.StreamReader(file);
            string s = read.ReadToEnd();

            String pattern = @"\s-\s?[+*]?\s?-\s., ";
            String[] res = Regex.Split(s, pattern);


            return res;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] word;
            XmlDocument xml;

                // разделяет на слова
            words = open();
                //подсчитывает количество по каждой букве выводит алфавит по максимальному количеству букв
                string[] res = search();

                //цикл для изменения букв (алфавита)

                //переводит новым алфавитом исходный текст
                string[] tr = translate();

            //получить слово
            word = ser(tr);
            //отправляет post 
            for (int i = 0; i < word.Length; i++)
            {
                xml = post(word[i]);
                bool x = endtest(xml);
                //проверка ВСЕХ слов, если хоть одно нет, то замена букв

            }
            
            

               

                
            
            //вывод текста в текстбокс
        }


    }
}
