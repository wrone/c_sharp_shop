using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWeb
{
    public class newsClass
    {
        int id;
        string name, text;
        string date;
        public newsClass()
        {
            id = 0;
            name = "";
            text = "";
            date = "";
        }

        public newsClass(int id, string name, string text)
        {
            this.id = id;
            this.name = name;
            this.text = text;
        }

        public newsClass(int id, string name, string text, string date)
        {
            this.id = id;
            this.name = name;
            this.text = text;
            this.date = date;
        }

        public void setId(int ID)
        {
            this.id = ID;
        }

        public int getId()
        {
            return id;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public void setText(string text)
        {
            this.text = text;
        }

        public string getName()
        {
            return name;
        }

        public string getDate()
        {
            return date;
        }

        public string getText()
        {
            return text;
        }
    }
}
