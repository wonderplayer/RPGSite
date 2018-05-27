using RPGSite.Models;
using System;
using System.Collections.Generic;

namespace RPGSite.ViewModels
{
    public class PostDetailViewModel
    {
        public Posts Post { get; set; }

        public DateTime Date { get; set; }

        public List<Comments> Comments { get; set; }

        public Comments Comment { get; set; }

    }
}