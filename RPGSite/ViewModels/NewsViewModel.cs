using System;

namespace RPGSite.ViewModels
{
    public class NewsViewModel
    {
        public int ID { get; set; }

        public string UserName { get; set; }

        public DateTime Date { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}